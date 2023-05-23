using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tiles : MonoBehaviour
{

    public enum TileType
    {
        
        Plain = 0,
        Forest = 1,
        Mountain = 2,
        Water = 3,
        Building = 4,
        Road = 5,
        Volcano = 6,
        Lava = 7
       
    }

    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //return a random
    TileType type;

    int random = Random.Range(1, 100);


    public void setType(Tiles[,] map, int i, int j, int size)
    {

        if (i <= (size / 2))
        {

            waterLand();
        }

        else if (i > (size / 2) && j > (size / 2))
        {

            lavaLand();
        }

        else
        {
            
            rockyLand();        
        }

        setSkin();
    }
    
    private void lavaLand()
    {
        
        if (random <= 5)
        {
            type = TileType.Volcano;
        }
        
        else if (random <= 60)
        {
            type = TileType.Mountain;
        }
        
        else
        {
            type = TileType.Plain;
        }


    }
    
    private void waterLand()
    {
        //random between 1 and 100
        

        if (random <= 10)
        {
            
            type = TileType.Mountain;
        }
        
        else if (random <= 25)
        {
            type = TileType.Water;
        }
        
        else if (random <= 50) {
            type = TileType.Forest;
        } 
        
        else
        {
            type = TileType.Plain;
        }

    }

    private void rockyLand()
    {

        if (random <= 35)
        {
            type = TileType.Mountain;
        }

        else if (random <= 70)
        {
            type = TileType.Forest;
        }

        else
        {
            type = TileType.Plain;
        }
    }


    
    

    public void setPosCube(float x, float y)
    {
        cube.transform.position = new Vector3(x, y, 0);
    }

    public void setLenghtCube(float x, float y, float z)
    {
        cube.transform.localScale = new Vector3(x, y, z);
    }


    public void setSkin()
    {
        
        switch (type)
        {
            case TileType.Road:
                cube.GetComponent<Renderer>().material.color = Color.black;
                break;
            case TileType.Plain:
                cube.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case TileType.Forest:
                cube.GetComponent<Renderer>().material.color = Color.green;
                break;
            case TileType.Mountain:
                cube.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case TileType.Water:
                cube.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case TileType.Building:
                cube.GetComponent<Renderer>().material.color = Color.white;
                break;
            case TileType.Volcano:
                cube.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case TileType.Lava:
                cube.GetComponent<Renderer>().material.color = Color.red;
                break;
        }
    }

    public TileType GetTypess()
    {
        return type;
    }

    public void setTypess(TileType type)
    {
        this.type = type;
    }

}










