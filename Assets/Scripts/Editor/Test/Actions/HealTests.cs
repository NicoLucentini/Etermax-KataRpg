using Actions;
using com.rpg.domain;
using NUnit.Framework;

namespace Editor.Test.Actions
{
    public class HealTests
    {
        private Character _actor;
        private Character _target;
        private Heal _heal;

        [SetUp]
        public void SetUp()
        {
            _actor = new Character("Actor");
            _heal = new Heal();
        }
        
        [Test]
        public void ACharacterCanBeHealed()
        {
            var initialHealth = 100;
            _target = new Character("Target", initialHealth);

            _heal.Execute(_actor, _target);
            
            Assert.AreEqual(initialHealth + _actor.HealAmount, _target.Health);
        }

        [Test]
        public void HealthCantBeRisedAboveMax()
        {
            _target = new Character("Target");

            _heal.Execute(_actor, _target);
            
            Assert.AreEqual(Character.MAX_HEALTH, _target.Health);
        }
        
        [Test]
        public void ADeadCharacterCantBeHealed()
        {
            _target = new Character("Target", _actor.DamageAmount);

            var _attack = new Attack();
            _attack.Execute(_actor, _target);
            
            _heal.Execute(_actor, _target);
            
            Assert.AreEqual(0, _target.Health);
            Assert.IsFalse(_target.Alive);
        }

    }
}