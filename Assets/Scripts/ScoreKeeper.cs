using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Singleton;

    [SerializeField] private Slider ratingSlider;
    private int _newValue;
    private float _sliderValue;

    private float _timeToFill = 5f;
    private float _timer;
    private bool _startSliding = false;

    public GameObject gameOverUI;

    
    // Start is called before the first frame update
    void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        else
            Singleton = this;
    }

    private void Update()
    {
        if (ratingSlider.value <= 0f && !GameController.isGameOver)
        {
            GameController.singleton.GameOver();
        }
        
        if (_timer > _timeToFill)
        {
            _startSliding = false;
            _timer = 0f;
            _sliderValue = ratingSlider.value;
            _newValue = 0;
        }

        if (_startSliding)
        {
            _timer += Time.deltaTime;
            ratingSlider.value = Mathf.Lerp(ratingSlider.value, _sliderValue + _newValue, _timer/_timeToFill);
        }
    }

    // Update is called once per frame
    public void ModScore(int modifier)
    {
        _timer = 0f;
        _newValue += modifier;
        _sliderValue = ratingSlider.value;
        _startSliding = true;
    }
}
