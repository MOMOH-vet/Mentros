using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ButtonColor : MonoBehaviour
{
    [SerializeField] private WallpaperColorTarget target;
    [SerializeField] private string ButtonName;
    [SerializeField] private Color AltColor;
    [SerializeField] private ColorMode mode = ColorMode.AltColor;

    private Image img;
    private enum ColorMode
    {
        ButtonColor,
        WhiteColor,
        AltColor
    }
    
    private void Awake()
    {
        img = GetComponent<Image>();        
    }

    public Color GetCollorForCurrentMy()
    {        
        switch (mode)
        {
            case ColorMode.ButtonColor:
                return img.color;

            case ColorMode.WhiteColor:
                return Color.white;

            case ColorMode.AltColor:
                return AltColor;

            default:
                Debug.Log("дефолтный альт цвет");
                mode = ColorMode.ButtonColor;
                return img.color;
        }
    }

    public void AdvanceMode()
    {
        switch (mode)
        {
            case ColorMode.ButtonColor:
                mode = ColorMode.WhiteColor;
                break;

            case ColorMode.WhiteColor:
                mode = ColorMode.AltColor;
                break;

            case ColorMode.AltColor:
                mode = ColorMode.ButtonColor;
                break;

            default:
                Debug.Log("Дефалт альт цвет");
                mode = ColorMode.AltColor;
                break;              
        }
       
    }
    
    public void AplyColor()
    {
              
        if (target == null)
        {
            Debug.Log("неназначен таргет");
            return;
        }

        if (img == null)
        {
            Debug.Log("неназначен имидж");
            return;
        }

                   
        Color colorToApply = GetCollorForCurrentMy();

        target.SetColor(colorToApply);

        AdvanceMode();

        Debug.Log($"[{ButtonName} {colorToApply}]");
    }
    
}
