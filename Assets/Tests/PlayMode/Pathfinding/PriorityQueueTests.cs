using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

namespace Tests
{
    public class PriorityQueueTests
    {
        [UnityTest]
        public IEnumerator EnqueueDequeueTest()
        {
            PriorityQueue priorityQueue = new PriorityQueue();

            BaseTile tile1 = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tile2 = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tile3 = ScriptableObject.CreateInstance<BaseTile>();

            priorityQueue.Enqueue(tile1, 1);
            priorityQueue.Enqueue(tile2, 3);
            priorityQueue.Enqueue(tile3, 2);

            Assert.AreEqual(3, priorityQueue.Count);

            BaseTile firstItem = priorityQueue.Dequeue();
            Assert.AreEqual(tile1, firstItem);

            BaseTile secondItem = priorityQueue.Dequeue();
            Assert.AreEqual(tile3, secondItem);

            BaseTile thirdItem = priorityQueue.Dequeue();
            Assert.AreEqual(tile2, thirdItem);

            Assert.AreEqual(0, priorityQueue.Count);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ContainsTest()
        {
            PriorityQueue priorityQueue = new PriorityQueue();

            BaseTile tileA = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tileB = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tileC = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tileD = ScriptableObject.CreateInstance<BaseTile>();

            priorityQueue.Enqueue(tileA, 1);
            priorityQueue.Enqueue(tileB, 2);
            priorityQueue.Enqueue(tileC, 3);

            Assert.IsTrue(priorityQueue.Contains(tileA));
            Assert.IsTrue(priorityQueue.Contains(tileB));
            Assert.IsTrue(priorityQueue.Contains(tileC));
            Assert.IsFalse(priorityQueue.Contains(tileD));

            yield return null;
        }

        [UnityTest]
        public IEnumerator ClearTest()
        {
            PriorityQueue priorityQueue = new PriorityQueue();

            BaseTile tile1 = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tile2 = ScriptableObject.CreateInstance<BaseTile>();
            BaseTile tile3 = ScriptableObject.CreateInstance<BaseTile>();

            priorityQueue.Enqueue(tile1, 1);
            priorityQueue.Enqueue(tile2, 2);
            priorityQueue.Enqueue(tile3, 3);

            Assert.AreEqual(3, priorityQueue.Count);

            priorityQueue.Clear();

            Assert.AreEqual(0, priorityQueue.Count);
            Assert.IsFalse(priorityQueue.Contains(tile1));
            Assert.IsFalse(priorityQueue.Contains(tile2));
            Assert.IsFalse(priorityQueue.Contains(tile3));

            yield return null;
        }
    }
}