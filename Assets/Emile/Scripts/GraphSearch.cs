using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GraphSearch
{
    public static BFSResult BFSGetRange(HexGrid grid, Vector3Int startPoint, int movPoint)
    {
        Dictionary<Vector3Int, Vector3Int?> visitedNode = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costSoFar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();
        nodesToVisitQueue.Enqueue(startPoint);
        costSoFar.Add(startPoint, 0);
        visitedNode.Add(startPoint, null);
        while (nodesToVisitQueue.Count > 0)
        {
            
            Vector3Int currentNode = nodesToVisitQueue.Dequeue();
            Debug.Log(currentNode);
            grid.GetNeighbours(currentNode).ForEach(x => Debug.Log(x));
            foreach (Vector3Int neighPos in grid.GetNeighbours(currentNode))
            {
                
                if (grid.GetTileAt(neighPos).IsObstacle())
                {
                    continue;
                }
                int nodeCost = grid.GetTileAt(neighPos).GetCost();
                int currentCost = costSoFar[currentNode];
                int newCost = currentCost + nodeCost;
                if (movPoint - newCost >= 0 || (movPoint - currentCost > 0 && nodeCost == 3)||movPoint==100)
                {
                    if (!visitedNode.ContainsKey(neighPos))
                    {
                        visitedNode[neighPos] = currentNode;
                        costSoFar[neighPos] = newCost;
                        nodesToVisitQueue.Enqueue(neighPos);
                    }
                    else if (costSoFar[neighPos] > newCost)
                    {
                        costSoFar[neighPos] = newCost;
                        visitedNode[neighPos] = currentNode;
                    }
                }
            }
        }
        return new BFSResult { visitedNodeD = visitedNode };
    }

   

    internal static List<Vector3Int> BFSGetRange(Vector3Int Current, Dictionary<Vector3Int, Vector3Int?> visitedNodeD)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(Current);
        while (visitedNodeD[Current] != null)
        {
            path.Add(visitedNodeD[Current].Value);
            Current = visitedNodeD[Current].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList();
    }
    public static List<Vector3Int> BFSGetRangeEnnemi(Vector3Int Current, Dictionary<Vector3Int, Vector3Int?> visitedNodeD)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(Current);
        while (visitedNodeD[Current] != null)
        {
            path.Add(visitedNodeD[Current].Value);
            Current = visitedNodeD[Current].Value;
        }
        return path.Skip(1).ToList();
    }
    public static BFSResult BFSGetAttack(HexGrid grid, Vector3Int startPoint, int atkRange)
    {
        Dictionary<Vector3Int, Vector3Int?> visitedNode = new Dictionary<Vector3Int, Vector3Int?>();
        Dictionary<Vector3Int, int> costSoFar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();
        nodesToVisitQueue.Enqueue(startPoint);
        costSoFar.Add(startPoint, 0);
        visitedNode.Add(startPoint, null);
        while (nodesToVisitQueue.Count > 0)
        {
            Vector3Int currentNode = nodesToVisitQueue.Dequeue();
            foreach (Vector3Int neighPos in grid.GetNeighbours(currentNode))
            {
                if (grid.GetTileAt(neighPos).IsObstacle())
                {
                    continue;
                }
                int nodeCost = grid.GetTileAt(neighPos).GetCost();
                int currentCost = costSoFar[currentNode];
                int newCost = currentCost + nodeCost;
                if (atkRange - newCost >= 0)
                {
                    if (!visitedNode.ContainsKey(neighPos))
                    {
                        visitedNode[neighPos] = currentNode;
                        costSoFar[neighPos] = newCost;
                        nodesToVisitQueue.Enqueue(neighPos);
                    }
                    else if (costSoFar[neighPos] > newCost)
                    {
                        costSoFar[neighPos] = newCost;
                        visitedNode[neighPos] = currentNode;
                    }
                }
            }
        }
        return new BFSResult { visitedNodeD = visitedNode };
    }
    internal static List<Vector3Int> BFSGetAttack(Vector3Int Current, Dictionary<Vector3Int, Vector3Int?> visitedNodeD)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(Current);
        Debug.Log(Current);
        while (visitedNodeD[Current] != null)
        {
            path.Add(visitedNodeD[Current].Value);
            Current = visitedNodeD[Current].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList();
    }
}
    public struct BFSResult
    {
        public Dictionary<Vector3Int, Vector3Int?> visitedNodeD;

        public List<Vector3Int> GetPathTo(Vector3Int destination)
        {
            Debug.Log(!visitedNodeD.ContainsKey(destination));
            if (!visitedNodeD.ContainsKey(destination))
            {
            Debug.Log("Path not found");
            return new List<Vector3Int>();
            }
            Debug.Log("Path found");
            return GraphSearch.BFSGetRange(destination, visitedNodeD);
        }
    public List<Vector3Int> GetPathToEnemy(Vector3Int destination)
    {
        Debug.Log(!visitedNodeD.ContainsKey(destination));
        if (!visitedNodeD.ContainsKey(destination))
        {
            Debug.Log("Path not found");
            return new List<Vector3Int>();
        }
        Debug.Log("Path found");
        return GraphSearch.BFSGetRangeEnnemi(destination, visitedNodeD);
    }
    public bool IsHexPosInRange(Vector3Int pos)
        {
            return visitedNodeD.ContainsKey(pos);
        }
        public IEnumerable<Vector3Int> GetRangePos() => visitedNodeD.Keys;
    }

