from __future__ import annotations

import json
import sys
from pathlib import Path

REPO_ROOT = Path(__file__).resolve().parents[2]
RUNTIME_ROOT = REPO_ROOT / "Assets" / "BattleTank" / "Scripts" / "Runtime"
DOCS_SNAPSHOT = REPO_ROOT / "Docs" / "ru" / "ARCH_SNAPSHOT_FOR_CHAT.md"

EXPECTED_GRAPH = {
    "BattleTank.Scripts.Runtime.Contracts": [],
    "BattleTank.Scripts.Runtime.Core": ["BattleTank.Scripts.Runtime.Contracts"],
    "BattleTank.Scripts.Runtime.Content": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
    ],
    "BattleTank.Scripts.Runtime.Input": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
    ],
    "BattleTank.Scripts.Runtime.Combat": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Content",
    ],
    "BattleTank.Scripts.Runtime.Tanks": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Input",
        "BattleTank.Scripts.Runtime.Combat",
        "BattleTank.Scripts.Runtime.Content",
    ],
    "BattleTank.Scripts.Runtime.AI": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Tanks",
        "BattleTank.Scripts.Runtime.Combat",
        "BattleTank.Scripts.Runtime.Content",
    ],
    "BattleTank.Scripts.Runtime.Levels": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Tanks",
        "BattleTank.Scripts.Runtime.AI",
        "BattleTank.Scripts.Runtime.Combat",
        "BattleTank.Scripts.Runtime.Content",
    ],
    "BattleTank.Scripts.Runtime.Persistence": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Content",
    ],
    "BattleTank.Scripts.Runtime.UI": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Input",
        "BattleTank.Scripts.Runtime.Content",
    ],
    "BattleTank.Scripts.Runtime.Tests": [
        "BattleTank.Scripts.Runtime.Contracts",
        "BattleTank.Scripts.Runtime.Core",
        "BattleTank.Scripts.Runtime.Content",
        "BattleTank.Scripts.Runtime.Input",
        "BattleTank.Scripts.Runtime.Combat",
        "BattleTank.Scripts.Runtime.Tanks",
        "BattleTank.Scripts.Runtime.AI",
        "BattleTank.Scripts.Runtime.Levels",
        "BattleTank.Scripts.Runtime.Persistence",
        "BattleTank.Scripts.Runtime.UI",
    ],
}


def load_json(path: Path) -> dict:
    with path.open("r", encoding="utf-8") as handle:
        return json.load(handle)


def detect_cycles(graph: dict[str, list[str]]) -> list[list[str]]:
    cycles = []
    visiting: set[str] = set()
    visited: set[str] = set()
    stack: list[str] = []

    def dfs(node: str) -> None:
        if node in visiting:
            cycle_start = stack.index(node)
            cycles.append(stack[cycle_start:] + [node])
            return
        if node in visited:
            return
        visiting.add(node)
        stack.append(node)
        for neighbor in graph.get(node, []):
            if neighbor in graph:
                dfs(neighbor)
        stack.pop()
        visiting.remove(node)
        visited.add(node)

    for node in graph:
        if node not in visited:
            dfs(node)

    return cycles


def main() -> int:
    errors: list[str] = []

    asmdef_paths = sorted(RUNTIME_ROOT.rglob("*.asmdef"))
    if not asmdef_paths:
        errors.append(f"No asmdef files found under {RUNTIME_ROOT}")
        print("\n".join(errors))
        return 1

    name_to_data: dict[str, dict] = {}
    for asmdef_path in asmdef_paths:
        data = load_json(asmdef_path)
        name = data.get("name")
        if not name:
            errors.append(f"{asmdef_path}: missing 'name' field")
            continue
        name_to_data[name] = data

        if asmdef_path.stem != name:
            errors.append(
                f"{asmdef_path}: filename '{asmdef_path.stem}' does not match name '{name}'"
            )

        if not name.startswith("BattleTank.Scripts.Runtime."):
            errors.append(f"{asmdef_path}: name must start with BattleTank.Scripts.Runtime.")

        references = data.get("references") or []
        if "Project." in name:
            errors.append(f"{asmdef_path}: name contains forbidden 'Project.' prefix")
        if any("Project." in ref for ref in references):
            errors.append(f"{asmdef_path}: reference contains forbidden 'Project.' prefix")

    missing_expected = sorted(set(EXPECTED_GRAPH) - set(name_to_data))
    if missing_expected:
        errors.append(f"Missing asmdef(s): {', '.join(missing_expected)}")

    for name, expected_refs in EXPECTED_GRAPH.items():
        data = name_to_data.get(name)
        if not data:
            continue
        actual_refs = data.get("references") or []
        actual_set = set(actual_refs)
        expected_set = set(expected_refs)
        missing = sorted(expected_set - actual_set)
        extra = sorted(actual_set - expected_set)
        if missing:
            errors.append(f"{name}: missing references: {', '.join(missing)}")
        if extra:
            errors.append(f"{name}: extra references: {', '.join(extra)}")

        if name.endswith(".Tests"):
            include_platforms = data.get("includePlatforms") or []
            if include_platforms != ["Editor"]:
                errors.append(
                    f"{name}: includePlatforms must be ['Editor'], got {include_platforms}"
                )

    actual_graph = {
        name: (data.get("references") or [])
        for name, data in name_to_data.items()
        if name in EXPECTED_GRAPH
    }
    cycles = detect_cycles(actual_graph)
    for cycle in cycles:
        errors.append(f"Circular dependency detected: {' -> '.join(cycle)}")

    if not DOCS_SNAPSHOT.exists():
        errors.append(f"Missing {DOCS_SNAPSHOT}")

    for module_dir in sorted(p for p in RUNTIME_ROOT.iterdir() if p.is_dir()):
        readme_path = module_dir / "README.txt"
        if not readme_path.exists():
            errors.append(f"Missing README.txt in {module_dir}")

    contracts_dir = RUNTIME_ROOT / "Contracts"
    if contracts_dir.exists():
        for subdir in sorted(p for p in contracts_dir.iterdir() if p.is_dir()):
            readme_path = subdir / "README.txt"
            if not readme_path.exists():
                errors.append(f"Missing README.txt in {subdir}")

    if errors:
        print("\n".join(errors))
        return 1

    print("OK")
    return 0


if __name__ == "__main__":
    sys.exit(main())
