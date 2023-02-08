using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private int _showAtLevel = 1;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Power _playerPower;
    [SerializeField] private int _showWhenPlayerPower;

    private void OnEnable()
    {
        _playerPower.Changed += PlayerPowerOnChanged;
    }

    private void Start()
    {
        if (LevelLoader.Instance.LevelIndex + 1 != _showAtLevel)
        {
            _playerPower.Changed -= PlayerPowerOnChanged;
        }
        else
        {
            if (_text.color.a > 0)
            {
                HideText();
            }
        }
    }

    private void OnDisable()
    {
        _playerPower.Changed -= PlayerPowerOnChanged;
    }

    private void DisplayText()
    {
        SetTextVisibility(true);
    }

    private void HideText()
    {
        SetTextVisibility(false);
    }

    private void PlayerPowerOnChanged(int initial, int current)
    {
        if (current < _showWhenPlayerPower)
        {
            return;
        }

        DisplayText();
    }

    private void SetTextVisibility(bool value)
    {
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, value ? 1 : 0);
    }
}