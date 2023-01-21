using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNameChanger : MonoBehaviour
{
    [SerializeField] private LeanToken _token;

    private void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        _token.SetValue(level.ToString());
    }
}