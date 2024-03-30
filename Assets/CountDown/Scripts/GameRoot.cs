using System;
using DataStructures;
using JetBrains.Annotations;
using UnityEngine;

namespace CountDown
{
    public class GameRoot: Singleton<GameRoot>
    {
        [Header("Players")]
        [SerializeField] private Player player1;
        [SerializeField] private Player player2;

        [Header("Cameras")] 
        [SerializeField] private Camera player1Camera;
        [SerializeField] private Camera player2Camera;

        [Header("Controllers")] 
        [SerializeField] private GameMaster gameMaster;
        
        public Player Player1 => player1;
        public Player Player2 => player2;

        public Camera Player1Camera => player1Camera;
        public Camera Player2Camera => player2Camera;

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            gameMaster.StartGame();
        }

        public Player GetOtherPlayer(Player player)
        {
            if (player == player1)
            {
                return player2;
            }
            else
            {
                return player1;
            }
        }
    }
}