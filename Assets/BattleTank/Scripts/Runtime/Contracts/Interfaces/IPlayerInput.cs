//«ачем: это единый контракт ввода дл€ игры.
//Tank/Combat будут читать только его и не будут знать, откуда пришЄл ввод (мобилка, клавиатура, бот).
//„то должно быть внутри (по смыслу):
//Move: Vector2 Ч направление движени€
//Aim: Vector2 Ч направление прицеливани€/башни
//FireHeld: bool Ч удержание атаки (дл€ автоогн€)

using UnityEngine;

namespace BattleTank.Scripts.Runtime.Contracts.Interfaces
{
    public interface IPlayerInput 
    {
        Vector2 Move { get; }
        Vector2 Aim { get; }
        bool FireHeld { get; }
    }
}