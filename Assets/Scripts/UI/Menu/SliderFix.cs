using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFix : MonoBehaviour
{
    // Я этот скрипт сделал чисто из за того, что слайдеры в менюшке не хотели работать(

    [SerializeField] private Slider slider;
    [SerializeField] private Transform sliderStart;
    [SerializeField] private Transform sliderEnd;

    [SerializeField] private float mousePositionMultiplier = 1;

    private bool onMouse = false;

    private void OnMouseEnter()
    {
        onMouse = true;
    }

    private void OnMouseExit()
    {
        onMouse = false;
    }

    private void Update()
    {
        if (onMouse && Input.GetMouseButton(0))
        {
            slider.value = slider.maxValue / (sliderEnd.position.x - sliderStart.position.x) * (Camera.main.ScreenToWorldPoint(Input.mousePosition).x * mousePositionMultiplier - sliderStart.position.x);
        }
    }
}
