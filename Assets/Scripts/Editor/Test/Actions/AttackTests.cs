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
    }
}