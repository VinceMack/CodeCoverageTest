using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Gather : LaborOrder_Base
{
    // constructor
    public LaborOrder_Gather(Item target)
    {
        laborType = LaborType.Gather;
        timeToComplete = 1f;
        location = Vector3Int.FloorToInt(target.transform.position);
    }

    public override IEnumerator Execute(Pawn pawn)
    {
        pawn.path.Clear();

        Chest chest = GlobalStorage.GetClosestChest(pawn.transform.position);
        BaseTile targetTile = GridManager.GetTile(location);
        BaseTile currentTile = (BaseTile)pawn.GetPawnTileFromTilemap();

        Item resource = targetTile.resource;

        if (resource == null)
        {
            Debug.LogWarning("Resource is null. Aborting.");
            yield break;
        }

        if (chest == null)
        {
            Debug.LogWarning("Chest is null. Aborting.");
            yield break;
        }

        chest.AddItem(resource.itemName);
        UnityEngine.Object.Destroy(targetTile.resource.gameObject);
        targetTile.resource = null;

        if (targetTile == null || currentTile == null)
        {
            Debug.LogWarning("Target or current tile is null. Aborting.");
            yield break;
        }

        Vector3Int targetPosition = Vector3Int.FloorToInt(chest.transform.position);
        Vector3Int currentPosition = Vector3Int.FloorToInt(pawn.transform.position);

        int targetLevel = targetPosition.x / GridManager.LEVEL_WIDTH;
        int currentLevel = currentPosition.x / GridManager.LEVEL_WIDTH;

        while (!IsAdjacent(currentPosition, targetPosition))
        {
            if (currentLevel != targetLevel)
            {
                StairsTile stairs;
                Vector3 levelChangeStairsPosition;

                if (currentLevel < targetLevel)
                {
                    stairs = GridManager.mapLevels[currentLevel].getDescendingStairs(currentPosition);
                    if (stairs == null)
                    {
                        Debug.LogWarning("Descending stairs tile is null. Aborting.");
                        yield break;
                    }
                    levelChangeStairsPosition = stairs.getLowerLevelStairs().position;
                }
                else
                {
                    stairs = GridManager.mapLevels[currentLevel].getAscendingStairs(currentPosition);
                    if (stairs == null)
                    {
                        Debug.LogWarning("Ascending stairs tile is null. Aborting.");
                        yield break;
                    }
                    levelChangeStairsPosition = stairs.getUpperLevelStairs().position;
                }

                Vector3Int stairsPosition = Vector3Int.FloorToInt(stairs.position);

                if (currentPosition == stairsPosition)
                {
                    pawn.transform.position = levelChangeStairsPosition;
                    currentLevel = (int)levelChangeStairsPosition.x / GridManager.LEVEL_WIDTH;
                    continue;
                }
                else
                {
                    pawn.path = PathfindingManager.GetPath(currentPosition, stairsPosition, currentLevel, true);
                }
            }
            else
            {
                pawn.path = PathfindingManager.GetPath(currentPosition, targetPosition, currentLevel, false);
            }

            if (pawn.path.Count == 0)
            {
                Debug.Log("Path is empty. Aborting.");
                yield break;
            }

            pawn.currentPathExecution = pawn.StartCoroutine(pawn.TakePath());
            yield return pawn.currentPathExecution;
            pawn.currentPathExecution = null;
            currentPosition = Vector3Int.FloorToInt(pawn.transform.position);
        }
    }

    private bool IsAdjacent(Vector3Int currentPosition, Vector3Int targetPosition)
    {
        Vector3Int[] adjacentPositions = new Vector3Int[]
        {
            targetPosition,
            targetPosition + Vector3Int.up,
            targetPosition + Vector3Int.down,
            targetPosition + Vector3Int.left,
            targetPosition + Vector3Int.right,
            targetPosition + Vector3Int.up + Vector3Int.left,
            targetPosition + Vector3Int.up + Vector3Int.right,
            targetPosition + Vector3Int.down + Vector3Int.left,
            targetPosition + Vector3Int.down + Vector3Int.right,
        };

        foreach (Vector3Int adjacentPosition in adjacentPositions)
        {
            if (currentPosition == adjacentPosition)
            {
                return true;
            }
        }

        return false;
    }
}




