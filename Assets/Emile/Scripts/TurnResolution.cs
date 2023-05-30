using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnResolution : MonoBehaviour
{
    private Chara[] all;
    
    void OnNextTuren()
    {
        all = FindObjectsOfType<Chara>();
        TriInsertion(all);
        for(int i = 0; i < all.Length; i++)
        {
            List<Chara> inRange = all[i].CheckInRange();
            if (inRange != null||inRange.Count!=0)
            {
                int cible = (int)Random.Range(0, inRange.Count);
                if (all[i]._isUltOn)
                {
                    all[i].Ult();
                }
                else
                {
                    all[i].Attack(inRange[cible]);
                }
            }
        }
    }
    public static void TriInsertion(Chara[] sortArray)
    {
        int tmp;
        for (int i = 1; i < sortArray.Length; ++i)
        {
            tmp = sortArray[i].Prio;
            int index = i;
            while (index > 0 && tmp < sortArray[index - 1].Prio)
            {
                Chara x = sortArray[index];
                sortArray[index] = sortArray[index-1];
                sortArray[index-1] = x; ;
                --index;
            }
        }
    }
    void Update()
    {
    }
}
