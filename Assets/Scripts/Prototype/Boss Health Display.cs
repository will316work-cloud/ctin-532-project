using UnityEngine;
using TMPro;

public class BossHealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthDisplay;

    public void UpdateDisplay(int health)
    {
        if (health <= 0)
        {
            _healthDisplay.text = "Boss Defeated";
        }
        else
        {
            _healthDisplay.text = "" + health;
        }
    }
}
