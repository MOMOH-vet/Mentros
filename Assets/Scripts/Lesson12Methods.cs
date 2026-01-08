using UnityEngine;

public class Lesson12Methods : MonoBehaviour
{
  
    public void runsum()
    {
       int result1 = Sum (2, 3);
       int result2 = Sum (-3, 8);

       Debug.Log ($"{result1}");
       Debug.Log ($"{result2}");
    }

    private int Sum(int v1, int v2)
    {
       return v1 + v2;
    }

    public void runodd()
    {
        Debug.Log($"{IsOdd(1)}");
        Debug.Log($"{IsOdd(2)}");
        Debug.Log($"{IsOdd(13)}");
    }

    private bool IsOdd(int x)
    {
        return x % 2 == 1;
    }

    public void runnormalize()
    {
        float n1 = Normalize01(5f,0f,0f);
        float n2 = Normalize01(15f, 0f, 0f);
        float n3 = Normalize01(10f, 10f, 10f);

        Debug.Log($"{n1}");
        Debug.Log($"{n2}");
        Debug.Log($"{n3}");

    }
    private float Normalize01(float value, float min, float max)
    {
        if (max == min)
        {
            Debug.LogWarning("normalize01: max == min, деление на 0 невозможно");
            return 0f;
        }

        return (value - min) / (max - min);
    }

    public void RunDiscount()
    {
        float p1 = ApplyDiscount(20, 3f);
        Debug.Log($"{p1}");
    }
    private float ApplyDiscount(float price, float percent)
    {
        if (price < 0f)
        {
            
            return price;
        }

        if (percent < 0f || percent > 100f)
        {
           
            return price;
        }

        return price * (1f - percent / 100f);

    }





}
