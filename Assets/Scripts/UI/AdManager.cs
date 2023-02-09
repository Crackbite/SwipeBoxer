using System.Linq;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private AudioMixerSnapshot _normal;
    [SerializeField] private AudioMixerSnapshot _mute;
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
        if (value)
        {
            _normal.TransitionTo(0f);
        }
        else
        {
            _mute.TransitionTo(0f);
        }

        Time.timeScale = value ? 1 : 0;
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