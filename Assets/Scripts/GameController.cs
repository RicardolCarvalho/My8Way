using UnityEngine;

public class GameController
{
    private static int collectableCount;
    private static bool started;

    public static bool gameOver
    {
        get { return started && collectableCount <= 0; }
    }

    public static void Init(int initialCount)
    {
        collectableCount = initialCount;
        started = true;
    }

    public static void Init()
    {
        Init(4);
    }

    public static void Collect()
    {
        collectableCount--;
    }
}

