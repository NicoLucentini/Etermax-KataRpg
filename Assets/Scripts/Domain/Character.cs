
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
        
        public Character(string id, int health = MAX_HEALTH, int level = 1)
        {
            Id = id;
            Health = health;
            Level = level;
            Alive = true;
        }

        public int HealthToMax()
        {
            return MAX_HEALTH - Health;
        }
    }
}