using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = "(" + slider.value.ToString() + "/" + slider.maxValue.ToString() + ")";
    }
}
