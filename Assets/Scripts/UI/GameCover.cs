using System.Collections;
using UnityEngine;

public class GameCover : Screen
{
    [SerializeField] private float _enableAfter = 1.13f;

    private void Start()
    {
        Close();
        StartCoroutine(EnableGameCover());
    }

    protected override void OnButtonClick()
    {
        LevelLoader.Instance.Reload();
    }

    private IEnumerator EnableGameCover()
    {
        yield return new WaitForSeconds(_enableAfter);
        Open();
    }
}