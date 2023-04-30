using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;

namespace Tests
{
    public class GrassTileTest
    {
        [UnityTest]
        public IEnumerator GrassTile_GetTileData_Sprite_Pass()
        {
            yield return null;

            Vector3 grassTilePosition = new Vector3(0.0f, 0.0f, 0.0f);
            GrassTile grassTile = ScriptableObject.CreateInstance<GrassTile>();
            grassTile.SetTileData(
                TileType.GRASS,
                false,
                null,
                0,
                grassTilePosition,
                -9,
                false,
                null,
                0);

            TileData tileData = new TileData();
            Sprite sprite = Resources.Load<Sprite>("sprites/tiles/grass");

            grassTile.GetTileData(Vector3Int.FloorToInt(grassTilePosition), null, ref tileData);

            Assert.AreEqual(tileData.sprite, sprite);
        }
    }
}
