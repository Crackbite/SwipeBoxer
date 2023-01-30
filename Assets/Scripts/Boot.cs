using GameAnalyticsSDK;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;

    private void Start()
    {
        GameAnalytics.Initialize();
        EventsSender.Instance.SendGameStartEvent();

        _levelLoader.enabled = true;
    }
}