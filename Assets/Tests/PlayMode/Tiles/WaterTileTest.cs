using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;

namespace Tests
{
    public class WaterTileTest
    {
        [UnityTest]
        public IEnumerator WaterTile_GetTileData_Sprite_Pass()
        {
            yield return null;

            Vector3 waterTilePosition = new Vector3(0.0f, 0.0f, 0.0f);
            WaterTile waterTile = ScriptableObject.CreateInstance<WaterTile>();
            waterTile.SetTileData(
                TileType.WATER,
                false,
                null,
                0,
                waterTilePosition,
                -9,
                false,
                null,
                0);

            TileData tileData = new TileData();
            Sprite sprite = Resources.Load<Sprite>("sprites/tiles/water");

            waterTile.GetTileData(Vector3Int.FloorToInt(waterTilePosition), null, ref tileData);

            Assert.AreEqual(tileData.sprite, sprite);
        }
    }
}
