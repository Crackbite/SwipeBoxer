using UnityEngine;

internal static class GameSettings
{
    internal static bool SoundDisabled
    {
        get => PlayerPrefs.GetInt("SoundDisabled") == 1;
        set => PlayerPrefs.SetInt("SoundDisabled", value ? 1 : 0);
    }
}