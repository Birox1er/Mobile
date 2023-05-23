using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hapyness : MonoBehaviour
{
    [SerializeField] private Slider _hapynessSlider;
    [SerializeField] private int _hapynessProgress = 100;

    void OnNextTurn()
    {
        if (_hapynessSlider.value == 0)
        {
            //gameOver
        }
    }

    void OnSliderChanged()
    {
        _hapynessProgress--;
    }
}
