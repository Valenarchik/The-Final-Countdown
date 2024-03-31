using DataStructures;
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

        public GameMaster GameMaster => gameMaster;
        
        public Vector2 Player1StartPosition { get; private set; }
        public Vector2 Player2StartPosition { get; private set; }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            gameMaster.StartGame();
            Player1StartPosition = Player1.transform.position;
            Player2StartPosition = Player2.transform.position;
        }

        public void GameOver()
        {
            
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