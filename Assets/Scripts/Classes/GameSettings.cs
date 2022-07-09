using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    public class GameSettings
    {
        public const int MAX_PLAYERS = 4;
        
        public int Players { get => players; }
        public int CPU { get => cpu;  }

        private int players=1;
        private int cpu=1;

        public void SetPlayers(int count)
        {
            if(count < 1)
                return;
            if (cpu + count >= MAX_PLAYERS)
            {
                players = MAX_PLAYERS - cpu;
                return;
            }

            players = count;
        }

        public void SetCPUs(int count)
        {
            if (players == 1 && count < 1)
            {
                cpu = 1;
            }

            if (players + count >= MAX_PLAYERS)
            {
                cpu = MAX_PLAYERS - players;
                return;
            }

            cpu = count;
        }
    }
}