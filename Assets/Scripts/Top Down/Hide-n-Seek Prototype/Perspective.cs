using UnityEngine;

public abstract class Perspective : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public void ToggleCamera(bool isActive)
    {
        _setupPerspective();
        _toggleCamera(isActive);
        _camera.gameObject.SetActive(isActive);
    }

    public bool SeesPoint(Vector3 targetPosition)
    {
        _setupPerspective();
        return _seesPoint(targetPosition);
    }

    protected abstract void _toggleCamera(bool isActive);

    protected abstract bool _seesPoint(Vector3 targtPosition);

    protected abstract void _setupPerspective();
}
