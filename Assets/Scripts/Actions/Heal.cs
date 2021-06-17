using System;
using com.rpg.domain;

namespace Actions
{
    public class Heal
    {
        public void Execute(Character actor, Character target)
        {
            if (!target.Alive)
                return;
            
            var healing = actor.HealAmount;
            target.Health += Math.Min(healing, target.HealthToMax() );
        }
    }
}