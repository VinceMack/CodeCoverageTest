using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class ItemTest
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("ItemTests");
        }

        [UnityTest]
        public IEnumerator Item_Awake()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Item item = objs.AddComponent<Item>();
            Berries berries = objs.AddComponent<Berries>();
            Bush bush = objs.AddComponent<Bush>();
            Rock rock = objs.AddComponent<Rock>();
            Coin coin = objs.AddComponent<Coin>();
            RockResource rockResource = objs.AddComponent<RockResource>();
            RockWall rockWall = objs.AddComponent<RockWall>();
            Tree tree = objs.AddComponent<Tree>();
            Wheat wheat = objs.AddComponent<Wheat>();
            WheatItem wheatItem = objs.AddComponent<WheatItem>();
            Wood wood = objs.AddComponent<Wood>();
            WoodWall woodWall = objs.AddComponent<WoodWall>();
            ErrorObject errorObject = objs.AddComponent<ErrorObject>();

            Assert.IsNotNull(item);
            Assert.IsNotNull(berries);
            Assert.IsNotNull(bush);
            Assert.IsNotNull(rock);
            Assert.IsNotNull(coin);
            Assert.IsNotNull(rockResource);
            Assert.IsNotNull(rockWall);
            Assert.IsNotNull(tree);
            Assert.IsNotNull(wheat);
            Assert.IsNotNull(wheatItem);
            Assert.IsNotNull(wood);
            Assert.IsNotNull(errorObject);
        }

        [UnityTest]
        public IEnumerator Item_Bush_IncrementAllResources()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Bush itemizedBush = objs.AddComponent<Bush>();
            Bush itemizedBush2 = objs.AddComponent<Bush>();
            Bush itemizedBush3 = objs.AddComponent<Bush>();
            Bush nullSpriteRendererBush = objs.AddComponent<Bush>();
            Bush nonNullSpriteRendererBush = objs.AddComponent<Bush>();
            Bush fullResourceBush = objs.AddComponent<Bush>();

            itemizedBush.isItemized = true;
            itemizedBush2.isItemized = false;
            itemizedBush3.isItemized = false;
            Bush.plantResources.Add(itemizedBush2);
            Bush.plantResources.Add(itemizedBush);
            Bush.plantResources.Add(itemizedBush3);
            Bush.plantResources[1].isItemized = true;
            Bush.plantResources[2].isItemized = false;
            Bush.IncrementAllResources(1);
            Bush.plantResources.Clear();

            nullSpriteRendererBush.isItemized = false;
            nullSpriteRendererBush.spriteRenderer = null;
            Bush.plantResources.Add(nullSpriteRendererBush);
            Bush.IncrementAllResources(1);
            Bush.plantResources.Clear();

            objs.AddComponent<SpriteRenderer>();
            nonNullSpriteRendererBush.isItemized = false;
            nonNullSpriteRendererBush.spriteRenderer = objs.GetComponent<SpriteRenderer>() as SpriteRenderer;
            nonNullSpriteRendererBush.resourceCount = 1;
            Bush.plantResources.Add(nonNullSpriteRendererBush);
            Bush.IncrementAllResources(1);
            Bush.plantResources.Clear();

            fullResourceBush.isItemized = false;
            fullResourceBush.spriteRenderer = objs.GetComponent<SpriteRenderer>() as SpriteRenderer;
            fullResourceBush.resourceCount = 24;
            Bush.plantResources.Add(fullResourceBush);
            Bush.IncrementAllResources(50);
            Bush.plantResources.Clear();

            Assert.IsNotNull(itemizedBush);
            Assert.IsNotNull(nullSpriteRendererBush);
            Assert.IsNotNull(nonNullSpriteRendererBush);
            Assert.IsNotNull(fullResourceBush);
        }

        [UnityTest]
        public IEnumerator Item_Bush_Deconstruct()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Bush.plantResources.Clear();
            Bush bush = objs.AddComponent<Bush>();

            bush.Deconstruct();

            Assert.AreEqual(Bush.plantResources.Count, 0);
        }

        [UnityTest]
        public IEnumerator Item_Bush_Harvest()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            objs.AddComponent<SpriteRenderer>();
            Bush bush = objs.AddComponent<Bush>();
            bush.resourceCount = 25;

            int val = bush.Harvest();
            Assert.AreEqual(val, 25);

            Bush bush2 = objs.AddComponent<Bush>();
            bush2.resourceCount = 1;

            int val2 = bush2.Harvest();
            Assert.AreEqual(val2, 1);
        }

        [UnityTest]
        public IEnumerator Item_Wheat_IncrementAllResources()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Wheat itemizedWheat = objs.AddComponent<Wheat>();
            Wheat itemizedWheat2 = objs.AddComponent<Wheat>();
            Wheat itemizedWheat3 = objs.AddComponent<Wheat>();
            Wheat nullSpriteRendererWheat = objs.AddComponent<Wheat>();
            Wheat nonNullSpriteRendererWheat = objs.AddComponent<Wheat>();
            Wheat fullResourceWheat = objs.AddComponent<Wheat>();

            itemizedWheat.isItemized = true;
            itemizedWheat2.isItemized = false;
            itemizedWheat3.isItemized = false;
            Wheat.plantResources.Add(itemizedWheat2);
            Wheat.plantResources.Add(itemizedWheat);
            Wheat.plantResources.Add(itemizedWheat3);
            Wheat.plantResources[1].isItemized = true;
            Wheat.plantResources[2].isItemized = false;
            Wheat.IncrementAllResources(1);
            Wheat.plantResources.Clear();

            nullSpriteRendererWheat.isItemized = false;
            nullSpriteRendererWheat.spriteRenderer = null;
            Wheat.plantResources.Add(nullSpriteRendererWheat);
            Wheat.IncrementAllResources(1);
            Wheat.plantResources.Clear();

            objs.AddComponent<SpriteRenderer>();
            nonNullSpriteRendererWheat.isItemized = false;
            nonNullSpriteRendererWheat.spriteRenderer = objs.GetComponent<SpriteRenderer>() as SpriteRenderer;
            nonNullSpriteRendererWheat.resourceCount = 1;
            Wheat.plantResources.Add(nonNullSpriteRendererWheat);
            Wheat.IncrementAllResources(1);
            Wheat.plantResources.Clear();

            fullResourceWheat.isItemized = false;
            fullResourceWheat.spriteRenderer = objs.GetComponent<SpriteRenderer>() as SpriteRenderer;
            fullResourceWheat.resourceCount = 24;
            Wheat.plantResources.Add(fullResourceWheat);
            Wheat.IncrementAllResources(50);
            Wheat.plantResources.Clear();

            Assert.IsNotNull(itemizedWheat);
            Assert.IsNotNull(nullSpriteRendererWheat);
            Assert.IsNotNull(nonNullSpriteRendererWheat);
            Assert.IsNotNull(fullResourceWheat);
        }

        [UnityTest]
        public IEnumerator Item_Wheat_Deconstruct()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Wheat.plantResources.Clear();
            Wheat wheat = objs.AddComponent<Wheat>();

            wheat.Deconstruct();

            Assert.AreEqual(Wheat.plantResources.Count, 0);
        }

        [UnityTest]
        public IEnumerator Item_Wheat_Harvest()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            objs.AddComponent<SpriteRenderer>();
            Wheat wheat = objs.AddComponent<Wheat>();
            wheat.resourceCount = 25;

            int val = wheat.Harvest();
            Assert.AreEqual(val, 25);

            Wheat wheat2 = objs.AddComponent<Wheat>();
            wheat2.resourceCount = 1;

            int val2 = wheat2.Harvest();
            Assert.AreEqual(val2, 1);
        }
        [UnityTest]
        public IEnumerator Item_Tree_IncrementAllResources()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Tree itemizedTree = objs.AddComponent<Tree>();
            Tree itemizedTree2 = objs.AddComponent<Tree>();
            Tree itemizedTree3 = objs.AddComponent<Tree>();
            Tree nullSpriteRendererTree = objs.AddComponent<Tree>();
            Tree nonNullSpriteRendererTree = objs.AddComponent<Tree>();
            Tree fullResourceTree = objs.AddComponent<Tree>();

            itemizedTree.isItemized = true;
            itemizedTree2.isItemized = false;
            itemizedTree3.isItemized = false;
            Tree.plantResources.Add(itemizedTree2);
            Tree.plantResources.Add(itemizedTree);
            Tree.plantResources.Add(itemizedTree3);
            Tree.plantResources[1].isItemized = true;
            Tree.plantResources[2].isItemized = false;
            Tree.IncrementAllResources(1);
            Tree.plantResources.Clear();

            nullSpriteRendererTree.isItemized = false;
            nullSpriteRendererTree.spriteRenderer = null;
            Tree.plantResources.Add(nullSpriteRendererTree);
            Tree.IncrementAllResources(1);
            Tree.plantResources.Clear();

            objs.AddComponent<SpriteRenderer>();
            nonNullSpriteRendererTree.isItemized = false;
            nonNullSpriteRendererTree.spriteRenderer = objs.GetComponent<SpriteRenderer>() as SpriteRenderer;
            nonNullSpriteRendererTree.resourceCount = 1;
            Tree.plantResources.Add(nonNullSpriteRendererTree);
            Tree.IncrementAllResources(1);
            Tree.plantResources.Clear();

            fullResourceTree.isItemized = false;
            fullResourceTree.spriteRenderer = objs.GetComponent<SpriteRenderer>() as SpriteRenderer;
            fullResourceTree.resourceCount = 24;
            Tree.plantResources.Add(fullResourceTree);
            Tree.IncrementAllResources(50);
            Tree.plantResources.Clear();

            Assert.IsNotNull(itemizedTree);
            Assert.IsNotNull(nullSpriteRendererTree);
            Assert.IsNotNull(nonNullSpriteRendererTree);
            Assert.IsNotNull(fullResourceTree);
        }

        [UnityTest]
        public IEnumerator Item_Tree_Deconstruct()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            Tree.plantResources.Clear();
            Tree tree = objs.AddComponent<Tree>();

            tree.Deconstruct();

            Assert.AreEqual(Tree.plantResources.Count, 0);
        }

        [UnityTest]
        public IEnumerator Item_Tree_Harvest()
        {
            yield return new WaitForSeconds(0.5f);

            GameObject objs = GameObject.Find("Objects");
            objs.AddComponent<SpriteRenderer>();
            Tree Tree = objs.AddComponent<Tree>();
            Tree.resourceCount = 25;

            int val = Tree.Harvest();
            Assert.AreEqual(val, 25);

            Tree tree2 = objs.AddComponent<Tree>();
            tree2.resourceCount = 1;

            int val2 = tree2.Harvest();
            Assert.AreEqual(val2, 1);
        }
    }
}