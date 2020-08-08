using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    [SerializeField] private Image _slider;
    [SerializeField] private Image _bg;
    
    public void SetDifficulty(float difficulty)
    {
        if (difficulty > 6) // hard
        {
            _bg.color = _slider.color = Color.red;
        }
        else if (difficulty > 3) // normal
        {
            _bg.color = _slider.color = Color.yellow;
        }
        else // easy
        {
            _bg.color = _slider.color = Color.green;
        }
    }
    
}
