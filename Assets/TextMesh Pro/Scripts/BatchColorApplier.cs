using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BatchColorApplier : MonoBehaviour
{
    [SerializeField] private List<Image> listImage;
    [SerializeField] private Color _colorToApply;
    [SerializeField] private int maxApply;

    public void ApplyShtamp()
    {
        if (listImage == null || listImage.Count == 0) { Debug.Log("Error"); return; }      

        int applied = 0;
        int skipied = 0;
        int index = 0;

        foreach (var img in listImage)
        {
            if (img == null)
            {
                skipied++;
                Debug.Log($"skip, index = {index}");
                index++;
                continue;

            }

            img.color = _colorToApply;
            applied++;

            if (maxApply > 0 && applied >= maxApply)
                break;
            index++;
            
        }

        Debug.Log($"applied={applied} skipied={skipied} total={listImage.Count}");

    }      
    

   
    
   
}
