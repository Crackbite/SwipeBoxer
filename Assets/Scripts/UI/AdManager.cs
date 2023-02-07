using System.Linq;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private string[] _excludeOnLevels;
    [SerializeField] private bool _outputInfoToLog;

    private void OnEnable()
    {
        _levelLoader.Loaded += LevelOnLoaded;
        _levelLoader.Reloaded += LevelOnLoaded;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        _levelLoader.Loaded -= LevelOnLoaded;
        _levelLoader.Reloaded -= LevelOnLoaded;
    }

    private void EnableGame(bool value)
    {
        Time.timeScale = value ? 1 : 0;

        AudioListener.pause = value == false;
        AudioListener.volume = value ? 1f : 0f;
    }

    private void LevelOnLoaded(string levelName)
    {
        if (_excludeOnLevels.Contains(levelName))
        {
            return;
        }

        if (_outputInfoToLog)
        {
            Debug.Log("Ad display");
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

#pragma warning disable CS0162
        InterstitialAd.Show(OnOpenAd, OnCloseAd);
#pragma warning restore CS0162
    }

    private void OnCloseAd(bool wasShown)
    {
        EnableGame(true);
    }

    private void OnOpenAd()
    {
        EnableGame(false);
    }
}