using UnityEngine;
using UnityEngine.UI;

public class WallpaperColorTarget : MonoBehaviour
{
    [SerializeField] private Image wallpaper;

    internal void SetColor(Color color)
    {
        wallpaper.color = color;
    }
}
