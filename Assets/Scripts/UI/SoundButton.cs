using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;
    [SerializeField] private AudioMixerSnapshot _normal;
    [SerializeField] private AudioMixerSnapshot _mute;
    [SerializeField] private float _timeTransitionToMute = 0.5f;

    private Button _button;
    private Image _image;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSoundClick);
    }

    private void Start()
    {
        SetActualSprite();
        SetStartSnapshot();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSoundClick);
    }

    private void OnSoundClick()
    {
        if (GameSettings.SoundEnabled)
        {
            TurnOffSound();
        }
        else
        {
            TurnOnSound();
        }

        SetActualSprite();
    }

    private void SetActualSprite()
    {
        _image.sprite = GameSettings.SoundEnabled ? _on : _off;
        _image.SetNativeSize();
    }

    private void SetStartSnapshot()
    {
        if (GameSettings.SoundEnabled)
        {
            _normal.TransitionTo(0f);
        }
        else
        {
            _mute.TransitionTo(0f);
        }
    }

    private void TurnOffSound()
    {
        GameSettings.SoundEnabled = false;
        _mute.TransitionTo(_timeTransitionToMute);
    }

    private void TurnOnSound()
    {
        GameSettings.SoundEnabled = true;
        _normal.TransitionTo(_timeTransitionToMute);
    }
}