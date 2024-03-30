using System;
using DataStructures;
using UnityEngine;

namespace CountDown
{
    public class GameRoot: Singleton<GameRoot>
    {
        [Header("Players")]
        [SerializeField] private Player player1;
        [SerializeField] private Player player2;

        [Header("Controllers")] 
        [SerializeField] private GameMaster gameMaster;
        
        public Player Player1 => player1;
        public Player Player2 => player2;

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            gameMaster.StartGame();
        }
    }
}