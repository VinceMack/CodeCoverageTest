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
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("EmptyMap", LoadSceneMode.Single);
            yield return null;
            GridManager.InitializeGridManager();
            for (int i = 1; i <= 2; i++)
            {
                GridManager.CreateLevel();
            }
            GlobalStorage.chests.Clear();
            LaborOrderManager.InitializeLaborOrderManager();
            Pawn.PawnList.Clear();
            yield return new EnterPlayMode();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
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
        public IEnumerator LaborOrder_Gather_Execute_Downstairs()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            // create downstairs chest
            Vector3 loc1 = new Vector3(130, 25, 0);
            BaseTile tile1 = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(loc1));
            Item chestPrefab = Resources.Load<Item>("prefabs/items/Chest");
            Item chestInstance = UnityEngine.Object.Instantiate(chestPrefab, loc1, Quaternion.identity);
            tile1.SetTileInformation(tile1.type, true, chestInstance, tile1.resourceCount, tile1.position);


            // create item
            Vector3 loc = new Vector3(55, 27, 0);
            Item resource = Resources.Load<Item>("prefabs/items/Coin");
            BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(loc));
            Item resourceObject = UnityEngine.Object.Instantiate(resource, loc, Quaternion.identity).GetComponent<Item>();
            tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);
            tile.resource = resourceObject;

            LaborOrder_Gather order = new LaborOrder_Gather(resourceObject);
            typeof(Pawn).GetField("pawnSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, 15f);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNull(GameObject.Find("Coin(Clone)"));
            Assert.IsNotNull(GlobalStorage.GetChestWithItem("Coin"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Gather_Execute_Upstairs()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(130, 25, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            // create upstairs chest
            Vector3 loc1 = new Vector3(45, 25, 0);
            BaseTile tile1 = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(loc1));
            Item chestPrefab = Resources.Load<Item>("prefabs/items/Chest");
            Item chestInstance = UnityEngine.Object.Instantiate(chestPrefab, loc1, Quaternion.identity);
            tile1.SetTileInformation(tile1.type, true, chestInstance, tile1.resourceCount, tile1.position);


            // create item
            Vector3 loc = new Vector3(55, 27, 0);
            Item resource = Resources.Load<Item>("prefabs/items/Coin");
            BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(loc));
            Item resourceObject = UnityEngine.Object.Instantiate(resource, loc, Quaternion.identity).GetComponent<Item>();
            tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);
            tile.resource = resourceObject;

            LaborOrder_Gather order = new LaborOrder_Gather(resourceObject);
            typeof(Pawn).GetField("pawnSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, 15f);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNull(GameObject.Find("Coin(Clone)"));
            Assert.IsNotNull(GlobalStorage.GetChestWithItem("Coin"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Forage_Execute_Tree()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            GridManager.PopulateWithTree();
            Tree obj = GameObject.Find("Tree(Clone)").GetComponent<Tree>();
            obj.resourceCount = Tree.MAX_RESOURCE;
            obj.isForageable = true;
            LaborOrder_Forage order = new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Tree);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNotNull(GameObject.Find("Wood(Clone)"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Forage_Execute_Bush()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            GridManager.PopulateWithBush();
            Bush obj = GameObject.Find("Bush(Clone)").GetComponent<Bush>();
            obj.resourceCount = Tree.MAX_RESOURCE;
            obj.isForageable = true;
            LaborOrder_Forage order = new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Bush);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNotNull(GameObject.Find("Berries(Clone)"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Place_Execute_Random()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            Item obj = Resources.Load<Item>("prefabs/items/ErrorObject");
            GameObject objectParent = new GameObject("Objects");
            LaborOrder_Place order = new LaborOrder_Place(obj);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNotNull(GameObject.Find("ErrorObject(Clone)"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Place_Execute_NotRandom()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            Item obj = Resources.Load<Item>("prefabs/items/ErrorObject");
            GameObject objectParent = new GameObject("Objects");
            LaborOrder_Place order = new LaborOrder_Place(obj, new Vector2(55, 27));

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsNotNull(GameObject.Find("ErrorObject(Clone)"));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Deconstruct_Execute()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            GameObject objectParent = new GameObject("Objects");

            Vector3 loc = new Vector3(55, 27, 0);
            Item resource = Resources.Load<Item>("prefabs/items/Tree");
            BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(loc));
            Item resourceObject = UnityEngine.Object.Instantiate(resource, loc, Quaternion.identity).GetComponent<Item>();
            resourceObject.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
            tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);
            tile.resource = resourceObject;


            LaborOrder_Deconstruct order = new LaborOrder_Deconstruct(resourceObject);

            yield return pawn.StartCoroutine(order.Execute(pawn));
            yield return null;

            Assert.IsTrue(GameObject.Find("Tree(Clone)").GetComponent<Tree>().isGatherable);
        }

        [UnityTest]
        public IEnumerator LaborOrder_Base_Execute_Random()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            LaborOrder_Base order = new LaborOrder_Base(true);

            yield return pawn.StartCoroutine(order.Execute(pawn));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Base_Execute_NotRandom()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(55, 27, 0), 0.1f);

            yield return pawn.StartCoroutine(order.Execute(pawn));
        }

        [UnityTest]
        public IEnumerator LaborOrder_Base_GetLocation()
        {
            yield return new WaitForSeconds(0.5f);
            Vector3Int loc = new Vector3Int(55, 27, 0);

            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, loc, 0.1f);

            Assert.IsTrue(loc == Vector3Int.FloorToInt(order.GetLocation()));
        }
        //targetPosition + Vector3Int.down + Vector3Int.right,
    }
}