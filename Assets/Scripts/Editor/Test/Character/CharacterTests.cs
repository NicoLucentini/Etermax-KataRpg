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

        //Iteration3
        [Test]
        public void MeleeFighterRangeCorrect()
        {
            MeleeFighter actor = new MeleeFighter("Actor");

            Assert.AreEqual(2, actor.Range);
        }
        [Test]
        public void RangeFighterCorrect()
        {
            RangeFighter actor = new RangeFighter("Actor");

            Assert.AreEqual(20, actor.Range);
        }

        //Iteration4
        [Test]
        public void CreatedCharacterWithNoFaction() {
            Character actor = new Character("Actor");

            bool hasFaction = actor.HasFaction();

            Assert.IsFalse(hasFaction);
        }
        [Test]
        public void CharacterJoinsFaction()
        {
            Character actor = new Character("Actor");
            actor.JoinFaction("Alliance");

            var inFaction = actor.InFaction("Alliance");

            Assert.IsTrue(inFaction);
        }
        [Test]
        public void CharacterLeavesFaction()
        {
            Character actor = new Character("Actor");
            actor.JoinFaction("Alliance");
            actor.LeavesFaction("Alliance");

            var inFaction = actor.InFaction("Alliance");

            Assert.IsFalse(inFaction);
        }
        [Test]
        public void CharacterLeavesFactionItsNotPresent()
        {
            Character actor = new Character("Actor");
            actor.JoinFaction("Orcs");
            actor.LeavesFaction("Alliance");

            var inFaction = actor.InFaction("Alliance");

            Assert.IsFalse(inFaction);
        }
        [Test]
        public void CharacterCantDoubleJoinAFaction()
        {
            Character actor = new Character("Actor");
            actor.JoinFaction("Orcs");
            actor.JoinFaction("Orcs");

            var factionCount = actor.Factions.Count;

            Assert.AreEqual(1, factionCount);
        }
        [Test]
        public void CharacterIsAllyWithOther() {
            Character actor = new Character("Actor");
            Character other = new Character("Other");
            actor.JoinFaction("Alliance");
            other.JoinFaction("Alliance");

            var isAlly = actor.IsAlly(other);

            Assert.IsTrue(isAlly);
        }
    }
}
