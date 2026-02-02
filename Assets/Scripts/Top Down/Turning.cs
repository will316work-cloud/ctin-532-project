using UnityEngine;
using UnityEngine.Events;

public class Turning : MonoBehaviour
{
    [SerializeField] private Transform[] _rotatingTransforms;
    [Space] [SerializeField] private UnityEvent<float> OnTurn;

    public void TurnTo(float angle)
    {
        foreach (Transform t in _rotatingTransforms)
        {
            _turnTransformTo(t, angle);
        }

        OnTurn?.Invoke(angle);
    }

    private void _turnTransformTo(Transform transform, float angle)
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = angle;

        transform.rotation = Quaternion.Euler(euler);
    }
}
