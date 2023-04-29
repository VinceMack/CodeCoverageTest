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
        [SetUp]
        public void Init()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                UnityEngine.Object.Destroy(obj);
            }
            GameObject g = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/test/TestGrid"));
            g.name = "Grid";
            GridManager.InitializeGridManager();
            for(int i=0; i<GridManager.mapLevels.Count; i++)
            {
                GridManager.ResetGrid(i);
            }
            GridManager.CreateLevel();
            LaborOrderManager.InitializeLaborOrderManager();
            Pawn.PawnList.Clear();
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
            yield return null;
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

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_GetLaborOrderFromTilemap_CorrectTile()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            Vector3Int loc = new Vector3Int(10, 10, 0);
            pawn.SetCurrentLaborOrder(new LaborOrder_Base(LaborType.Basic, loc, 1f));

            yield return null;

            Assert.IsTrue(pawn.GetLaborOrderTileFromTilemap() == GridManager.tileMap.GetTile(loc));

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_GetPawnTileFromTilemap_CorrectTile()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            Vector3Int loc = Vector3Int.FloorToInt(GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))));
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), loc, Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            Assert.IsTrue(pawn.GetPawnTileFromTilemap() == GridManager.tileMap.GetTile(loc));

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_MoveLaborTypeDownPriorityLevel()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
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

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_MoveLaborTypeUpPriorityLevel()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();

            int start = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
            int x;
            for (int i = 0; i < start-1; i++)
            {
                x = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
                pawn.MoveLaborTypeUpPriorityLevel(LaborType.Basic);
                Assert.IsTrue(pawn.GetPriorityLevelOfLabor(LaborType.Basic) == x - 1);
            }
            x = pawn.GetPriorityLevelOfLabor(LaborType.Basic);
            pawn.MoveLaborTypeUpPriorityLevel(LaborType.Basic);
            Assert.IsTrue(pawn.GetPriorityLevelOfLabor(LaborType.Basic) == x);

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_SetCurrentLaborOrder()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(0,0,0), 1f);

            pawn.SetCurrentLaborOrder(order);
            Assert.IsTrue(pawn.GetCurrentLaborOrder() == order);
            Assert.IsFalse(pawn.SetCurrentLaborOrder(order));

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_SetCurrentLaborOrder_AlreadyInAvailablePawns()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(0, 0, 0), 1f);

            LaborOrderManager.AddAvailablePawn(pawn);
            pawn.SetCurrentLaborOrder(order);
            Assert.IsFalse(LaborOrderManager.availablePawns.Contains(pawn));

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }

        [UnityTest]
        public IEnumerator Pawn_DecrementAllHunger()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            pawn.hunger = 100;
            int dec = 1;

            while(pawn.hunger > 0)
            {
                int old = pawn.hunger;
                Pawn.DecrementAllHunger(dec);
                Assert.IsTrue(pawn.hunger == old - dec);
            }
            Assert.IsFalse(Pawn.PawnList.Contains(pawn));
        }

        [UnityTest]
        public IEnumerator Pawn_CompleteLaborOrder_TileNull()
        {
            yield return new WaitForSeconds(0.5f);
            GridManager.InitializeGridManager();
            GameObject pawnObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn"), GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);
            Pawn pawn = pawnObject.GetComponent<Pawn>();
            LaborOrder_Base order = new LaborOrder_Base(LaborType.Basic, new Vector3Int(-99999999, -9999999, 0), 1f);
            pawn.SetCurrentLaborOrder(order);

            pawn.StartCoroutine(pawn.CompleteLaborOrder());
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(LaborOrderManager.availablePawns.Contains(pawn));

            pawn.Die();
            UnityEngine.Object.Destroy(pawnObject);
        }
    }
}

//typeof(Pawn).GetField("currentLaborOrder", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(pawn, new LaborOrder_Base(LaborType.Basic, loc, 1f));

