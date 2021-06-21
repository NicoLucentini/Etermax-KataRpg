using System;
using com.rpg.domain;

namespace Actions
{
    public class Attack
    {
        public void Execute(Character actor, Character target)
        {
            Execute(actor, target, 0);
        }
        public void Execute(Character actor, Character target, int distance) {

            if (actor.Id == target.Id) return;
            if (actor.IsAlly(target)) return;
            if (actor.Range < distance) return;

            float attackCoef = 1;
            if (target.Level - actor.Level >= 5)
                attackCoef = 0.5f;
            else if (actor.Level - target.Level >= 5)
                attackCoef = 1.5f;

            var damage = (int)(actor.DamageAmount * attackCoef);
            target.Health -= Math.Min(damage, target.Health);
            if (target.Health <= 0)
            {
                target.Alive = false;
            }
        }
    }
}