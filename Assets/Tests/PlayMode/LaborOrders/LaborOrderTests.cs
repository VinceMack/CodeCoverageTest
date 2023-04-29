using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;

namespace Tests
{
    public class LaborOrderTests
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("EmptyMap", LoadSceneMode.Single);
        }

        [UnityTest]
        public IEnumerator LaborOrder_Plantcut_Execute()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            GridManager.PopulateWithWheatPlant();
            Item obj = GameObject.Find("Wheat(Clone)").GetComponent<Wheat>();
            LaborOrder_Plantcut order = new LaborOrder_Plantcut(obj);

            yield return pawn.StartCoroutine(order.Execute(pawn));

            Assert.IsNotNull(GameObject.Find("Wheat(Clone)"));
            Assert.IsNotNull(GameObject.Find("WheatItem(Clone)"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Mine_Execute()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            
            GridManager.PopulateWithRock();
            Item obj = GameObject.Find("Rock(Clone)").GetComponent<Rock>();
            LaborOrder_Mine order = new LaborOrder_Mine(obj);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNull(GameObject.Find("Rock(Clone)"));
            Assert.IsTrue(GameObject.Find("RockResource(Clone)") != null || GameObject.Find("Coin(Clone)") != null);
        }

        [UnityTest]
        public IEnumerator LaborOrder_Gather_Execute()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            GridManager.PopulateWithChest();

            // create item
            Vector3 loc = new Vector3(55, 27, 0);
            Item resource = Resources.Load<Item>("prefabs/items/Coin");
            BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(loc));
            Item resourceObject = UnityEngine.Object.Instantiate(resource, loc, Quaternion.identity).GetComponent<Item>();
            resourceObject.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
            tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);
            tile.resource = resourceObject;


            LaborOrder_Gather order = new LaborOrder_Gather(resourceObject);
            typeof(Pawn).GetField("pawnSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, 15f);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNull(GameObject.Find("Coin(Clone)"));
            Assert.IsNotNull(GlobalStorage.GetChestWithItem("Coin"));
        }

        //targetPosition + Vector3Int.down + Vector3Int.right,
    }
}