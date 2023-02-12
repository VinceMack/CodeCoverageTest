using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests {
	public class GitHubTest {

		[Test]
		public void EditmodeTestScripExampleSimplePasses () {
            Assert.AreNotEqual(2, 2);
            //Test
		}

		[UnityTest]
		public IEnumerator EditmodeTestScriptExampleWithEnumeratorPasses () {
			yield return null;
		}

	}
}