using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests 
{
	public class GameObjectTileTest
	{

        GameObjectTile tile;

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("GameObjectTileTests");
		}

		[UnityTest]
		public IEnumerator TestPlacement()
		{
            //Arrange
			yield return new WaitForSeconds(1);

            // Spawn Tiles
            Map map = GameObject.Find("World").GetComponent<Map>();
            map.TestSpawnWorld();

            yield return new WaitForSeconds(1);

            // Get tile to take neighbors of
            Transform layer = GameObject.Find("Layer1").transform;
            tile = layer.transform.GetChild(9).GetComponent<GameObjectTile>();

            //Act
            var neighbors = tile.GetNeighbors(true, true);

            //Assert
            Assert.AreEqual(neighbors.Count, 26);

            //Cleanup
            map.DestroyWorld();
        }
    }
}