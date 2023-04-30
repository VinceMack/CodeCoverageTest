using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class BaseTileTest
    {
        BaseTile baseTile;

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

            tileType = TileType.GENERIC;
            collision = true;

            resource = null;
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

            baseTile = ScriptableObject.CreateInstance<BaseTile>();
            baseTile.SetTileData(
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
        public IEnumerator BaseTile_GetTileData_Sprite_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            TileData tileData = new TileData();
            Sprite sprite = Resources.Load<Sprite>("sprites/tiles/generic");

            baseTile.GetTileData(
                Vector3Int.FloorToInt(position),
                null,
                ref tileData);

            Assert.AreEqual(tileData.sprite, sprite);
        }

        [UnityTest]
        public IEnumerator BaseTile_GetXPosition_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.GetXPosition(), (int)position.x);
        }

        [UnityTest]
        public IEnumerator BaseTile_GetYPosition_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.GetYPosition(), (int)position.y);
        }

        [UnityTest]
        public IEnumerator BaseTile_isAdjacent_True_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            BaseTile adjacentTile = ScriptableObject.CreateInstance<BaseTile>();
            adjacentTile.SetTileData(
                TileType.GENERIC,
                false,
                null,
                0,
                new Vector3(1.0f, 1.0f, 0.0f),
                -9,
                false,
                null,
                0);

            Assert.IsTrue(baseTile.isAdjacent(adjacentTile));
        }

        [UnityTest]
        public IEnumerator BaseTile_isAdjacent_False_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            BaseTile nonAdjacentTile = ScriptableObject.CreateInstance<BaseTile>();
            nonAdjacentTile.SetTileData(
                TileType.GENERIC,
                false,
                null,
                0,
                new Vector3(4.0f, 4.0f, 0.0f),
                -9,
                false,
                null,
                0);

            Assert.IsFalse(baseTile.isAdjacent(nonAdjacentTile));
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_type_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.type, tileType);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_isCollision_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.isCollision, collision);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_resource_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject itemObject = new GameObject();
            Item item = itemObject.AddComponent<Item>();
            baseTile.SetTileData(
                tileType,
                collision,
                item,
                resourceCount,
                position,
                distance,
                visited,
                parent,
                level);

            Assert.AreEqual(baseTile.resource, item);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_resourceCount_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.resourceCount, resourceCount);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_position_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.position, position);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_distance_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.distance, distance);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_visited_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.visited, visited);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_parent_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.parent, parent);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileData_level_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(baseTile.level, level);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileInformation_type_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            TileType newTileType = TileType.SAND;
            baseTile.SetTileInformation(
                newTileType,
                collision,
                resource,
                resourceCount,
                position);

            Assert.AreEqual(baseTile.type, newTileType);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileInformation_isCollision_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            bool newCollision = false;
            baseTile.SetTileInformation(
                tileType,
                newCollision,
                resource,
                resourceCount,
                position);

            Assert.AreEqual(baseTile.isCollision, newCollision);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileInformation_resource_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject newItemObject = new GameObject();
            Item newItem = newItemObject.AddComponent<Item>();

            baseTile.SetTileInformation(
                tileType,
                collision,
                newItem,
                resourceCount,
                position);

            Assert.AreEqual(baseTile.resource, newItem);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileInformation_resourceCount_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            int newResourceCount = 2;
            baseTile.SetTileInformation(
                tileType,
                collision,
                resource,
                newResourceCount,
                position);

            Assert.AreEqual(baseTile.resourceCount, newResourceCount);
        }

        [UnityTest]
        public IEnumerator BaseTile_SetTileInformation_position_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 newPosition = new Vector3(2.0f, 2.0f, 0.0f);
            baseTile.SetTileInformation(
                tileType,
                collision,
                resource,
                resourceCount,
                newPosition);

            Assert.AreEqual(baseTile.position, newPosition);
        }

        [UnityTest]
        public IEnumerator BaseTile_ToString_ResourceIsNull_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            Item newResource = null;
            baseTile.SetTileInformation(
                tileType,
                collision,
                newResource,
                resourceCount,
                position);

            string expectedString = $"Tile Type: {tileType,-10} Collision: {collision,-10} Position: {position,-50}";

            Assert.AreEqual(baseTile.ToString(), expectedString);
        }

        [UnityTest]
        public IEnumerator BaseTile_ToString_ResourceIsNotNull_Pass()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject newItemObject = new GameObject();
            Item newItem = newItemObject.AddComponent<Item>();
            newItem.name = "Unit Test Item";

            baseTile.SetTileInformation(
                tileType,
                collision,
                newItem,
                resourceCount,
                position);


            string expectedString = $"Tile Type: {tileType,-10} Collision: {collision,-10} Resource: {newItem.name,-10} ResourceCount: {resourceCount,-10} Position: {position,-50}";

            Assert.AreEqual(baseTile.ToString(), expectedString);
        }
    }
}
