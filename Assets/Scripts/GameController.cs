using UnityEngine;

public class GameController
{
    private static int score;
    private static bool started;
    public static bool gameOver { get { return false; } }
    public static void Init() { score = 0; started = true; }
    public static void Init(int _) { Init(); }
    public static void AddScore(int amount)
    {
        score += amount;
        if (score < 0) score = 0;
    }

    public static void Collect() { AddScore(1); }

    public static int Score { get { return score; } }
}

