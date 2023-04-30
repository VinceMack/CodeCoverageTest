using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;

namespace Tests
{
    public class SandTileTest
    {
        [UnityTest]
        public IEnumerator SandTile_GetTileData_Sprite_Pass()
        {
            yield return null;

            Vector3 sandTilePosition = new Vector3(0.0f, 0.0f, 0.0f);
            SandTile sandTile = ScriptableObject.CreateInstance<SandTile>();
            sandTile.SetTileData(
                TileType.SAND,
                false,
                null,
                0,
                sandTilePosition,
                -9,
                false,
                null,
                0);

            TileData tileData = new TileData();
            Sprite sprite = Resources.Load<Sprite>("sprites/tiles/sand");

            sandTile.GetTileData(Vector3Int.FloorToInt(sandTilePosition), null, ref tileData);

            Assert.AreEqual(tileData.sprite, sprite);
        }
    }
}
