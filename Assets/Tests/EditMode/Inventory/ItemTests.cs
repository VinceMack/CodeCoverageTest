using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests
{
    public class ItemTests
    {
        private Item item;

        [Test]
        public void testInvokation()
        {
            //Arrange
            item = new Item();
            
            //Act
            item.InvokePlacing(new BaseNPC());

            //Assert
            Assert.NotNull(item);
        }
    }
}