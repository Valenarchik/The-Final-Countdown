using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Camera rocketCamera;
        [SerializeField] private float moveCameraDelay = 3;
        [SerializeField] private float loadSceneDelay = 6;

        [Header("Controllers")] 
        [SerializeField] private GameMaster gameMaster;

        [Space] 
        [SerializeField] private Rocket rocket;
        [SerializeField] private GameObject UI;
        
        public Player Player1 => player1;
        public Player Player2 => player2;

        public IEnumerable<Player> Players => new[] {Player1, Player2};

        public Camera Player1Camera => player1Camera;
        public Camera Player2Camera => player2Camera;

        public IEnumerable<Camera> PlayerCameras => new[] {Player1Camera, Player2Camera};

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
            UI.gameObject.SetActive(false);
            StartCoroutine(LoadWinOrLoseSceneDelay(false, -1, loadSceneDelay));
            foreach (var p in Players)
            {
                p.LockInput(10000);
                p.LockMovement(10000);
                p.Canvas.gameObject.SetActive(false);
            }
            foreach (var playerCamera in PlayerCameras)
                playerCamera.enabled = false;
            rocketCamera.enabled = true;
            //rocketCamera.GetComponent<CameraShaker>().Shake(10000);

            StartCoroutine(MoveCameraToPlayer(player1, rocketCamera, moveCameraDelay));
            StartCoroutine(MoveCameraToPlayer(player1, rocketCamera, moveCameraDelay + moveCameraDelay/2));
        }

        public int GetPlayerId(Player player)
        {
            return player == player1 ? 0 : 1;
        }

        public void WinGame(Player winPlayer)
        {
            UI.gameObject.SetActive(false);
            rocket.effects.StartFlight();
            foreach (var p in Players)
            {
                p.LockInput(10000);
                p.LockMovement(10000);
                p.Canvas.gameObject.SetActive(false);
            }
            
            winPlayer.gameObject.SetActive(false);
            foreach (var playerCamera in PlayerCameras)
                playerCamera.enabled = false;
            rocketCamera.enabled = true;
            //rocketCamera.GetComponent<CameraShaker>().Shake(10000);

            StartCoroutine(LoadWinOrLoseSceneDelay(true, GetPlayerId(winPlayer), loadSceneDelay));
            StartCoroutine(MoveCameraToPlayer(GetOtherPlayer(winPlayer), rocketCamera, moveCameraDelay));
        }

        private IEnumerator MoveCameraToPlayer(Player player, Camera camera, float timeInSeconds)
        {
            yield return new WaitForSeconds(timeInSeconds);
            camera.transform.position = player.transform.position;
        }

        private IEnumerator LoadWinOrLoseSceneDelay(bool isWin,int playerId, float timeInSeconds)
        {
            yield return new WaitForSeconds(timeInSeconds);
            WinOrLoseScene.Load(isWin, playerId);
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