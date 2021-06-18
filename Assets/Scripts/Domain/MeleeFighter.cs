using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.rpg.domain
{
    public class MeleeFighter : Character
    {
        public MeleeFighter(string id, int health = MAX_HEALTH, int level = 1) : base(id, health, level) {
            Range = 2;
        }
    }
}
