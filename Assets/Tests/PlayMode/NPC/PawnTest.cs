using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{/* 
    public class Pawn_Test : Pawn
    {
        [UnityTest]
        public void Pawn_Die_RemoveFromPawnList()
        {
            (new GameObject()).AddComponent<LaborOrderManager>();
            if (Pawn.PawnList != null) Pawn.PawnList.Clear();
            Pawn pawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn")).GetComponent<Pawn>();

            pawn.Die();

            Assert.IsFalse(Pawn.PawnList.Contains(pawn));
        }
        
        [UnityTest]
        public void Pawn_CancelCurrentLaborOrder_ClearPath()
        {
            GridManager.InitializeGridManager();
            GridManager.CreateLevel();
            Pawn_Test pawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn")).AddComponent<Pawn_Test>();

            pawn.currentLaborOrder = new LaborOrder_Base(true);
            pawn.CompleteLaborOrder();

            pawn.CancelCurrentLaborOrder();

            Assert.IsTrue(pawn.path.Count == 0);
        }
    }*/
}

/*
    public void CancelCurrentLaborOrder()
    {
        path.Clear();
        if (currentExecution != null)
            StopCoroutine(currentExecution);
        if (currentPathExecution != null)
            StopCoroutine(currentPathExecution);
    }*/

