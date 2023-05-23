using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class populationGrowth : MonoBehaviour
{
    [SerializeField] private Slider _population;
    [SerializeField] private int _populationProgress = 0;

    void OnNextTurn()
    {
        if (_population.value == _population.maxValue)
        {
            //get reward
        }
        if (_population.value == _population.minValue)
        {
            //gameOver
        }
    }

    void PopulationGrowth()
    {
        _populationProgress++; 
    }

}
