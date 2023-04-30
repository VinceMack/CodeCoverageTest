using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class GameManagerTests
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("CombinedManagersTesting");
        }

        [UnityTest]
        public IEnumerator GameManagerTestMethod()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject gm = GameObject.Find("GameManager");
            GameObject grid = GameObject.Find("Grid");
            GameObject pawns = GameObject.Find("Pawns");
            GameObject clock = GameObject.Find("GameClock");
            
            Assert.AreEqual(gm==null, false);
            Assert.AreEqual(grid==null, false);
            Assert.AreEqual(pawns==null, false);
            Assert.AreEqual(clock==null, false);

            GameManager gameManager = gm.GetComponent<GameManager>();
            Grid gridComp = grid.GetComponent<Grid>();
            int numOfPawns = pawns.transform.childCount;
            GameClock gameClock = clock.GetComponent<GameClock>();

            Assert.AreEqual(gameManager==null, false);
            Assert.AreEqual(gridComp==null, false);
            Assert.AreEqual(LaborOrderManager.GetPawnCount(), GameManager.NUM_OF_PAWNS_TO_SPAWN);
            LaborOrderManager.ClearLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetNumOfLaborOrders(), 0);

            LaborOrderManager.AddLaborOrder(new LaborOrder_Base());
            LaborOrderManager.AssignPawnsToLaborOrders();

            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 1);

            Assert.AreEqual(LaborOrderManager.GetLaborTypesCount(), 9);

            LaborOrderManager.AddLaborOrder(new LaborOrder_Base());
            LaborOrderManager.ClearLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0);

            GameObject item = new GameObject();
            item.AddComponent<Item>();
            Item itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddPlaceLaborOrder(itemComp);
            LaborOrder_Base Place = LaborOrderManager.laborQueues[(int)LaborType.Place].Dequeue();
            Assert.AreEqual(Place.laborType, LaborType.Place);

            item = new GameObject();
            item.AddComponent<Item>();
            itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddPlaceLaborOrder(itemComp, new Vector2(0,0));
            LaborOrder_Base vecPlace = LaborOrderManager.laborQueues[(int)LaborType.Place].Dequeue();
            Assert.AreEqual(vecPlace.laborType, LaborType.Place);

            item = new GameObject();
            item.AddComponent<Item>();
            itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddCraftLaborOrder(itemComp);
            LaborOrder_Base Craft = LaborOrderManager.laborQueues[(int)LaborType.Craft].Dequeue();
            Assert.AreEqual(Craft.laborType, LaborType.Craft);

            item = new GameObject();
            item.AddComponent<Item>();
            itemComp = item.GetComponent<Item>();
            LaborOrderManager.AddCraftLaborOrder(itemComp, new Vector2(0,0));
            LaborOrder_Base vecCraft = LaborOrderManager.laborQueues[(int)LaborType.Craft].Dequeue();
            Assert.AreEqual(vecCraft.laborType, LaborType.Craft);
            LaborOrderManager.ClearLaborOrders();

            // delete all items from the game scene
            LaborOrderManager.AddDeconstructLaborOrder();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0); // line 90 
            LaborOrderManager.ClearLaborOrders();

            // create an item
            GameObject item2 = new GameObject();
            item2.AddComponent<Item>();
            Item itemComp2 = item2.GetComponent<Item>();
            LaborOrderManager.AddSpecificDeconstructLaborOrder(itemComp2);

            // create a pawn
            GameObject pawn = new GameObject();
            pawn.AddComponent<Pawn>();
            Pawn pawnComp = pawn.GetComponent<Pawn>();

            // create a second pawn
            GameObject pawn2 = new GameObject();
            pawn2.AddComponent<Pawn>();
            Pawn pawnComp2 = pawn2.GetComponent<Pawn>();

            // create a third pawn
            LaborOrderManager.AddAvailablePawn(pawnComp);
            LaborOrderManager.AddAvailablePawn(pawnComp2);
            LaborOrderManager.RemoveFromAvailablePawns(pawnComp2);
            LaborOrderManager.RemoveSpecificPawn(pawnComp);

            LaborOrderManager.ClearLaborOrders();
            LaborOrderManager.FillWithRandomLaborOrders(5);
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 5);
            LaborOrderManager.ClearLaborOrders();

            LaborOrderManager.PopulateObjectLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetLaborOrderCount(), 0);

            string temp = LaborOrderManager.GetLaborTypeName(0);
            Assert.AreEqual(temp, "Eat");

            Assert.AreEqual(LaborOrderManager.GetLaborType("Eat"), LaborType.Eat);

            // Create an item called "Tree(Clone)"
            GridManager.PopulateWithTrees();
            GridManager.PopulateWithBushes();
            GridManager.PopulateWithRocksPerlin();
            GridManager.PopulateWithRocks();
            GridManager.PopulateWithWheatPlants();
            GridManager.PopulateWithChest();
            LaborOrderManager.PopulateObjectLaborOrders();
            LaborOrderManager.FillWithRandomPawns(10);
            LaborOrderManager.AssignPawnsToLaborOrders();

            LaborOrderManager.ClearLaborOrders();
            Wheat.IncrementAllResources(10000);
            Tree.IncrementAllResources(10000);
            Bush.IncrementAllResources(10000);
            LaborOrderManager.PopulateObjectLaborOrders();
            LaborOrderManager.AssignPawnsToLaborOrders();
            LaborOrderManager.ClearLaborOrders();

            // create a gatherable berries item in the scene
            GameObject berries = new GameObject();
            berries.AddComponent<Berries>();
            Berries berriesComp = berries.GetComponent<Berries>();
            berriesComp.isGatherable = true;

            // create a gatherable RockResource item in the scene
            GameObject rock = new GameObject();
            rock.AddComponent<RockResource>();
            RockResource rockComp = rock.GetComponent<RockResource>();
            rockComp.isGatherable = true;

            // create a gatherable WheatItem item in the scene
            GameObject wheat = new GameObject();
            wheat.AddComponent<WheatItem>();
            WheatItem wheatComp = wheat.GetComponent<WheatItem>();
            wheatComp.isGatherable = true;

            // create a gatherable Wood item in the scene
            GameObject wood = new GameObject();
            wood.AddComponent<Wood>();
            Wood woodComp = wood.GetComponent<Wood>();
            woodComp.isGatherable = true;

            // create a gatherable Wheat item in the scene
            GameObject wheatPlant = new GameObject();
            wheatPlant.AddComponent<Wheat>();
            Wheat wheatPlantComp = wheatPlant.GetComponent<Wheat>();
            wheatPlantComp.isGatherable = true;

            // create a gatherable Coin item in the scene
            GameObject coin = new GameObject();
            coin.AddComponent<Coin>();
            Coin coinComp = coin.GetComponent<Coin>();
            coinComp.isGatherable = true;

            LaborOrderManager.PopulateObjectLaborOrders();
            LaborOrderManager.PopulateObjectLaborOrdersUpdated();
            LaborOrderManager.PopulateObjectLaborOrdersMineable();
            LaborOrderManager.PopulateObjectLaborOrdersGatherable();
            LaborOrderManager.PopulateObjectLaborOrdersPlantcuttable();
            LaborOrderManager.PopulateObjectLaborOrdersForageableBushes();
            LaborOrderManager.PopulateObjectLaborOrdersForageableTrees();
            LaborOrderManager.PopulateObjectLaborOrdersDeconstructable();

            LaborOrderManager.PopulateObjectLaborOrderTile(null);
            LaborOrderManager.PopulateObjectLaborOrderTile(new BaseTile());

            // create a game object named "Bush(Clone)"
            GameObject bush = new GameObject();
            bush.name = "Bush(Clone)";
            // add an item component to the game object
            bush.AddComponent<Item>();
            // get the item component
            Item bushComp = bush.GetComponent<Item>();
            bushComp.isForageable = true;
            // create a tile with the resource set to the bushComp
            BaseTile bushTile = new BaseTile();
            bushTile.resource = bushComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(bushTile);

            // create a game object named "TreeObj(Clone)"
            GameObject TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            // add an item component to the game object
            TreeObj.AddComponent<Item>();
            // get the item component
            Item TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isForageable = true;
            // create a tile with the resource set to the TreeObjComp
            BaseTile TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);

            // create a game object named "TreeObj(Clone)"
            TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            // add an item component to the game object
            TreeObj.AddComponent<Item>();
            // get the item component
            TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isGatherable = true;
            // create a tile with the resource set to the TreeObjComp
            TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);

            // create a game object named "TreeObj(Clone)"
            TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            // add an item component to the game object
            TreeObj.AddComponent<Item>();
            // get the item component
            TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isDeconstructable = true;
            // create a tile with the resource set to the TreeObjComp
            TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);

            // create a game object named "TreeObj(Clone)"
            TreeObj = new GameObject();
            TreeObj.name = "Tree(Clone)";
            // add an item component to the game object
            TreeObj.AddComponent<Item>();
            // get the item component
            TreeObjComp = TreeObj.GetComponent<Item>();
            TreeObjComp.isMineable = true;
            // create a tile with the resource set to the TreeObjComp
            TreeObjTile = new BaseTile();
            TreeObjTile.resource = TreeObjComp;
            LaborOrderManager.PopulateObjectLaborOrderTile(TreeObjTile);

            GridManager.GetAdjacentAndDiagonalTiles(TreeObjTile);
            GridManager.ClearStairs();

            LaborOrderManager.laborQueues = null;
            LaborOrderManager.availablePawns = null;
            LaborOrderManager.ClearLaborOrders();
            LaborOrderManager.GetLaborOrderCount();
            LaborOrderManager.GetAvailablePawnCount();


        }
    }
}



