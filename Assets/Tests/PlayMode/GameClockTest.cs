using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class GameClockTests
    {
        private GameObject game;
        private GameClock clock;

        [SetUp]
        public void SetUp()
        {
            game = new GameObject();
            game.AddComponent<GameClock>();
            clock = game.GetComponent<GameClock>();
        }

        [UnityTest]
        public IEnumerator GameClock_Resume_Pause()
        {
            yield return new WaitForSeconds(0.5f);

            clock.Resume();
            clock.Pause();

            yield return new WaitForSeconds(0.5f);

            clock.Resume();

            Assert.Pass();
        }

        [UnityTest]
        public IEnumerator GameClock_OnTick()
        {
            yield return new WaitForSeconds(0.5f);

            game.AddComponent<Bush>();
            game.AddComponent<Tree>();
            clock.OnTick();

            Assert.Pass();
        }
    }
}