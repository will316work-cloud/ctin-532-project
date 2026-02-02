using UnityEngine;

public class ExposableList : MonoBehaviour
{
    [SerializeField] private Exposable[] _exposables;

    public Exposable[] Exposables { get => _exposables; }

    public bool[] _isSpotted;

    private void Awake()
    {
        _isSpotted = new bool[_exposables.Length];
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _exposables.Length; i++)
        {
            if (_isSpotted[i])
            {
                _exposables[i].Find();
            }
            else
            {
                _exposables[i].DoNotFound();
            }
        }

        _isSpotted = new bool[_exposables.Length];
    }
}
