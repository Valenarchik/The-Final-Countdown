using DataStructures;
using UnityEngine;

namespace CountDown
{
    public class GameRoot: Singleton<GameRoot>
    {
        public Map Map;
        public Player Player1;
        public Player Player2;
    }
}