using System;
using GameAnalyticsSDK;
using UnityEngine;

public class EventsSender : MonoBehaviour
{
    private float _realtimeSinceLevelStart;

    public static EventsSender Instance { get; private set; }

    public void SendGameStartEvent()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game", ++GameSettings.Sessions);
    }

    public void SendLevelCompleteEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {level}", GetElapsedSeconds());
    }

    public void SendLevelFailEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level {level}", GetElapsedSeconds());
    }

    public void SendLevelRestartEvent(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Restart {level}", GetElapsedSeconds());
    }

    public void SendLevelStartEvent(int level)
    {
        _realtimeSinceLevelStart = Time.realtimeSinceStartup;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level {level}");
    }

    public void SendSoundDisableEvent()
    {
        GameAnalytics.NewDesignEvent("Sound:Disable");
    }

    public void SendSoundEnableEvent()
    {
        GameAnalytics.NewDesignEvent("Sound:Enable");
    }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private int GetElapsedSeconds()
    {
        return Convert.ToInt32(Time.realtimeSinceStartup - _realtimeSinceLevelStart);
    }
}