using UnityEngine.SceneManagement;

namespace CountDown
{
    public static class WinOrLoseScene
    {
        public static bool IsWin { get; private set; }
        public static int PlayerId { get; private set; } 
        public static int Score { get; private set; }
        public static float TimeInMinutes { get; private set; }

        public static void Load(bool isWin, int playerId, int score, float time)
        {
            PlayerId = playerId;
            IsWin = isWin;
            Score = score;
            TimeInMinutes = time;
            
            if (SceneTransition.Exist())
                SceneTransition.LoadScene(SceneName.WinOrLoseScene);
            else
            {
                SceneManager.LoadScene((int) SceneName.WinOrLoseScene);
            }
        }
    }
}