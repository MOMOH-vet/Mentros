using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugTraining11 : MonoBehaviour
{
    [SerializeField] private Image ImageColor;
    [SerializeField] private List<Image> Limage;

    
    public void TestNull()
    {

        if (ImageColor == null)
        {
            Debug.Log("Null");
            return;
        }

        ImageColor.color = Color.white;
        Debug.Log("TestNull done");

    }

    public void TestIndex()
    {
        if (Limage == null || Limage.Count <= 8 )
        {
            Debug.Log("Index");
            return;
        }

        var x = Limage[8];
        Debug.Log("TestIndex done");
    }

    public void TestParse()
    {
        string s = "abc";
        if (!int.TryParse(s, out int n ))
        {
            Debug.Log("Parse");
            return;
        }
        
        n = int.Parse(s);

        Debug.Log("TestParse done");
    }
}
