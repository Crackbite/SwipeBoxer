using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class LoseCover : Screen
{
    [SerializeField] private Player _player;
    [SerializeField] private float _enableAfterLose = 0.805f;

    private Animator _animator;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _player.Died += PlayerOnDied;
        OnEnableBase();
    }

    private void Start()
    {
        Close();

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void OnDisable()
    {
        _player.Died -= PlayerOnDied;
        OnDisableBase();
    }

    protected override void OnButtonClick()
    {
        LevelLoader.Instance.Reload();
    }

    private IEnumerator EnableCover()
    {
        yield return new WaitForSeconds(_enableAfterLose);

        _audioSource.Play();
        Open();
        _animator.enabled = true;
    }

    private void PlayerOnDied()
    {
        EventsSender.Instance.SendLevelFailEvent(LevelLoader.Instance.LevelIndex + 1);
        StartCoroutine(EnableCover());
    }
}