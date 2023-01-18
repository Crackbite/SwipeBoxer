using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] _music;
    [SerializeField] private AudioMixerSnapshot _endGame;
    [SerializeField] private float _timeTransitionToEndGame = 3f;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _playerMovement.LastHitInitiated += GameEnd;
        _player.Died += GameEnd;
    }

    private void Start()
    {
        if (_music.Length < 1)
        {
            return;
        }

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = GetMusicClip();
        _audioSource.Play();
    }

    private void OnDisable()
    {
        _playerMovement.LastHitInitiated -= GameEnd;
        _player.Died -= GameEnd;
    }

    private void GameEnd()
    {
        if (GameSettings.SoundDisabled == false)
        {
            _endGame.TransitionTo(_timeTransitionToEndGame);
        }
    }

    private AudioClip GetMusicClip()
    {
        int randomMusicIndex = Random.Range(0, _music.Length);
        return _music[randomMusicIndex];
    }
}