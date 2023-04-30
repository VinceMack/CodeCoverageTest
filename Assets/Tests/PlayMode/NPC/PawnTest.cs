using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;

namespace Tests
{
    public class Pawn_Test
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
        public IEnumerator Pawn_Die_RemoveFromPawnList()
        {
            yield return new WaitForSeconds(0.5f);

            (new GameObject()).AddComponent<LaborOrderManager>();
            if (Pawn.PawnList != null) Pawn.PawnList.Clear();
            Pawn pawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn")).GetComponent<Pawn>();

            pawn.Die();

            Assert.IsFalse(Pawn.PawnList.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_CancelCurrentLaborOrder_ClearPath()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            pawn.SetCurrentLaborOrder(new LaborOrder_Base(true));

            pawn.StartCoroutine(pawn.CompleteLaborOrder());
            yield return null;
            pawn.CancelCurrentLaborOrder();

            Assert.IsTrue(pawn.path.Count == 0);
        }

        [UnityTest]
        public IEnumerator Pawn_GetLaborOrderFromTilemap_CorrectTile()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            Vector3Int loc = new Vector3Int(10, 10, 0);
            pawn.SetCurrentLaborOrder(new LaborOrder_Base(LaborType.Basic, loc, 1f));

            yield return null;

            Assert.IsTrue(pawn.GetLaborOrderTileFromTilemap() == GridManager.tileMap.GetTile(loc));
        }

        [UnityTest]
        public IEnumerator Pawn_GetPawnTileFromTilemap_CorrectTile()
        {
            yield return new WaitForSeconds(0.5f);
            Vector3Int loc = Vector3Int.FloorToInt(GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))));
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), loc, Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            Assert.IsTrue(pawn.GetPawnTileFromTilemap() == GridManager.tileMap.GetTile(loc));
        }

        [UnityTest]
        public IEnumerator Pawn_MoveLaborTypeDownPriorityLevel()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            int levels = pawn.GetPriorityLevelsCount();

            int start = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
            int x;
            for (int i = 0; i < levels - start; i++)
            {
                x = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
                pawn.MoveLaborTypeDownPriorityLevel(LaborType.Basic);
                Assert.IsTrue(pawn.GetPriorityLevelOfLabor(LaborType.Basic) == x + 1);
            }
            x = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
            pawn.MoveLaborTypeDownPriorityLevel(LaborType.Basic);
            Assert.IsTrue(pawn.GetPriorityLevelOfLabor(LaborType.Basic) == x);
        }

        [UnityTest]
        public IEnumerator Pawn_MoveLaborTypeUpPriorityLevel()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            int start = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
            int x;
            for (int i = 0; i < start - 1; i++)
            {
                x = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
                pawn.MoveLaborTypeUpPriorityLevel(LaborType.Basic);
                Assert.IsTrue(pawn.GetPriorityLevelOfLabor(LaborType.Basic) == x - 1);
            }
            x = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
            pawn.MoveLaborTypeUpPriorityLevel(LaborType.Basic);
            Assert.IsTrue(pawn.GetPriorityLevelOfLabor(LaborType.Basic) == x);
        }

        [UnityTest]
        public IEnumerator Pawn_SetCurrentLaborOrder()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(0, 0, 0), 1f);

            pawn.SetCurrentLaborOrder(order);
            Assert.IsTrue(pawn.GetCurrentLaborOrder() == order);
            Assert.IsFalse(pawn.SetCurrentLaborOrder(order));
        }

        [UnityTest]
        public IEnumerator Pawn_SetCurrentLaborOrder_AlreadyInAvailablePawns()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(0, 0, 0), 1f);

            LaborOrderManager.AddAvailablePawn(pawn);
            pawn.SetCurrentLaborOrder(order);
            Assert.IsFalse(LaborOrderManager.availablePawns.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_DecrementAllHunger()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            pawn.hunger = 100;
            int dec = 1;

            while (pawn.hunger > 0)
            {
                int old = pawn.hunger;
                Pawn.DecrementAllHunger(dec);
                Assert.IsTrue(pawn.hunger == old - dec);
            }
            Assert.IsFalse(Pawn.PawnList.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_DecrementAllHunger_SeekBerryChest()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            GridManager.PopulateWithChest();
            Chest chest = GlobalStorage.GetRandomChest();
            chest.AddItem("Berries");
            pawn.hunger = Pawn.HUNGER_RESPONSE_THRESHOLD;

            Pawn.DecrementAllHunger(1);

            Assert.IsTrue(pawn.GetCurrentLaborOrder() is LaborOrder_Eat);
        }

        [UnityTest]
        public IEnumerator Pawn_CompleteLaborOrder_TargetTileNull()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(-99999999, -9999999, 0), 1f);
            pawn.SetCurrentLaborOrder(order);

            pawn.StartCoroutine(pawn.CompleteLaborOrder());
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(LaborOrderManager.availablePawns.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_CompleteLaborOrder_CurrentTileNull()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(-99999999, -9999999, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(55, 27, 0), 1f);
            pawn.SetCurrentLaborOrder(order);

            pawn.StartCoroutine(pawn.CompleteLaborOrder());
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(LaborOrderManager.availablePawns.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_CompleteLaborOrder_Upstairs()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(130, 25, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            typeof(Pawn).GetField("pawnSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, 15f);
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(55, 27, 0), 0.2f);
            pawn.SetCurrentLaborOrder(order);
            LaborOrderManager.RemoveSpecificPawn(pawn);

            yield return pawn.StartCoroutine(pawn.CompleteLaborOrder());

            Assert.IsTrue(LaborOrderManager.availablePawns.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_CompleteLaborOrder_Downstairs()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(55, 27, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            typeof(Pawn).GetField("pawnSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, 15f);
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(130, 25, 0), 0.2f);
            pawn.SetCurrentLaborOrder(order);
            LaborOrderManager.RemoveSpecificPawn(pawn);

            yield return pawn.StartCoroutine(pawn.CompleteLaborOrder());

            Assert.IsTrue(LaborOrderManager.availablePawns.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_CompleteLaborOrder_ReachedTarget()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(55, 27, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            typeof(Pawn).GetField("pawnSpeed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, 15f);
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(55, 27, 0), 0.2f);
            pawn.SetCurrentLaborOrder(order);
            LaborOrderManager.RemoveSpecificPawn(pawn);

            yield return pawn.StartCoroutine(pawn.CompleteLaborOrder());

            Assert.IsTrue(LaborOrderManager.availablePawns.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_GetPawnName()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(55, 27, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            typeof(Pawn).GetField("pawnName", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, "Waltuh");

            Assert.IsTrue(pawn.GetPawnName() == "Waltuh");
        }

        [UnityTest]
        public IEnumerator Pawn_GetLaborTypesCount()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), new Vector3(55, 27, 0), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            Assert.IsTrue(pawn.GetLaborTypesCount() == typeof(LaborType).GetFields().Length - 1);
        }
    }
}

//typeof(Pawn).GetField("currentLaborOrder", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, new LaborOrder_Base(LaborType.Basic, loc, 1f));

