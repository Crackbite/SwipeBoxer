using TMPro;
using UnityEngine;

public class PowerVisualizer : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private Vector3 _increaseTextScale = new Vector3(1.8f, 1.8f, 1.8f);
    [SerializeField] private float _increaseTextSpeed = 8f;

    private Vector3 _initalTextScale;
    private bool _isStart;
    private bool _isTextScalable;
    private bool _textIncreaed;

    private void OnEnable()
    {
        _power.Changed += PowerOnChanged;
    }

    private void Start()
    {
        _initalTextScale = _displayText.transform.localScale;
        _isStart = true;
    }

    private void Update()
    {
        if (_isTextScalable == false)
        {
            return;
        }

        if (_textIncreaed == false && _displayText.transform.localScale != _increaseTextScale)
        {
            SetNewTextScale(_displayText.transform.localScale, _increaseTextScale);
            return;
        }

        _textIncreaed = true;

        if (_displayText.transform.localScale != _initalTextScale)
        {
            SetNewTextScale(_displayText.transform.localScale, _initalTextScale);
            return;
        }

        _isTextScalable = false;
        _textIncreaed = false;
    }

    private void OnDisable()
    {
        _power.Changed -= PowerOnChanged;
    }

    private void PowerOnChanged(int initialPower, int currentPower)
    {
        _displayText.text = currentPower.ToString();
        _isTextScalable = _isStart && currentPower > initialPower;
    }

    private void SetNewTextScale(Vector3 current, Vector3 target)
    {
        _displayText.transform.localScale = Vector3.MoveTowards(current, target, _increaseTextSpeed * Time.deltaTime);
    }
}