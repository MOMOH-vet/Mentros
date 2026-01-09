using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 1000;
    private int current;
  

    private void Awake()
    {
        current = maxHP;
        Debug.Log($"hp {current} health");
    }

    public void TakeDamage(int amount)
    {
        current -= amount;
        if( current < 0)  current = 0;
        Debug.Log($"Damage {amount}. HP = {current}");
    }

}
 