using UnityEngine;

internal static class GameSettings
{
    internal static int LevelIndex
    {
        get => PlayerPrefs.GetInt("LevelIndex");
        set => PlayerPrefs.SetInt("LevelIndex", value);
    }

    internal static int Sessions
    {
        get => PlayerPrefs.GetInt("Sessions");
        set => PlayerPrefs.SetInt("Sessions", value);
    }

    internal static bool SoundDisabled
    {
        get => PlayerPrefs.GetInt("SoundDisabled") == 1;
        set => PlayerPrefs.SetInt("SoundDisabled", value ? 1 : 0);
    }
}