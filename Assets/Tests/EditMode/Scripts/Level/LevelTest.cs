using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test
{
    public class LevelTest
    {
        Level level;
        int xMin, xMax, yMin, yMax, levelNumber;

        StairsTile ascendingStairs;
        StairsTile descendingStairs;

        [SetUp]
        public void Setup()
        {
            xMin = 0;
            xMax = 10;
            yMin = 0;
            yMax = 5;
            levelNumber = 0;

            level = new Level(levelNumber, xMin, xMax, yMin, yMax);

            // Set ascendingStairs
            Vector3 ascendingStairsPosition = new Vector3(1, 1, 0);
            ascendingStairs = ScriptableObject.CreateInstance<StairsTile>();
            ascendingStairs.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                ascendingStairsPosition,
                -9,
                false,
                null,
                levelNumber);
            level.AddAscendingStairs(ascendingStairs);

            // Set descendingStairs
            Vector3 descendingStairsPosition = new Vector3(4, 4, 0);
            descendingStairs = ScriptableObject.CreateInstance<StairsTile>();
            descendingStairs.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                descendingStairsPosition,
                -9,
                false,
                null,
                levelNumber);
            level.AddDescendingStairs(descendingStairs);
        }

        [Test]
        public void Level_getXMin_xMin_Pass()
        {
            Assert.AreEqual(level.getXMin(), xMin);
        }

        [Test]
        public void Level_getXMax_xMax_Pass()
        {
            Assert.AreEqual(level.getXMax(), xMax);
        }

        [Test]
        public void Level_getYMin_yMin_Pass()
        {
            Assert.AreEqual(level.getYMin(), yMin);
        }

        [Test]
        public void Level_getYMax_yMax_Pass()
        {
            Assert.AreEqual(level.getYMax(), yMax);
        }

        [Test]
        public void Level_getLevelNumber_levelNumber_Pass()
        {
            Assert.AreEqual(level.getLevelNumber(), levelNumber);
        }

        [Test]
        public void Level_getAllAscendingStairs_ExistingStairs_Pass()
        {
            List<StairsTile> stairsList = level.getAllAscendingStairs();

            Assert.AreEqual(stairsList.Count, 1);
        }

        [Test]
        public void Level_getAllDescendingStairs_ExistingStairs_Pass()
        {
            List<StairsTile> stairsList = level.getAllDescendingStairs();

            Assert.AreEqual(stairsList.Count, 1);
        }

        [Test]
        public void Level_SetLevel_LevelNumber_Pass()
        {
            int newLevelNumber = 1;
            level.setLevel(newLevelNumber, xMin, xMax, yMin, yMax);

            Assert.AreEqual(level.getLevelNumber(), newLevelNumber);
        }

        [Test]
        public void Level_SetLevel_xMin_Pass()
        {
            int newXMin = 1;
            level.setLevel(levelNumber, newXMin, xMax, yMin, yMax);

            Assert.AreEqual(level.getXMin(), newXMin);
        }

        [Test]
        public void Level_SetLevel_xMax_Pass()
        {
            int newXMax = 1;
            level.setLevel(levelNumber, xMin, newXMax, yMin, yMax);

            Assert.AreEqual(level.getXMax(), newXMax);
        }

        [Test]
        public void Level_SetLevel_yMin_Pass()
        {
            int newYMin = 1;
            level.setLevel(levelNumber, xMin, xMax, newYMin, yMax);

            Assert.AreEqual(level.getYMin(), newYMin);
        }

        [Test]
        public void Level_SetLevel_yMax_Pass()
        {
            int newYMax = 1;
            level.setLevel(levelNumber, xMin, xMax, yMin, newYMax);

            Assert.AreEqual(level.getYMax(), newYMax);
        }

        [Test]
        public void Level_AddAscendingStairs_QuantityEquals2_Pass()
        {
            StairsTile newStairsTile = ScriptableObject.CreateInstance<StairsTile>();
            newStairsTile.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                new Vector3(0.0f, 0.0f, 0.0f),
                -9,
                false,
                null,
                levelNumber);
            level.AddAscendingStairs(newStairsTile);

            List<StairsTile> stairsList = level.getAllAscendingStairs();
            Assert.AreEqual(stairsList.Count, 2);
        }

        [Test]
        public void Level_AddDescendingStairs_QuantityEquals2_Pass()
        {
            StairsTile newStairsTile = ScriptableObject.CreateInstance<StairsTile>();
            newStairsTile.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                new Vector3(0.0f, 0.0f, 0.0f),
                -9,
                false,
                null,
                levelNumber);
            level.AddDescendingStairs(newStairsTile);

            List<StairsTile> stairsTile = level.getAllDescendingStairs();
            Assert.AreEqual(stairsTile.Count, 2);
        }

        [Test]
        public void Level_getAscendingStairs_ClosestStairs_Pass()
        {
            Vector3 newStairsPosition = new Vector3(3.0f, 3.0f, 0.0f);
            StairsTile newStairsTile = ScriptableObject.CreateInstance<StairsTile>();
            newStairsTile.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                newStairsPosition,
                -9,
                false,
                null,
                levelNumber);
            level.AddAscendingStairs(newStairsTile);

            Vector3Int currentPosition = new Vector3Int(4, 4, 0);
            StairsTile closestStairs = level.getAscendingStairs(currentPosition);

            Assert.AreEqual(newStairsPosition, closestStairs.position);
        }

        [Test]
        public void Level_getDescendingStairs_ClosestStairs_Pass()
        {
            Vector3 newStairsPosition = new Vector3(2.0f, 2.0f, 0.0f);
            StairsTile newStairsTile = ScriptableObject.CreateInstance<StairsTile>();
            newStairsTile.SetTileData(
                TileType.STAIRS,
                false,
                null,
                0,
                newStairsPosition,
                -9,
                false,
                null,
                levelNumber);
            level.AddDescendingStairs(newStairsTile);

            Vector3Int currentPosition = new Vector3Int(1, 1, 0);
            StairsTile closestStairs = level.getDescendingStairs(currentPosition);

            Assert.AreEqual(newStairsPosition, closestStairs.position);
        }

        [Test]
        public void Level_getAscendingStairs_Null_Pass()
        {
            Level newLevel = new Level();
            Vector3Int currentPosition = new Vector3Int(0, 0, 0);
            StairsTile stairs = newLevel.getAscendingStairs(currentPosition);

            Assert.IsNull(stairs);
        }

        [Test]
        public void Level_getDescendingStairs_Null_Pass()
        {
            Level newLevel = new Level();
            Vector3Int currentPosition = new Vector3Int(0, 0, 0);
            StairsTile stairs = newLevel.getDescendingStairs(currentPosition);

            Assert.IsNull(stairs);
        }
    }
}
