using UnityEngine;

[RequireComponent(typeof(Turning))]
public class PlayerTurningInput : MonoBehaviour
{
    [SerializeField] private Transform _origin;

    private Turning _turningReference;
    private float _angle;

    private void Awake()
    {
        _turningReference = GetComponent<Turning>();
    }

    private void FixedUpdate()
    {
        _angle = _directionVectorToAngleDegreeFloat(
                Camera.main.ScreenToWorldPoint(Input.mousePosition) - _origin.position);
    }

    private void LateUpdate()
    {
        _turningReference.TurnTo(_angle);
    }

    private float _directionVectorToAngleDegreeFloat(Vector3 direction)
    {
        direction.Normalize();

        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (n < 0)
        {
            n += 360;
        }

        return n;
    }
}
