using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string[] _levelNames;

    private int _sceneIndex;

    public static LevelLoader Instance { get; private set; }

    public int LevelIndex { get; private set; }

    public void LoadNext()
    {
        GameSettings.LevelIndex = ++LevelIndex;

        _sceneIndex = GetSceneIndex();
        Load();
    }

    public void Reload()
    {
        Load();
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
    }

    private int GetSceneIndex()
    {
        return LevelIndex - ((LevelIndex / _levelNames.Length) * _levelNames.Length);
    }

    private void Load()
    {
        SceneManager.LoadScene(_levelNames[_sceneIndex]);
    }
}