using CountDown;
using TMPro;
using UnityEngine;

public class WinLoseSceneDrawer : MonoBehaviour
{
    [SerializeField] private GameObject pers1;
    [SerializeField] private GameObject pers2;

    [SerializeField] private GameObject rip1;
    [SerializeField] private GameObject rip2;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    
    [SerializeField] private TMP_Text win0Text;
    [SerializeField] private TMP_Text win1Text;
    [SerializeField] private TMP_Text loseText;
    
    private void OnEnable()
    {
        scoreText.text = WinOrLoseScene.Score.ToString();
        var time = WinOrLoseScene.TimeInMinutes;
        timeText.text = $"{(int)time}:{(int)(time % 1 * 60f)}";
        
        if (!WinOrLoseScene.IsWin)
        {
            loseText.gameObject.SetActive(true);
            rip1.gameObject.SetActive(true);
            rip2.gameObject.SetActive(true);
            pers1.gameObject.SetActive(false);
            pers2.gameObject.SetActive(false);
        }
        else
        {
            if (WinOrLoseScene.PlayerId == 0)
            {
                rip1.gameObject.SetActive(false);
                rip2.gameObject.SetActive(true);
                pers1.gameObject.SetActive(true);
                pers2.gameObject.SetActive(false);

                win0Text.gameObject.SetActive(true);
            }
            else if (WinOrLoseScene.PlayerId == 1)
            {
                rip1.gameObject.SetActive(true);
                rip2.gameObject.SetActive(false);
                pers1.gameObject.SetActive(false);
                pers2.gameObject.SetActive(true);
                
                win1Text.gameObject.SetActive(true);
            }
        }
    }
}
