using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaHexMovement : MonoBehaviour
{

    //the speed of the unit
    public float speed = 0.3f;

    //search all the objects with the tag "Unit" and put them in a list
    private List<GameObject> units = new List<GameObject>();

    //move to the closest unit
    private void MoveToClosestUnit()
    {
        //get the closest unit
        GameObject closestUnit = GetClosestUnit();

        //get the closest hex
        HexCoord closestHex = GetClosestHex(closestUnit.transform.position);

        //move to the closest hex
        MoveTo(closestHex);
    }

    //get the closest unit
    private GameObject GetClosestUnit()
    {
        //get all the units
        units = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));

        //get the closest unit
        GameObject closestUnit = units[0];
        float closestDistance = Vector3.Distance(transform.position, closestUnit.transform.position);
        foreach (GameObject unit in units)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);
            if (distance < closestDistance)
            {
                closestUnit = unit;
                closestDistance = distance;
            }
        }

        //return the closest unit
        return closestUnit;
    }



    //get the closest hex to the closest unit (not the hex where the unit is)
    private HexCoord GetClosestHex(Vector3 position)
    {
        //get all the hexes
        List<HexCoord> hexes = new List<HexCoord>(FindObjectsOfType<HexCoord>());

        //get the closest hex
        HexCoord closestHex = hexes[0];
        float closestDistance = Vector3.Distance(position, closestHex.transform.position);
        foreach (HexCoord hex in hexes)
        {
            float distance = Vector3.Distance(position, hex.transform.position);
            if (distance < closestDistance)
            {
                closestHex = hex;
                closestDistance = distance;
            }
        }

        //return the closest hex
        return closestHex;
    }
    //move slowly to the closest hex
    private void MoveTo(HexCoord closestHex)
    {
        //get the closest hex position
        Vector3 closestHexPosition = closestHex.transform.position;

        //move to the closest hex
        transform.position = Vector3.MoveTowards(transform.position, closestHexPosition, speed);
    }


    //make move to the closest unit 
    private void Update()
    {
        MoveToClosestUnit();
    }








}
