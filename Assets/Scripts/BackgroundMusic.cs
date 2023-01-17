using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] _music;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _playerMovement.FinishReached += GameEnd;
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
        _playerMovement.FinishReached -= GameEnd;
        _player.Died -= GameEnd;
    }

    private AudioClip GetMusicClip()
    {
        int randomMusicIndex = Random.Range(0, _music.Length);
        return _music[randomMusicIndex];
    }

    private void GameEnd()
    {
        _audioSource.Stop();
    }
}