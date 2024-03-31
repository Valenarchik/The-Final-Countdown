using UnityEngine.SceneManagement;

namespace CountDown
{
    public static class WinOrLoseScene
    {
        public static bool IsWin { get; private set; }
        public static int PlayerId { get; private set; } 

        public static void Load(bool isWin, int playerId)
        {
            PlayerId = playerId;
            IsWin = isWin;
            if (SceneTransition.Exist())
                SceneTransition.LoadScene(SceneName.WinOrLoseScene);
            else
            {
                SceneManager.LoadScene((int) SceneName.WinOrLoseScene);
            }
        }
    }
}