using UnityEngine;

public class Weakspot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{name} triggered with {collision.name}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        Debug.Log($"{name} collided with {collision.gameObject.name}");
    }
}
