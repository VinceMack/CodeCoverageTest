using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{/* 
    public class Pawn_Test : Pawn_VM
    {
        [UnityTest]
        public void Pawn_Die_RemoveFromPawnList()
        {
            (new GameObject()).AddComponent<LaborOrderManager_VM>();
            if (Pawn_VM.PawnList != null) Pawn_VM.PawnList.Clear();
            Pawn_VM pawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn_VM")).GetComponent<Pawn_VM>();

            pawn.Die();

            Assert.IsFalse(Pawn_VM.PawnList.Contains(pawn));
        }
        
        [UnityTest]
        public void Pawn_CancelCurrentLaborOrder_ClearPath()
        {
            GridManager.InitializeGridManager();
            GridManager.CreateLevel();
            Pawn_Test pawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("prefabs/npc/Pawn_VM")).AddComponent<Pawn_Test>();

            pawn.currentLaborOrder = new LaborOrder_Base_VM(true);
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

