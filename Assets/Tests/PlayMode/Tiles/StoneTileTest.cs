using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;

namespace Tests
{
    public class StoneTileTest
    {
        [UnityTest]
        public IEnumerator StoneTile_GetTileData_Sprite_Pass()
        {
            yield return null;

            Vector3 stoneTilePosition = new Vector3(0.0f, 0.0f, 0.0f);
            StoneTile stoneTile = ScriptableObject.CreateInstance<StoneTile>();
            stoneTile.SetTileData(
                TileType.STONE,
                false,
                null,
                0,
                stoneTilePosition,
                -9,
                false,
                null,
                0);

            TileData tileData = new TileData();
            Sprite sprite = Resources.Load<Sprite>("sprites/tiles/stone");

            stoneTile.GetTileData(Vector3Int.FloorToInt(stoneTilePosition), null, ref tileData);

            Assert.AreEqual(tileData.sprite, sprite);
        }
    }
}
