# Architecture snapshot (для вставки в чат)

## 1) Факты проекта
- Платформа: Android, оффлайн.
- Ориентация: портрет.
- Камера: top-down.
- Движение игрока: 4 направления (up/down/left/right).
- Сцены (план): Bootstrap, MainMenu, Game, Garage — только вручную (manual-only).

## 2) Навигация по папкам
- Assets/BattleTank/
  - Images/
  - Prefabs/
  - ScriptableObjects/
  - Scripts/Runtime/
    - AI/
    - Combat/
    - Content/
    - Contracts/ (Enums, Interfaces, Ids)
    - Core/
    - Input/
    - Levels/
    - Persistence/
    - Tanks/
    - Tests/
    - UI/

## 3) Сборки (asmdef → путь)
- BattleTank.Scripts.Runtime.Contracts → Assets/BattleTank/Scripts/Runtime/Contracts
- BattleTank.Scripts.Runtime.Core → Assets/BattleTank/Scripts/Runtime/Core
- BattleTank.Scripts.Runtime.Content → Assets/BattleTank/Scripts/Runtime/Content
- BattleTank.Scripts.Runtime.Input → Assets/BattleTank/Scripts/Runtime/Input
- BattleTank.Scripts.Runtime.Combat → Assets/BattleTank/Scripts/Runtime/Combat
- BattleTank.Scripts.Runtime.Tanks → Assets/BattleTank/Scripts/Runtime/Tanks
- BattleTank.Scripts.Runtime.AI → Assets/BattleTank/Scripts/Runtime/AI
- BattleTank.Scripts.Runtime.Levels → Assets/BattleTank/Scripts/Runtime/Levels
- BattleTank.Scripts.Runtime.Persistence → Assets/BattleTank/Scripts/Runtime/Persistence
- BattleTank.Scripts.Runtime.UI → Assets/BattleTank/Scripts/Runtime/UI
- BattleTank.Scripts.Runtime.Tests → Assets/BattleTank/Scripts/Runtime/Tests

## 4) Граф зависимостей (ожидаемый)
- Contracts → (нет)
- Core → Contracts
- Content → Contracts, Core
- Input → Contracts, Core
- Combat → Contracts, Core, Content
- Tanks → Contracts, Core, Input, Combat, Content
- AI → Contracts, Core, Tanks, Combat, Content
- Levels → Contracts, Core, Tanks, AI, Combat, Content
- Persistence → Contracts, Core, Content
- UI → Contracts, Core, Input, Content
- Tests → Contracts, Core, Content, Input, Combat, Tanks, AI, Levels, Persistence, UI (Editor only)

## 5) Правило размещения Contracts
- Contracts: только межмодульные/публичные контракты (2+ модулей и/или API сохранений/данных).
- Локальные контракты — внутри своего модуля (или private в классе).
- Примеры: GameState/Team/ResourceType в Contracts; локальные enum AI — в AI.

## 6) Правило размещения данных (ScriptableObjects)
- Ассеты: Assets/BattleTank/ScriptableObjects/.
- Код классов SO: Assets/BattleTank/Scripts/Runtime/Content/.
- Навигация: классы SO держим в Content/Definitions (или аналогичной подпапке).

## 7) Базовые правила сохранения (Persistence)
- Формат: JSON.
- Версионирование данных.
- Безопасная загрузка (safe load), без падений.
- Атомарная запись (tmp → replace).
- Файлы сохранений: Application.persistentDataPath (Android).

## 8) UI boundary
- UI не зависит от Tanks/Combat/AI/Levels (ни ссылок, ни asmdef-зависимостей).

## 9) Guard checklist
- Inspector reference guards:
  - Обязательные [SerializeField] проверяются в Awake/OnValidate; при отсутствии → Debug.LogError + disable компонента или return.
  - Для обязательных компонентов на том же объекте используем [RequireComponent].
- Event subscription guards:
  - Подписка в OnEnable, отписка в OnDisable; исключить двойные подписки.
- ScriptableObject validation:
  - OnValidate валидирует/клэмпит: speeds > 0, fireRate > 0, damage >= 0, IDs не пустые и уникальные.
- Save/load guards:
  - Try/catch при load; при повреждении → reset к дефолту (без краша).
  - Version field; неизвестные IDs игнорируются; отрицательные значения клэмпятся; атомарная запись.
- Runtime safety:
  - Проверки индексов для списков; null-check перед dot access; не предполагать существование спавн-префабов.

## 10) CI checks
- validate_project_layout.py:
  - Проверяет asmdef: имя файла, префикс, отсутствие Project.*.
  - Проверяет точное соответствие графу зависимостей и Editor-only для Tests.
  - Проверяет наличие ARCH_SNAPSHOT_FOR_CHAT.md и README.txt в модулях.
- Локальный запуск:
  - python Tools/ci/validate_project_layout.py
