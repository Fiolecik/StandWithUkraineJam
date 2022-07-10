using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    [System.Serializable]
    public struct Statistics
    {
        public int damage;
        public int heal;
        public int def;

        public static Statistics operator+(Statistics s1, Statistics s2)
        {
            Statistics s = new Statistics();
            s.damage = s1.damage + s2.damage;
            s.def = s1.def + s2.def;
            s.heal = s1.heal + s2.heal;
            return s;
        }
    }
}