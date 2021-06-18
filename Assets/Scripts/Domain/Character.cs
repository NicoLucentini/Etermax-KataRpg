

using System.Collections.Generic;

namespace com.rpg.domain
{
    public class Character
    {
        public const int MAX_HEALTH = 1000;
        public string Id { get; }
        public int Health { get; set; }
        public int Level { get; private set; }

        public bool Alive { get; set; }

        public int DamageAmount = 10;
        public int HealAmount = 10;

        public int Range { get; protected set; }

        public List<string> Factions;

        public Character(string id, int health = MAX_HEALTH, int level = 1)
        {
            Id = id;
            Health = health;
            Level = level;
            Alive = true;
            Factions = new List<string>();
        }

        public int HealthToMax()
        {
            return MAX_HEALTH - Health;
        }
        public bool HasFaction() => Factions.Count > 0;

        public bool InFaction(string faction) => Factions.Exists(x => x.Equals(faction));

        public void JoinFaction(string faction)  =>  Factions.Add(faction);

        public void LeavesFaction(string faction) => Factions.Remove(faction);

        public bool IsAlly(Character other) => Factions.Exists(x => other.Factions.Contains(x));

        public string FactionsToString() {
            string factionsValue = "None";
            if(Factions.Count > 0)
            { 
                Factions.ForEach(x => factionsValue += $"{x} -");
                factionsValue.Remove(factionsValue.Length - 1);
            }
            return factionsValue;
        }
    }
}