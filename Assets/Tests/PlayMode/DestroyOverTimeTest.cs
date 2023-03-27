using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
	public class DestroyOverTimeTest
    {
        [UnityTest]
        public IEnumerator DestroyTest()
        {
            GameObject obj = new GameObject("ObjectToDestroy");
            SerializedObject so = new SerializedObject(obj.AddComponent<DestroyOverTime>());
            SerializedProperty property = so.FindProperty("destroyDelay");
            property.floatValue = 0.5f;
            so.ApplyModifiedProperties();

            Assert.IsNotNull(GameObject.Find("ObjectToDestroy"));
            yield return new WaitForSeconds(1.0f);
            Assert.IsNull(GameObject.Find("ObjectToDestroy"));
        }
    }
}

