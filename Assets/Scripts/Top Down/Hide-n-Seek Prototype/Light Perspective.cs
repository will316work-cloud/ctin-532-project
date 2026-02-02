using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPerspective : Perspective
{
    [SerializeField] private GameObject _viewRoot;
    [SerializeField] private Light2D _spotlight;
    [SerializeField] private Vector2 _radiusBounds;
    [SerializeField] private Vector2 _angleBounds;

    public float Radius { get => _spotlight ? _spotlight.pointLightOuterRadius : _radiusBounds.y; }
    public float Angle { get => _spotlight ? _spotlight.pointLightOuterAngle : _angleBounds.y; }

    protected override void _toggleCamera(bool isActive)
    {
        _viewRoot.SetActive(isActive);
    }

    protected override bool _seesPoint(Vector3 targetPosition)
    {
        Vector2 detectionVector = targetPosition - _spotlight.transform.position;

        Vector2 spotLightDirection = _spotlight.transform.up;
        Vector2 leftAngleBound = Quaternion.Euler(0, 0, Angle / 2) * spotLightDirection;
        //Vector2 rightAngleBound = Quaternion.Euler(0, 0, - Angle / 2) * spotLightDirection;

        float detectionDot = Vector2.Dot(detectionVector.normalized, spotLightDirection);
        float leftBoundDot = Vector2.Dot(leftAngleBound, spotLightDirection);
        //float rightBoundDot = Vector2.Dot(rightAngleBound, spotLightDirection);

        //Debug.Log($"{name} {detectionVector} {spotLightDirection}");

        return detectionDot >= leftBoundDot && detectionVector.magnitude <= Radius;


        //Debug.Log($"{name} {detectionDot} {leftBoundDot}");

        //return false;
    }

    protected override void _setupPerspective()
    {
        _spotlight.pointLightInnerRadius = _radiusBounds.x;
        _spotlight.pointLightOuterRadius = _radiusBounds.y;
        _spotlight.pointLightInnerAngle = _angleBounds.x;
        _spotlight.pointLightOuterAngle = _angleBounds.y;
    }
}
