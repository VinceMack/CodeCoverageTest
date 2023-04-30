using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class ManagersTests
    {
        // This LoadScene will be universal for all playmode tests
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("CombinedManagersTesting", LoadSceneMode.Single);
            yield return null;
            yield return new EnterPlayMode();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
        }

        // Function 1: Test if scene for GameManager is setup correctly
        [UnityTest]
        public IEnumerator SceneSetup()
        {
            yield return new WaitForSeconds(1f);

            GameObject gm = GameObject.Find("GameManager");
            GameObject grid = GameObject.Find("Grid");
            GameObject pawns = GameObject.Find("Pawns");
            GameObject clock = GameObject.Find("GameClock");

            Assert.IsNotNull(gm);
            Assert.IsNotNull(grid);
            Assert.IsNotNull(pawns);
            Assert.IsNotNull(clock);

            GameManager gameManager = gm.GetComponent<GameManager>();
            Grid gridComp = grid.GetComponent<Grid>();
            int numOfPawns = pawns.transform.childCount;
            GameClock gameClock = clock.GetComponent<GameClock>();

            Assert.IsNotNull(gameManager);
            Assert.IsNotNull(gridComp);

            Assert.AreEqual(LaborOrderManager.GetPawnCount(), GameManager.NUM_OF_PAWNS_TO_SPAWN);

            Assert.AreEqual(LaborOrderManager.GetNumOfLaborOrders(), 0);
        }

        // Function 2: LaborOrder Generation & Assignment
        [UnityTest]
        public IEnumerator TestLaborOrderGenerationAndAssignment()
        {
            LaborOrderManager.AddLaborOrder(new LaborOrder_Base());
            LaborOrderManager.AssignPawnsToLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 1);
            Assert.AreEqual(LaborOrderManager.GetLaborTypesCount(), 9);
            yield return null;
        }

        // Function 3: Clear Labor Orders
        [UnityTest]
        public IEnumerator TestClearLaborOrders()
        {
            LaborOrderManager.AddLaborOrder(new LaborOrder_Base());
            LaborOrderManager.ClearLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0);
            yield return null;
        }

        // Function 4: AddPlaceLaborOrder(itemComp);
        [UnityTest]
        public IEnumerator TestAddPlaceLaborOrder()
        {
            GameObject item = new GameObject();
            item.AddComponent<Item>();
            Item itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddPlaceLaborOrder(itemComp);
            LaborOrder_Base Place = LaborOrderManager.laborQueues[(int)LaborType.Place].Dequeue();
            Assert.AreEqual(Place.laborType, LaborType.Place);
            yield return null;
        }

        // Function 5: AddPlaceLaborOrder(itemComp, new Vector2(0,0));
        [UnityTest]
        public IEnumerator TestAddPlaceLaborOrderWithVector()
        {
            GameObject item = new GameObject();
            item.AddComponent<Item>();
            Item itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddPlaceLaborOrder(itemComp, new Vector2(0, 0));
            LaborOrder_Base vecPlace = LaborOrderManager.laborQueues[(int)LaborType.Place].Dequeue();
            Assert.AreEqual(vecPlace.laborType, LaborType.Place);
            yield return null;
        }

        // Function 6: AddDeconstructLaborOrder();
        [UnityTest]
        public IEnumerator TestAddDeconstructLaborOrder()
        {
            GameObject item = new GameObject();
            item.AddComponent<Item>();
            Item itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddCraftLaborOrder(itemComp);
            LaborOrder_Base Craft = LaborOrderManager.laborQueues[(int)LaborType.Craft].Dequeue();
            Assert.AreEqual(Craft.laborType, LaborType.Craft);

            GameObject[] items = GameObject.FindGameObjectsWithTag("item");
            foreach (GameObject i in items)
            {
                GameObject.Destroy(i);
            }
            LaborOrderManager.AddDeconstructLaborOrder();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0); // line 90 
            LaborOrderManager.ClearLaborOrders();
            yield return null;
        }

        // Function 7: AddCraftLaborOrder(itemComp, new Vector2(0,0));
        [UnityTest]
        public IEnumerator TestAddCraftLaborOrderWithVector()
        {
            GameObject item = new GameObject();
            item.AddComponent<Item>();
            Item itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddCraftLaborOrder(itemComp, new Vector2(0, 0));
            LaborOrder_Base vecCraft = LaborOrderManager.laborQueues[(int)LaborType.Craft].Dequeue();
            Assert.AreEqual(vecCraft.laborType, LaborType.Craft);
            LaborOrderManager.ClearLaborOrders();

            GameObject item2 = new GameObject();
            item2.AddComponent<Item>();
            Item itemComp2 = item2.GetComponent<Item>();
            LaborOrderManager.AddSpecificDeconstructLaborOrder(itemComp2);
            Assert.AreEqual(LaborOrderManager.laborQueues[(int)LaborType.Deconstruct].Count, 1);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestAddAndRemoveAvailablePawns()
        {
            Assert.AreEqual(LaborOrderManager.GetAvailablePawnCount(), 10);
            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 0);

            GameObject pawn = new GameObject();
            pawn.AddComponent<Pawn>();
            Pawn pawnComp = pawn.GetComponent<Pawn>();

            LaborOrderManager.AddAvailablePawn(pawnComp);
            Assert.AreEqual(LaborOrderManager.GetAvailablePawnCount(), 11);

            LaborOrderManager.RemoveFromAvailablePawns(pawnComp);
            Assert.AreEqual(LaborOrderManager.GetAvailablePawnCount(), 10);

            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 0);
            Assert.AreEqual(LaborOrderManager.GetPawnCount(), 10);
            yield return null;
        }

        // Function 9: GetLaborOrderCount()
        [UnityTest]
        public IEnumerator TestGetLaborOrderCount()
        {
            LaborOrderManager.ClearLaborOrders();
            LaborOrderManager.FillWithRandomLaborOrders(5);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 5);
            LaborOrderManager.ClearLaborOrders();
            yield return null;
        }

        // Function 10: PopulateObjectLaborOrders() w/o objects
        [UnityTest]
        public IEnumerator TestPopulateObjectLaborOrdersWithoutObjects()
        {
            // remove all items from the scene
            GameObject[] items = GameObject.FindGameObjectsWithTag("item");
            foreach (GameObject i in items)
            {
                GameObject.Destroy(i.gameObject);
            }
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0);
            yield return null;
        }

        // Function 11: PopulateObjectLaborOrders() with objects
        [UnityTest]
        public IEnumerator TestPopulateObjectLaborOrdersWithObjects()
        {
            GridManager.PopulateWithTrees();
            GridManager.PopulateWithBushes();
            GridManager.PopulateWithRocksPerlin();
            GridManager.PopulateWithRocks();
            GridManager.PopulateWithWheatPlants();
            GridManager.PopulateWithChest();
            LaborOrderManager.PopulateObjectLaborOrders();
            LaborOrderManager.FillWithRandomPawns(10);
            LaborOrderManager.AssignPawnsToLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 10);

            LaborOrderManager.ClearLaborOrders();
            Wheat.IncrementAllResources(10000);
            Tree.IncrementAllResources(10000);
            Bush.IncrementAllResources(10000);
            LaborOrderManager.PopulateObjectLaborOrders();
            LaborOrderManager.FillWithRandomPawns(10);
            LaborOrderManager.AssignPawnsToLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 20);
            yield return null;
        }

        // Function 12: GetLaborTypeName
        [UnityTest]
        public IEnumerator TestGetLaborTypeName()
        {
            string temp = LaborOrderManager.GetLaborTypeName(0);
            Assert.AreEqual(temp, "Eat");
            Assert.AreEqual(LaborOrderManager.GetLaborType("Eat"), LaborType.Eat);
            yield return null;
        }

        // Function 13: PopulateObjectLaborOrders, all types
        [UnityTest]
        public IEnumerator TestPopulateObjectLaborOrdersAllTypes()
        {
            LaborOrderManager.ClearLaborOrders();
            GameObject berries = new GameObject();
            berries.AddComponent<Berries>();
            Berries berriesComp = berries.GetComponent<Berries>();
            berriesComp.isGatherable = true;
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 1);

            // create a gatherable RockResource item in the scene
            LaborOrderManager.ClearLaborOrders();
            GameObject rock = new GameObject();
            rock.AddComponent<RockResource>();
            RockResource rockComp = rock.GetComponent<RockResource>();
            rockComp.isGatherable = true;
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 2);

            // create a gatherable WheatItem item in the scene
            LaborOrderManager.ClearLaborOrders();
            GameObject wheat = new GameObject();
            wheat.AddComponent<WheatItem>();
            WheatItem wheatComp = wheat.GetComponent<WheatItem>();
            wheatComp.isGatherable = true;
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 3);

            // create a gatherable Wood item in the scene
            LaborOrderManager.ClearLaborOrders();
            GameObject wood = new GameObject();
            wood.AddComponent<Wood>();
            Wood woodComp = wood.GetComponent<Wood>();
            woodComp.isGatherable = true;
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 4);

            // create a gatherable Wheat item in the scene
            LaborOrderManager.ClearLaborOrders();
            GameObject wheatPlant = new GameObject();
            wheatPlant.AddComponent<Wheat>();
            Wheat wheatPlantComp = wheatPlant.GetComponent<Wheat>();
            wheatPlantComp.isGatherable = true;
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 5);

            // create a gatherable Coin item in the scene
            LaborOrderManager.ClearLaborOrders();
            GameObject coin = new GameObject();
            coin.AddComponent<Coin>();
            Coin coinComp = coin.GetComponent<Coin>();
            coinComp.isGatherable = true;
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 6);

            LaborOrderManager.ClearLaborOrders();
            LaborOrderManager.PopulateObjectLaborOrders();
            LaborOrderManager.PopulateObjectLaborOrdersUpdated();
            LaborOrderManager.PopulateObjectLaborOrdersMineable();
            LaborOrderManager.PopulateObjectLaborOrdersGatherable();
            LaborOrderManager.PopulateObjectLaborOrdersPlantcuttable();
            LaborOrderManager.PopulateObjectLaborOrdersForageableBushes();
            LaborOrderManager.PopulateObjectLaborOrdersForageableTrees();
            LaborOrderManager.PopulateObjectLaborOrdersDeconstructable();
            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.Greater(LaborOrderManager.GetLaborOrderCount(), 0);
            yield return null;
        }

        // Function 14: PopulateObjectLaborOrderTile all types
        [UnityTest]
        public IEnumerator TestPopulateObjectLaborOrderTileAllTypes()
        {
            LaborOrderManager.ClearLaborOrders();
            LaborOrderManager.PopulateObjectLaborOrderTile(null);
            LaborOrderManager.PopulateObjectLaborOrderTile(new BaseTile());
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0);

            // create a game object named "Bush(Clone)"
            GameObject bush = new GameObject();
            bush.name = "Bush(Clone)";
            bush.AddComponent<Item>();
            Item bushComp = bush.GetComponent<Item>();
            bushComp.isForageable = true;
            BaseTile bushTile = new BaseTile();
            bushTile.resource = bushComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(bushTile);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 1);

            // create a game object named "TreeObj(Clone)"
            GameObject TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            TreeObj.AddComponent<Item>();
            Item TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isForageable = true;
            BaseTile TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 2);

            // create a game object named "TreeObj(Clone)"
            TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            TreeObj.AddComponent<Item>();
            TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isGatherable = true;
            TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 3);

            // create a game object named "TreeObj(Clone)"
            TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            TreeObj.AddComponent<Item>();
            TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isDeconstructable = true;
            TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 4);

            // create a game object named "TreeObj(Clone)"
            TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            TreeObj.AddComponent<Item>();
            TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isMineable = true;
            TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 5);
            yield return null;
        }

    }
}