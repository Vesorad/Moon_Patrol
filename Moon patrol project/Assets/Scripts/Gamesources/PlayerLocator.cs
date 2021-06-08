using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocator : MonoBehaviour
{
    PlayerMovement player;
    Ground ground;

    [SerializeField] Slider slider;

    void Start()
    {
        ground = FindObjectOfType<Ground>();
        player = FindObjectOfType<PlayerMovement>();

        SetSliderValues(ground.deltaPosition);
    }

    void Update()
    {
        UpdateSlider();
    }

    void SetSliderValues(float delta)
    {
        slider.minValue = 0;
        slider.maxValue = delta;
        slider.value = slider.maxValue;
    }
    void UpdateSlider()
    {
        slider.value = slider.maxValue - ground.deltaPosition;
    }


}
