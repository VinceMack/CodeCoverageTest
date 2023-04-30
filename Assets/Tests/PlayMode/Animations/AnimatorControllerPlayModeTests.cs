using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AnimatorControllerPlayModeTests
    {
        private GameObject testGameObject;
        private AnimatorController testAnimatorController;

        [SetUp]
        public void Setup()
        {
            testGameObject = new GameObject();
            testGameObject.AddComponent<Animator>();
            testAnimatorController = testGameObject.AddComponent<AnimatorController>();
        }

        [UnityTest]
        public IEnumerator TestChangeAnim()
        {
            // ARRANGE
            Vector2 positiveXDirection = new Vector2(1, 0);
            Vector2 negativeXDirection = new Vector2(-1, 0);
            Vector2 positiveYDirection = new Vector2(0, 1);
            Vector2 negativeYDirection = new Vector2(0, -1);

            // ACT
            testAnimatorController.ChangeAnim(positiveXDirection);
            yield return null;
            float xMovementPositive = testAnimatorController.GetAnimFloat("xMovement");

            testAnimatorController.ChangeAnim(negativeXDirection);
            yield return null;
            float xMovementNegative = testAnimatorController.GetAnimFloat("xMovement");

            testAnimatorController.ChangeAnim(positiveYDirection);
            yield return null;
            float yMovementPositive = testAnimatorController.GetAnimFloat("yMovement");

            testAnimatorController.ChangeAnim(negativeYDirection);
            yield return null;
            float yMovementNegative = testAnimatorController.GetAnimFloat("yMovement");

            testAnimatorController.SetAnimParameter("dead", true);
            bool isDead = testAnimatorController.GetAnimBool("dead");

            // ASSERT
            Assert.AreEqual(1f, xMovementPositive);
            Assert.AreEqual(-1f, xMovementNegative);
            Assert.AreEqual(1f, yMovementPositive);
            Assert.AreEqual(-1f, yMovementNegative);
            Assert.AreEqual(true, isDead);
        }
    }
}