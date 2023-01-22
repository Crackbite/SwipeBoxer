using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SceneField[] _levels;

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
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            return;
        }

        _sceneIndex = GetSceneIndex();

        DontDestroyOnLoad(gameObject);
        Load();
    }

    private int GetSceneIndex()
    {
        return LevelIndex - ((LevelIndex / _levels.Length) * _levels.Length);
    }

    private void Load()
    {
        SceneManager.LoadScene(_levels[_sceneIndex].Name);
    }
}