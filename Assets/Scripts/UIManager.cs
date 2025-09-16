using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public Text finalTimeText;

    void FixedUpdate()
    {
        if (GameController.gameOver || GameTimer.timeOver)
        {
            endGamePanel.SetActive(true);

            GameTimer timer = Object.FindFirstObjectByType<GameTimer>();
            if (finalTimeText != null && timer != null)
            {
                finalTimeText.text = "Tempo final: " + timer.CurrentFormatted();
                timer.StopTimer();
            }
        }        
    }

    string GameTimerText()
    {
        GameTimer timer = Object.FindFirstObjectByType<GameTimer>();
        if (timer != null)
        {
            return timer.timerText.text;
        }
        return "00:00";
    }
}
