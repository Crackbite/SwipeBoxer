using Lean.Localization;
using UnityEngine;

public class LevelNameChanger : MonoBehaviour
{
    [SerializeField] private LeanToken _token;

    private void Awake()
    {
        int level = LevelLoader.Instance.LevelIndex + 1;
        _token.SetValue(level.ToString());
    }
}