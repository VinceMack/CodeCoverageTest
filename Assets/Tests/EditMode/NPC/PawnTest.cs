using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

namespace Tests
{
    public class PawnTest
    {
        [Test]
        public void moveLaborTypeToPriorityLevelTest()
        {
            GameObject pawn = new GameObject();
            Pawn p =  pawn.AddComponent<Pawn>();
            //pawn.SetActive(true);
            int expected = 3;
            LaborType type = (LaborType)0;

            p.moveLaborTypeToPriorityLevel(type, expected);

            List<LaborType>[] priority = p.getLaborTypePriority();
            Assert.IsTrue(priority[expected].Contains(type));
        }
    }
}
