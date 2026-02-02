using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSelection : MonoBehaviour
{
    [SerializeField] private List<Perspective> _cameras;

    [Space] public UnityEvent OnTogglePerspectiveOff;
    [Space] public UnityEvent OnTogglePerspectiveOn;

    private int _selectedPerspective = 0;

    private void Start()
    {
        //_cameras = new List<CameraPerspective>(Object.FindObjectsByType<CameraPerspective>(FindObjectsSortMode.InstanceID));

        for (int i = 0; i < _cameras.Count; i++)
        {
            if (_cameras[i] != null)
            {
                _cameras[i].ToggleCamera(i == _selectedPerspective);
            }
        }
    }

    public void SelectPerspective(int index)
    {
        if (index < 0 || index >= _cameras.Count)
        {
            Debug.LogWarning($"Cannot access perspective");
            return;
        }

        /*
        if (_selectedPerspective == index)
        {
            Debug.Log($"Already at index {index}");
            return;
        }
        */

        _cameras[_selectedPerspective].ToggleCamera(false);
        OnTogglePerspectiveOff?.Invoke();

        _selectedPerspective = index;

        _cameras[_selectedPerspective].ToggleCamera(true);
        OnTogglePerspectiveOn?.Invoke();
    }

    public void ShiftPerspective(int steps)
    {
        int newIndex = _selectedPerspective + steps;
        int count = _cameras.Count;

        if (newIndex < 0)
        {
            newIndex = newIndex % count + count;
        }
        else if (newIndex >= count)
        {
            newIndex = newIndex % count;
        }

        SelectPerspective(newIndex);
    }

    [ContextMenu("Increment To The Right")]
    public void IncrementToRight()
    {
        ShiftPerspective(1);
    }

    [ContextMenu("Increment To The Left")]
    public void IncrementToLeft()
    {
        ShiftPerspective(-1);
    }
}
