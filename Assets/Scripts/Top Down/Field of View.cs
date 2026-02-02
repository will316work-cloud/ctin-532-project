using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private Vector3 _origin = Vector3.zero;
    [SerializeField] private float _fov = 90f;
    [SerializeField] private float _startingAngle = 0f;
    [SerializeField] private float _viewDistance = 5f;
    [SerializeField] private int _rayCount = 25;
    [SerializeField] private LayerMask _layerMask;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void LateUpdate()
    {
        float angle = _startingAngle;
        float angleIncrease = _fov / _rayCount;

        Vector3[] vertices = new Vector3[_rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[_rayCount * 3];

        vertices[0] = _origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= _rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, _degreeAngleToDirectionVector(angle), _viewDistance, _layerMask);

            if (raycastHit2D.collider == null)
            {
                vertex = _origin + _degreeAngleToDirectionVector(angle) * _viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        _origin = origin;
    }

    public void SetStartingAngle(Vector3 direction)
    {
        _startingAngle = _directionVectorToAngleDegreeFloat(direction) - _fov / 2f;
    }

    private Vector3 _degreeAngleToDirectionVector(float angle)
    {
        float angleRad = angle * Mathf.PI / 180f;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
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
