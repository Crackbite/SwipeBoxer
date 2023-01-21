using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Platform))]
public class TutorialPlatform : MonoBehaviour
{
    [SerializeField] private Image _goodChoice;
    [SerializeField] private Image _badChoice;
    [SerializeField] private Power _playerPower;
    [SerializeField] private int _showAtPower;

    private Image _currentInstance;
    private Platform _platform;
    private PowerCanvas _powerCanvas;
    private bool _readyForShow;

    private void OnEnable()
    {
        _playerPower.Changed += PlayerPowerOnChanged;
    }

    private void Start()
    {
        _platform = GetComponent<Platform>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_readyForShow)
        {
            return;
        }

        var enemy = collision.GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            return;
        }

        _powerCanvas = enemy.GetComponentInChildren<PowerCanvas>();
        _readyForShow = _powerCanvas != null;
    }

    private void OnDisable()
    {
        _playerPower.Changed -= PlayerPowerOnChanged;
    }

    private void PlayerPowerOnChanged(int initial, int current)
    {
        if (_readyForShow == false || current < _showAtPower)
        {
            return;
        }

        if (_currentInstance != null)
        {
            Destroy(_currentInstance);
        }

        if (current >= _platform.Power)
        {
            _currentInstance = Instantiate(_goodChoice, _powerCanvas.transform);

            var powerImage = _powerCanvas.GetComponentInChildren<Image>();
            powerImage.color = new Color(
                _goodChoice.color.r,
                _goodChoice.color.g,
                _goodChoice.color.b,
                powerImage.color.a);

            _playerPower.Changed -= PlayerPowerOnChanged;
        }
        else
        {
            _currentInstance = Instantiate(_badChoice, _powerCanvas.transform);
        }
    }
}