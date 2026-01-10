//Зачем: если хочешь изоляцию виртуального стика как источника данных.
//Тогда MobilePlayerInput сможет работать с любым стиком, не зная его конкретный класс.
//Минимальный смысл:
//Value: Vector2 — текущее направление
//IsPressed: bool — палец на стике или нет
//Почему это опционально: можно начать с одного класса VirtualJoystick
//А интерфейс добавить позже, когда понадобится подмена/тестирование.


using UnityEngine;

namespace BattleTank.Scripts.Runtime.Contracts.Interfaces
{

    public interface IStick 
    {
     Vector2 Value { get; }
     bool IsPressed { get; }
    }
}
