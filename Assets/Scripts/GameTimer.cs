using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startSeconds = 60f;
    public TextMeshProUGUI timerText;
    private float remaining;
    private float elapsed;
    public static bool timeOver;
    private bool stopped;

    void Start()
    {
        remaining = startSeconds;
        timeOver = false;
        stopped = false;
        elapsed = 0f;
        UpdateUI();
    }

    void Update()
    {
        if (GameController.gameOver){
            stopped = true;
        }

        if (stopped || timeOver) return;

        elapsed += Time.deltaTime;

        remaining -= Time.deltaTime;
        if (remaining <= 0f)
        {
            remaining = 0f;
            timeOver = true;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timerText != null) timerText.text = Format(remaining);
    }

    string Format(float t)
    {
        if (t < 0f) t = 0f;
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        return m.ToString("00") + ":" + s.ToString("00");
    }

    public void StopTimer() { stopped = true; }

    public string CurrentFormatted(){
        return Format(remaining);
    }

    public string ElapsedFormatted(){
        return Format(elapsed);
    }

    public void AddBonus(float seconds)
    {
        if (timeOver || stopped) return;

        remaining += seconds;
        if (remaining < 0f) remaining = 0f;
        UpdateUI();
    }
}
