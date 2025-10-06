using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI finalTimeText; 
    public TextMeshProUGUI finalScoreText;

    bool shown;
    GameTimer timer;

    void Start()
    {
        timer = Object.FindFirstObjectByType<GameTimer>();
        if (endGamePanel != null) endGamePanel.SetActive(false);
        shown = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (shown) return;

        if (GameController.gameOver || GameTimer.timeOver)
        {
            if (timer != null)
            {
                timer.StopTimer();
                // esconder HUD do timer
                if (timer.timerText != null)
                    timer.timerText.gameObject.SetActive(false);
            }

            if (endGamePanel != null) endGamePanel.SetActive(true);

            if (finalTimeText != null && timer != null)
                finalTimeText.text = "Tempo total: " + timer.ElapsedFormatted();

            if (finalScoreText != null)
                finalScoreText.text = "Coletados: " + GameController.Score;

            Time.timeScale = 0f;
            shown = true;
        }
    }
}
