using UnityEngine;

internal static class GameSettings
{
    internal static bool SoundEnabled
    {
        get => PlayerPrefs.GetInt("SoundEnabled") == 1;
        set => PlayerPrefs.SetInt("SoundEnabled", value ? 1 : 0);
    }
}