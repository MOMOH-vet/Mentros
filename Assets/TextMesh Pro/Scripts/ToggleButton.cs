using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

  
    private int ClickCont;
    public void ShowPanel()
    {
        if (_panel == null)
        {
            Debug.Log("панель неназначенна");
            return;
        }

        ClickCont++;
        int mode = ClickCont % 3;

        if (mode == 1)
        {
            _panel.SetActive(false);
        }
        else if (mode == 2)
        {
            _panel.SetActive(true);
        }
        else
        {
            Debug.Log("облом");
        }

    }
}