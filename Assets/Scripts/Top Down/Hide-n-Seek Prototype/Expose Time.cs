using UnityEngine;
using UnityEngine.Events;

public class ExposeTime : MonoBehaviour
{
    [SerializeField] private float _maxTime;
    [SerializeField] private float _decreaseTimeScale = 1f;
    public UnityEvent OnReachTime;

    [SerializeField] private float _currentTime;
    private bool _increasingTime;

    public bool IncreasingTime { get => _increasingTime; set => _increasingTime = value; }

    private void Update()
    {
        if (_increasingTime && _currentTime < _maxTime)
        {
            float previousTime = _currentTime;

            _currentTime += Time.deltaTime;

            if (_currentTime >= _maxTime && previousTime < _maxTime)
            {
                OnReachTime?.Invoke();
            }
        }
        else if (!_increasingTime && _currentTime >= 0)
        {
            _currentTime -= Time.deltaTime * _decreaseTimeScale;
        }
    }
}
