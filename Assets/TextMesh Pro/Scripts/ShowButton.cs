using UnityEngine;
public class ShowButton : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private string _buttonName;
    public void ShowPanel()
    {
        if (_panel == null)
        {
            Debug.Log("панель неназначенна");
            return;
        }

        _panel.SetActive(!_panel.activeSelf);

       Debug.Log($" {_buttonName} {_panel.activeSelf} ");
    }
}