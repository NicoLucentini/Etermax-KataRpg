using Actions;
using com.rpg.domain;
using NUnit.Framework;

namespace Editor.Test.Actions
{
    public class AttackTests
    {
        private Character _actor;
        private Character _target;
        private Attack _attack;

        [SetUp]
        public void SetUp()
        {
            _actor = new Character("Actor");
            _attack = new Attack();
        }
        
        [Test]
        public void DamageIsSubstractedFromHealth()
        {
            var initialHealth = 100;
            _target = new Character("Target", initialHealth);

            _attack.Execute(_actor, _target);
            
            Assert.AreEqual(initialHealth - _actor.DamageAmount, _target.Health);
        }
        
        [Test]
        public void HealthCantBeLessThanZero()
        {
            var initialHealth = _actor.DamageAmount - 1;
            _target = new Character("Target", initialHealth);

            _attack.Execute(_actor, _target);
            
            Assert.AreEqual(0, _target.Health);
        }
        
        [Test]
        public void WhenHealthIsZeroTheCharacterDies()
        {
            var initialHealth = _actor.DamageAmount;
            _target = new Character("Target", initialHealth);

            _attack.Execute(_actor, _target);
            
            Assert.IsFalse(_target.Alive);
        }

        //Iteration2

        [Test]
        public void CharacterCantAttackHimSelf()
        {
            _target = new Character("Actor");

            _attack.Execute(_actor, _target);
            Assert.AreEqual(1000, _target.Health);
        }

        [Test]
        public void AttackIsReducedBy50PercentWhenTargetIs5LevelsAbove()
        {
            _target = new Character("Target", Character.MAX_HEALTH, 6);

            _attack.Execute(_actor, _target);
            Assert.AreEqual(1000 - (int)(_actor.DamageAmount * 0.5f), _target.Health);
        }
        [Test]
        public void AttackIsIncreasedBy50PercentWhenTargetIs5LevelsBelow()
        {
            _actor = new Character("Actor", Character.MAX_HEALTH, 6);
            _target = new Character("Target", Character.MAX_HEALTH, 1);

            _attack.Execute(_actor, _target);
            Assert.AreEqual(1000 - (int)(_actor.DamageAmount * 1.5f), _target.Health);
        }

        [Test]
        public void AttackIsNotReducedBy50PercentWhenTargetIsNot5LevelsAbove()
        {
            _target = new Character("Target", Character.MAX_HEALTH, 1);

            _attack.Execute(_actor, _target);
            Assert.AreEqual(1000 - _actor.DamageAmount, _target.Health);
        }
        [Test]
        public void AttackIsNotIncreasedBy50PercentWhenTargetIsNot5LevelsBelow()
        {
            _actor = new Character("Actor", Character.MAX_HEALTH, 1);
            _target = new Character("Target", Character.MAX_HEALTH, 1);

            _attack.Execute(_actor, _target);
            Assert.AreEqual(1000 - _actor.DamageAmount, _target.Health);
        }

        //Iteration 3
        [Test]
        public void AttackInRange()
        {
            _actor = new MeleeFighter("Actor");
            _target = new Character("Target");

            _attack.Execute(_actor, _target, 1);
            Assert.AreEqual(Character.MAX_HEALTH - _actor.DamageAmount, _target.Health);
        }
        [Test]
        public void AttackOutsideRange()
        {
            _actor = new RangeFighter("Actor");
            _target = new Character("Target");

            _attack.Execute(_actor, _target, 30);
            Assert.AreEqual(Character.MAX_HEALTH, _target.Health);
        }

        //Iteration4

        [Test]
        public void AlliesCantDealDamageBetweenThem() {
            _actor.JoinFaction("Alliance");
            _target = new Character("Target");
            _target.JoinFaction("Alliance");

            _attack.Execute(_actor, _target);
            Assert.AreEqual(Character.MAX_HEALTH, _target.Health);

        }
    }
}