using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPerspective : Perspective
{
    private Camera _camera;

    public Camera Camera { get => _camera; }

    protected override void _toggleCamera(bool isActive)
    {
        _camera.gameObject.SetActive(isActive);
    }

    protected override bool _seesPoint(Vector3 targetPosition)
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(targetPosition);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void _setupPerspective()
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
        }
    }
}
