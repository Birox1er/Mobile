using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    Chara[] units;
    List<Vector3> ps=new List<Vector3>();
     public void ResetTurn()
    {
        for(int i = 0; i < ps.Count; i++)
        {
            units[i].transform.position = ps[i];
            units[i].GetComponent<Unit>().SetHasMoved(false);
        }
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
