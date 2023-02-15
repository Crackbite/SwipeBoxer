using System.Collections;
using Agava.YandexGames;
using GameAnalyticsSDK;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        StartGame();
        yield break;
#endif

#pragma warning disable CS0162
        yield return YandexGamesSdk.Initialize();

        StartGame();
    }

    private void StartGame()
    {
        GameAnalytics.Initialize();
        EventsSender.Instance.SendGameStartEvent();

        _levelLoader.enabled = true;
    }
}