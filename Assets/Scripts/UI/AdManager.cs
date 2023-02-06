using System.Linq;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private string[] _excludeOnLevels;

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

    private void LevelOnLoaded(string levelName)
    {
        if (_excludeOnLevels.Contains(levelName))
        {
            return;
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

#pragma warning disable CS0162
        InterstitialAd.Show(() => Time.timeScale = 0, _ => Time.timeScale = 1);
#pragma warning restore CS0162
    }
}