using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            foreach (Vector3Int neighPos in grid.GetNeighbours(currentNode))
            {
                if (grid.GetTileAt(neighPos).IsObstacle())
                {
                    continue;
                }
                int nodeCost = grid.GetTileAt(neighPos).GetCost();
                int currentCost = costSoFar[currentNode];
                int newCost = currentCost + nodeCost;
                if (movPoint - newCost >= 0 || (movPoint - currentCost > 0 && nodeCost == 3))
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
}
    public struct BFSResult
    {
        public Dictionary<Vector3Int, Vector3Int?> visitedNodeD;

        public List<Vector3Int> GetPathTo(Vector3Int destination)
        {
            if (!visitedNodeD.ContainsKey(destination))
            {
                return new List<Vector3Int>();
            }
            return GraphSearch.BFSGetRange(destination, visitedNodeD);
        }
        public bool IsHexPosInRange(Vector3Int pos)
        {
            return visitedNodeD.ContainsKey(pos);
        }
        public IEnumerable<Vector3Int> GetRangePos() => visitedNodeD.Keys;
    }

