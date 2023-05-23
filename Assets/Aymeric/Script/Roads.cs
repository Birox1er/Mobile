using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour
{
        bool isRoad;
        bool hasPlayer;
        bool isLeftConnected;
        bool isRightConnected;
        bool isUpConnected;
        bool isDownConnected;

    //constructeur

    public Roads()
    {
        isRoad = true;
        hasPlayer = false;
        isLeftConnected = false;
        isRightConnected = false;
        isUpConnected = false;
        isDownConnected = false;
    }



    //getters

    public bool getHasPlayer()
        {
            return hasPlayer;
        }

        public bool getIsLeftConnected()
        {
            return isLeftConnected;
        }

        public bool getIsRightConnected()
        {
            return isRightConnected;
        }

        public bool getIsUpConnected()
        {
            return isUpConnected;
        }

        public bool getIsDownConnected()
        {
            return isDownConnected;
        }
        

        public bool isConnected()
        {
            return (isLeftConnected || isRightConnected || isUpConnected || isDownConnected);
        }

    //setters

    public void setHasPlayer(bool hasPlayer)
        {
            this.hasPlayer = hasPlayer;
        }


        public void setIsLeftConnected(bool isLeftConnected)
        {
            this.isLeftConnected = isLeftConnected;
        }

        public void setIsRightConnected(bool isRightConnected)
        {
            this.isRightConnected = isRightConnected;
        }

        public void setIsUpConnected(bool isUpConnected)
        {
            this.isUpConnected = isUpConnected;
        }

        public void setIsDownConnected(bool isDownConnected)
        {
            this.isDownConnected = isDownConnected;
        }

        //methods

        public void connectLeft()
        {
            isLeftConnected = true;
        }

        public void connectRight()
        {
            isRightConnected = true;
        }

        public void connectUp()
        {
            isUpConnected = true;
        }

        public void connectDown()
        {
            isDownConnected = true;
        }

        public void disconnectLeft()
        {
            isLeftConnected = false;
        }

        public void disconnectRight()
        {
            isRightConnected = false;
        }

        public void disconnectUp()
        {
            isUpConnected = false;
        }

        public void disconnectDown()
        {
            isDownConnected = false;
        }

    //Function


    //vérifier si la case d'acoté est aussi une route sur la map
    
    public void checkRoads(Tiles[,] map, int i, int j)
    {
        if (j > 0)
        {
            if (map[i, j - 1].GetTypess() == Tiles.TileType.Road)
            {
                connectLeft();
            }
            else { disconnectLeft(); }
        }
        
        if (j < map.GetLength(1) - 1)
        {
            if (map[i, j + 1].GetTypess() == Tiles.TileType.Road)
            {
                connectRight();
            }
            else { disconnectRight(); }
        }
        if (i > 0)
        {
            if (map[i - 1, j].GetTypess() == Tiles.TileType.Road)
            {
                connectUp();
            }
        }
        else { disconnectUp(); }
        if (i < map.GetLength(0) - 1)
        {
            if (map[i + 1, j].GetTypess() == Tiles.TileType.Road)
            {
                connectDown();
            }
            else { disconnectDown(); }
        }
        
    }

    public bool IsRoad()
    {
        return isRoad;
    }
}
