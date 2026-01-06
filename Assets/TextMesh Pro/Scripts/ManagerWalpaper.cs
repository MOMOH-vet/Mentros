using System.Collections.Generic;
using UnityEngine;

public class ManagerWalpaper : MonoBehaviour
{
    [SerializeField] private List<UnityEngine.UI.Image> Targets;
    [SerializeField] private Color Timage0;
    [SerializeField] private Color Timage1;
    [SerializeField] private Color Timage2;
    [SerializeField] private Modlist mode = Modlist.ThemeColor1;

    private enum Modlist
    {
        ThemeColor0,
        ThemeColor1,
        ThemeColor2
    }
 
    public void Aplly()
    {
        if (Targets == null || Targets.Count == 0) 
        { 
            Debug.Log("targets null"); 
            return; 
        }

        Color selectedColor;

        switch (mode)
        {
            case Modlist.ThemeColor0:
                selectedColor = Timage0;
                mode = Modlist.ThemeColor1;
                break;

            case Modlist.ThemeColor1:
                selectedColor = Timage1;
                mode = Modlist.ThemeColor2;
                break;

            case Modlist.ThemeColor2:
                selectedColor = Timage2;
                mode = Modlist.ThemeColor0;
                break;

            default:
                mode = Modlist.ThemeColor1;
                selectedColor = Timage1;
                break;

        }

        for (int i = 0; i < Targets.Count; i++)
        {
            var target = Targets[i];

            if (target == null)
                continue;

            target.color = selectedColor;
        }

    }

}
