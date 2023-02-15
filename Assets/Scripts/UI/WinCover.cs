using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class WinCover : Screen
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _confetti;
    [SerializeField] private float _enableAfterWin = 0.805f;

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _canGoToNextLevel;

    private void OnEnable()
    {
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
        OnEnableBase();
    }

    private void Start()
    {
        Close();
        _confetti.gameObject.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
        {
            GoToNextLevel();
        }
    }

    private void OnDisable()
    {
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
        OnDisableBase();
    }

    protected override void OnButtonClick()
    {
        GoToNextLevel();
    }

    private IEnumerator EnableCover()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_enableAfterWin);

        Open();
        _animator.enabled = true;
        _confetti.gameObject.SetActive(true);
        _canGoToNextLevel = true;
    }

    private void GoToNextLevel()
    {
        if (_canGoToNextLevel)
        {
            LevelLoader.Instance.LoadNext();
        }
    }

    private void PlayerMovementOnFinishReached()
    {
        EventsSender.Instance.SendLevelCompleteEvent(LevelLoader.Instance.LevelIndex + 1);
        StartCoroutine(EnableCover());
    }
}