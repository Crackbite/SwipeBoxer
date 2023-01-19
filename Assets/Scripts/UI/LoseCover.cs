using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        StartCoroutine(EnableCover());
    }
}