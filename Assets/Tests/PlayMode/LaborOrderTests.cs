using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
	public class LaborOrderTests
	{
		[UnityTest]
		public IEnumerator LaborOrderWoodcutExecuteTest()
        {
            SceneManager.LoadScene("LaborOrderTestScene");
            yield return new WaitForSeconds(1);

            GameObject tree = GlobalInstance.Instance.entityDictionary.InstantiateEntity("tree");
            LaborOrder_Woodcut order = new LaborOrder_Woodcut(tree);
            LaborOrderManager.addLaborOrder(order);
            GameObject pawn = GlobalInstance.Instance.entityDictionary.InstantiateEntity("pawn");
            LaborOrderManager.addPawn(pawn.GetComponent<Pawn>());

            yield return new WaitForSeconds(order.getTimeToComplete()+0.5f);

            Assert.IsNull(GameObject.Find("Tree(Clone)"));
            Assert.IsNotNull(GameObject.Find("Wood(Clone)"));
        }
	}
}