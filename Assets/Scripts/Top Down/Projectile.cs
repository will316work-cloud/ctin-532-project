using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] public Vector2 _projectileTrajectory;
    [SerializeField] public float _projectileSpeed = 5f;
    [SerializeField] public float _trajectoryDamping = 0f;
    [SerializeField] public float _gravityScale = 1f;

    public UnityEvent OnCollision;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //Launch(_projectileTrajectory, _projectileSpeed);
        _rigidbody.linearDamping = _trajectoryDamping;
        _rigidbody.gravityScale = _gravityScale;

        OnCollision.AddListener(() => Debug.Log("Hit something."));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Invoke();
    }

    public void Launch(Vector2 direction, float speed)
    {
        _projectileTrajectory = direction;
        _projectileSpeed = speed;

        _rigidbody.AddForce(_projectileTrajectory * _projectileSpeed);
    }

    public void Launch(Vector2 direction)
    {
        Launch(direction, _projectileSpeed);
    }
}
