using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string[] _levelNames;

    private int _sceneIndex;

    public event UnityAction<string> Loaded;
    public event UnityAction<string> Reloaded;

    public static LevelLoader Instance { get; private set; }
    public int LevelIndex { get; private set; }

    private string LevelName => _levelNames[_sceneIndex];

    public void LoadNext()
    {
        GameSettings.LevelIndex = ++LevelIndex;

        _sceneIndex = GetSceneIndex();
        Load();
        Loaded?.Invoke(LevelName);
    }

    public void Reload()
    {
        Load();
        Reloaded?.Invoke(LevelName);
    }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LevelIndex = GameSettings.LevelIndex;
    }

    private void Start()
    {
        _sceneIndex = GetSceneIndex();

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            return;
        }

        DontDestroyOnLoad(gameObject);
        Load();
        Loaded?.Invoke(LevelName);
    }

    private int GetSceneIndex()
    {
        return LevelIndex - ((LevelIndex / _levelNames.Length) * _levelNames.Length);
    }

    private void Load()
    {
        EventsSender.Instance.SendLevelStartEvent(LevelIndex + 1);
        SceneManager.LoadScene(LevelName);
    }
}