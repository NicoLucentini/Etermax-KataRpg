using System.Collections;
using System.Collections.Generic;
using com.rpg.domain;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Test.Actions
{
    public class CharacterTests
    {


        [Test]
        public void CharacterSetupsCorrectly()
        {
            Character actor = new Character("Actor");

            Assert.AreEqual(Character.MAX_HEALTH, actor.Health);
            Assert.AreEqual(1, actor.Level);
            Assert.True(actor.Alive);
        }

        
    }
}
