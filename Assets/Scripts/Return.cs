using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    Chara[] units;
    List<Vector3> ps=new List<Vector3>();
    int nbrReset = 0;
     public void ResetTurn()
    {
        nbrReset++;
        for(int i = 0; i < ps.Count; i++)
        {
            units[i].transform.position = ps[i];
            units[i].GetComponent<Unit>().SetHasMoved(false);
        }
        if (nbrReset == 1)
        {
            ValidateSuccess.ValidateBackInTime();
        }
        Social.ReportProgress("CgkIsfzlyYQEEAIQAw", 100.0f, (bool succes) => {
            succes = (nbrReset == 10);
        });
    }
    public void SaveTurn()
    {
        ps.Clear();
        units = FindObjectsOfType<Chara>();
        foreach(Chara unit in units)
        {
            ps.Add(unit.transform.position);
        }
    }
}
