using UnityEngine;

public class ConstantMover : MonoBehaviour
{
    [SerializeField] private Vector2 pravo = Vector2.right;
    [SerializeField] private float speed = 1f;

    private float timer;

    private void Update()
    {
        Vector3 step = (Vector3)pravo.normalized * speed * Time.deltaTime;
        transform.position += step;       

        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Debug.Log($"{transform.position}");
            timer = 0f;
        }
    }
}
   
