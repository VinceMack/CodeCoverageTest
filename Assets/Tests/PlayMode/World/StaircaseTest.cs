using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class StaircaseTest
	{

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

			GameObject npc = GameObject.Find("TestCharacter");

            // Prepare staircase
            StaircaseItem stairItem = new StaircaseItem();
            stairItem.entityDictionary = GlobalInstance.Instance.entityDictionary;
            stairItem.prefabList = GlobalInstance.Instance.prefabList;

            // Get tile to spawn staircase on
            Transform layer = GameObject.Find("Layer0").transform;
            GameObjectTile goTile = new GameObjectTile();

            int i = 0;
            foreach(Transform child in layer)
            {
                i += 1;
                if(i != 9)
                {
                    continue;
                }
                goTile = child.gameObject.GetComponent<GameObjectTile>();
            }

            // Set tile for staircase to spawn on
            npc.GetComponent<SaveableEntity>().SetCurrentLocation(goTile);

            //Act
            stairItem.InvokePlacing(npc.GetComponent<BaseNPC>());

            //Assert
			Assert.AreEqual(goTile.transform.childCount, 1);
		}
	}
}