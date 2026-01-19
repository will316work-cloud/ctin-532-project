using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnReachZeroHealth;

    public void SetHealth(int newHealth)
    {
        if (newHealth <= 0)
        {
            if (_health > 0)
            {
                OnReachZeroHealth?.Invoke();
            }

            _health = 0;
        }
        else
        {
            _health = newHealth;
        }

        OnHealthChanged?.Invoke(_health);
    }

    public void IncrementHealth(int healthIncrement)
    {
        SetHealth(_health + healthIncrement);
    }
}
