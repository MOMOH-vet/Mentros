using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ButtonColor : MonoBehaviour
{
    [SerializeField] private WallpaperColorTarget target;
    [SerializeField] private string ButtonName;
    [SerializeField] private Color AltColor;
    


    private Image img;
    private int _clickTick;
    private Color _color;
    
    private void Awake()
    {
        img = GetComponent<Image>();
        _color = Color.white;       
    }
    
    public void AplyColor()
    {
              
        if (target == null)
        {
            Debug.Log("неназначен таргет");
            return;
        }

        _clickTick++;
        int mode = _clickTick % 3;
        
        Color colorToApply;
        if (mode == 1)
        {
            colorToApply = img.color;
            Debug.Log("применен альт цвет");
            
        }
        else if (mode == 2)
        {
            colorToApply = _color;
            Debug.Log("применен белый цвет");
        }
        else
        {
            colorToApply = AltColor;
            Debug.Log("Применен цвет конпки");
        }
        

            target.SetColor(colorToApply);
        
 

        Debug.Log($"[{ButtonName} {colorToApply} {_clickTick}]");
    }
    
}
