using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class StairsTileTest
    {
        StairsTile stairs;

        TileType tileType;
        bool collision;
        Item resource;
        int resourceCount;
        Vector3 position;
        int distance;
        bool visited;
        BaseTile parent;
        int level;

        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("TileTestScene");

            tileType = TileType.STAIRS;
            collision = true;

            GameObject itemObject = new GameObject();
            resource = itemObject.AddComponent<Item>();

            resourceCount = 1;
            position = new Vector3(0.0f, 0.0f, 0.0f);
            distance = 2;
            visited = true;

            GrassTile grassTile = ScriptableObject.CreateInstance<GrassTile>();
            grassTile.SetTileData(
                TileType.GRASS,
                false,
                null,
                0,
                new Vector3(1.0f, 1.0f, 0.0f),
                -9,
                false,
                null,
                0);
            parent = grassTile;

            level = 3;

            stairs = ScriptableObject.CreateInstance<StairsTile>();
            stairs.SetTileData(
                tileType,
                collision,
                resource,
                resourceCount,
                position,
                distance,
                visited,
                parent,
                level);
        }

        [UnityTest]
        public IEnumerator StairsTile_GetTileData_Sprite_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            TileData tileData = new TileData();
            Sprite sprite = Resources.Load<Sprite>("sprites/tiles/stairs");

            stairs.GetTileData(
                Vector3Int.FloorToInt(position),
                null,
                ref tileData);

            Assert.AreEqual(tileData.sprite, sprite);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_type_Pass()
        {
            yield return new WaitForSeconds(0.5f);
            
            Assert.AreEqual(stairs.type, tileType);
        }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_isCollision_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.isCollision, collision);
        }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_Resource_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.resource, resource);
        }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_resourceCount_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.resourceCount, resourceCount);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_position_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.position, position);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_distance_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.distance, distance);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_visited_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.visited, visited);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_parent_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.parent, parent);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_level_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(stairs.level, level);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_upperLevelStairs_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            StairsTile upperLevelStairs = stairs.getUpperLevelStairs();

            Assert.IsNull(upperLevelStairs);
	    }

        [UnityTest]
        public IEnumerator StairsTile_SetTileData_lowerLevelStairs_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            StairsTile lowerLevelStairs = stairs.getLowerLevelStairs();

            Assert.IsNull(lowerLevelStairs);
	    }

        [UnityTest]
        public IEnumerator StairsTile_setUpperLevelStairs_AddedStairs_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            StairsTile upperLevelStairs = ScriptableObject.CreateInstance<StairsTile>();
            upperLevelStairs.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                new Vector3(1.0f, 1.0f, 0.0f),
                -9,
                false,
                null,
                0);

            stairs.setUpperLevelStairs(upperLevelStairs);

            Assert.AreEqual(stairs.getUpperLevelStairs(), upperLevelStairs);
	    }

        [UnityTest]
        public IEnumerator StairsTile_setLowerLevelStairs_AddedStairs_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            StairsTile lowerLevelStairs = ScriptableObject.CreateInstance<StairsTile>();
            lowerLevelStairs.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                new Vector3(1.0f, 1.0f, 1.0f),
                -9,
                false,
                null,
                0);

            stairs.setLowerLevelStairs(lowerLevelStairs);

            Assert.AreEqual(stairs.getLowerLevelStairs(), lowerLevelStairs);
	    }
    }
}
