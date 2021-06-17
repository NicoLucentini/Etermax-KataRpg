using System;
using com.rpg.domain;

namespace Actions
{
    public class Attack
    {
        public void Execute(Character actor, Character target)
        {
            var damage = actor.DamageAmount;
            target.Health -= Math.Min(damage, target.Health);
            if (target.Health <= 0)
            {
                target.Alive = false;
            }
        }
    }
}