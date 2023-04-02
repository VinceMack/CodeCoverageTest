using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LaborOrderManagerTests : LaborOrderManager
    {
        [Test]
        public void removeSpecificPawnTest()
        {
            availablePawns = new Queue<Pawn>();
            GameObject pawn1 = new GameObject();
            GameObject pawn2 = new GameObject();
            Pawn p1 = pawn1.AddComponent<Pawn>();
            Pawn p2 = pawn2.AddComponent<Pawn>();

            removeSpecificPawn(p1);

            Assert.IsTrue(availablePawns.Count == 1);
            Assert.IsTrue(availablePawns.Dequeue() == p2);
        }
    }
}
