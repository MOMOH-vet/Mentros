using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class DamageButton : MonoBehaviour
{

    [SerializeField] private Health Target;
    [SerializeField] private int MyDamage = 10;
    [SerializeField] private Button button;

    private int damage;


    private void Awake()
    {
        damage = MyDamage;
        Debug.Log($"урон {damage} ");

        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }

    private void OnEnable()
    {
         button.onClick.AddListener(DamagCast);
    }

    private void OnDisable()
    {
        if (button == null)
        {
            Debug.Log(" кнопка неназначенна ");
            return;
        }

        button.onClick.RemoveListener(DamagCast);
    }


    public void DamagCast()
    {
        if (Target == null)
        {
            Debug.Log("цель необнаружена");
            return;
        }
       

        Target.TakeDamage(damage);
        Debug.Log($"нанесен {damage} по цели ");
        
    }
}
