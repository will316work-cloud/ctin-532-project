using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Perspective))]
public class Detector : MonoBehaviour
{
    [SerializeField] private GameObject _owner;
    [SerializeField] private ExposureType[] _exposureTypeFilter;
    [SerializeField] private ActionsOnExposureType[] _actions;

    private Perspective _perspective;

    private static ExposableList _exposableList;

    private void Awake()
    {
        if (_owner == null)
        {
            _owner = gameObject;
        }

        _perspective = GetComponent<Perspective>();

        if (_exposableList == null)
        {
            _exposableList = FindFirstObjectByType<ExposableList>();
        }
    }

    private void Update()
    {
        Dictionary<ExposureType, bool> spottedTypes = new Dictionary<ExposureType, bool>();

        for (int i = 0; i < _exposableList.Exposables.Length; i++)
        {
            Exposable exposable = _exposableList.Exposables[i];

            if (!spottedTypes.ContainsKey(exposable.ExposeType))
            {
                spottedTypes[exposable.ExposeType] = false;
            }

            bool spotted = exposable.gameObject != _owner
                            && _visibleToDetector(exposable)
                            && _perspective.SeesPoint(exposable.transform.position)
                            && exposable.HasLineOfSight(_perspective.transform.position, _owner);

            //Debug.Log($"{name} {exposable.gameObject != _owner} {_visibleToDetector(exposable)} {_perspective.SeesPoint(exposable.transform.position)} {exposable.HasLineOfSight(_perspective.transform.position, _owner)}");

            if (spotted)
            {
                spottedTypes[exposable.ExposeType] = true;

                _exposableList._isSpotted[i] = true;
            }
        }

        foreach (ActionsOnExposureType action in _actions)
        {
            foreach (KeyValuePair<ExposureType, bool> pair in spottedTypes)
            {
                action.DoActionsOnExposed(pair.Value, pair.Key);
            }
        }
    }

    private bool _visibleToDetector(Exposable exposable)
    {
        foreach (ExposureType type in _exposureTypeFilter)
        {
            if (exposable.ExposeType == type)
            {
                return false;
            }
        }

        return true;
    }
}
