using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;


public class MapGenerator : MonoBehaviour
{
    public int spawnSize = 1;
    public int size = 21;
    //Start is called before the first frame update
    void Start()
    {



        Tiles[,] map = new Tiles[size, size];
        Roads[,] roadMap = new Roads[size, size];

        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {

                map[i, j] = new Tiles();
                map[i, j].setPosCube(i, j);
                map[i, j].setLenghtCube(0.9f, 0.9f, 0.9f);
                map[i, j].setType(map,i,j,size);
                

                if (map[i, j].GetTypess() == Tiles.TileType.Road)
                {
                    Debug.Log("ahhh");
                    roadMap[i, j] = new Roads();
                }
                else { roadMap[i, j] = null; }
                
            }

        }
        setSpawn(map,size);

        connectRoads(map, roadMap);





    }

    public void firstStep(Tiles[,] map, Roads[,] roadMap) {
        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {

                map[i, j] = new Tiles();
                map[i, j].setPosCube(i, j);
                map[i, j].setLenghtCube(0.9f, 0.9f, 0.9f);
                map[i, j].setType(map,i,j,size);

               
            }

        }
    }

    public void connectRoads(Tiles[,] map, Roads[,] roadMap)
    {
        
        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {


                if (map[i, j].GetTypess() == Tiles.TileType.Road)
                {
                    roadMap[i, j] = new Roads();

                    roadMap[i, j].checkRoads(map, i, j);

                    if (roadMap[i, j].isConnected())
                    {
                        map[i, j].setLenghtCube(1.1f, 1.1f, 1.1f);
                    }
                }

            }
        }
    }

    private void setSpawn(Tiles[,] map, int size)
    {

        for (int i = size / 2 - spawnSize; i < size / 2 + 1 + spawnSize; i++)
        {
            for (int j = size / 2 - spawnSize; j < size / 2 + + 1 + spawnSize; j++)
            {
                map[i, j].setTypess(Tiles.TileType.Plain);
                map[i, j].setSkin();
            }
        }
        map[size / 2, size / 2].setTypess(Tiles.TileType.Road);
        map[size / 2, size / 2].setSkin();
    }
    
}


