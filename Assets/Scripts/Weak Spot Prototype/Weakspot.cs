using UnityEngine;
using UnityEngine.Events;

public class Weakspot : MonoBehaviour
{
    public UnityEvent OnGetHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{name} triggered with {collision.name}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;

        float horizontalDot = Vector2.Dot(Vector2.right, normal);
        float verticalDot = Vector2.Dot(Vector2.up, normal);
        string direction = "";

        if (Mathf.Abs(horizontalDot) >= Mathf.Abs(verticalDot))
        {
            direction = horizontalDot <= 0 ? "the right" : "the left";
        }
        else
        {
            direction = verticalDot <= 0 ? "upwards" : "downwards";
        }

        Debug.Log($"{name} collided with {collision.gameObject.name} from {direction}.");

        OnGetHit?.Invoke();
    }
}
