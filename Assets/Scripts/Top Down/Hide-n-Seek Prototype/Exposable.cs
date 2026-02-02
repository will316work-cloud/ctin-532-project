using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Exposable : MonoBehaviour
{
    [SerializeField] private ExposureType _exposureType;

    public UnityEvent OnDestroy;
    public UnityEvent OnFound;
    public UnityEvent OnNotFound;

    public ExposureType ExposeType { get => _exposureType; }

    public void Find()
    {
        OnFound?.Invoke();
    }

    public void DoNotFound()
    {
        OnNotFound?.Invoke();
    }

    public void DestroyExposable()
    {
        OnDestroy?.Invoke();
    }

    public bool HasLineOfSight(Vector3 lookOrigin, GameObject looker)
    {
        if (!isActiveAndEnabled)
        {
            return false;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(lookOrigin, transform.position - lookOrigin);
        
        for (int i = 1; i < hits.Length; i++)
        {
            GameObject owner = hits[i].transform.gameObject;

            if (owner != looker && owner == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
