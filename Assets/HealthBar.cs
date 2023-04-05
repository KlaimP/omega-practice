using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void Set(float hp)
    {
        slider.value = hp;
    }

    public void MaxHealth(float hp)
    {

        slider.maxValue = hp;
    }
}
