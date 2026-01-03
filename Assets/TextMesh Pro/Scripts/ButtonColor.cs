using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ButtonColor : MonoBehaviour
{
    [SerializeField] private WallpaperColorTarget target;
    [SerializeField] private string ButtonName;

    private Image img;
   
    

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    
    public void AplyMyColor()
    {
        target.SetColor(img.color);

        if (target == null)
        {
            Debug.Log("неназначен таргет");
        }

        if (ButtonName == null)
        {
            Debug.Log("неназначена  кнопка");
        }
        Debug.Log($"[{ButtonName} {img.color}]");
    }
    
}
