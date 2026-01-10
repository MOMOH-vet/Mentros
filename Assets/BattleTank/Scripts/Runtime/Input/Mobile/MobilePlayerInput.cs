//Зачем: это “адаптер”, который объединяет два стика в один контракт IPlayerInput.
//using Codice.Client.BaseCommands.Import;
//Логика(по смыслу):
//Move берём с левого стика (left.Value)
//Aim берём с правого стика (right.Value)
//FireHeld берём с правого стика (right.IsPressed) — “удержание для автоогня”
//Почему так: Tank / Combat дальше работают с одним объектом IPlayerInput, а UI-виртуальные стики остаются деталями реализации.
//Тонкий момент про Aim: обычно в бою удобно, чтобы при отпускании правого стика башня просто переставала обновляться, а не сбрасывалась в (0,0). Это можно сделать либо:
//в MobilePlayerInput(хранить “последний ненулевой Aim”), либо
//в логике башни (если Aim около нуля — не вращать).
//Мы выберем один вариант, когда дойдём до проверки в сцене.

using UnityEngine;

namespace BattleTank.Scripts.Runtime.Input.Mobile
{
    public class MobilePlayerInput : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
