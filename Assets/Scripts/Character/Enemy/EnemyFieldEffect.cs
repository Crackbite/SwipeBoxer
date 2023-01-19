using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(AudioSource))]
public class EnemyFieldEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _visualEffect;
    [SerializeField] private AudioClip _soundEffect;

    private AudioSource _audioSource;
    private float _defaultPitch;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _audioSource = GetComponent<AudioSource>();

        _defaultPitch = _audioSource.pitch;
    }

    private void OnEnable()
    {
        _enemy.Won += EnemyOnWon;
    }

    private void OnDisable()
    {
        _enemy.Won -= EnemyOnWon;
    }

    private void CreateEffect(Vector3 position)
    {
        if (_visualEffect != null)
        {
            Instantiate(_visualEffect, position, Quaternion.identity);
        }
    }

    private void EnemyOnWon(Vector3 position)
    {
        PlaySound();
        CreateEffect(position);
    }

    private void PlaySound()
    {
        if (_soundEffect == null)
        {
            return;
        }

        _audioSource.pitch = _defaultPitch;
        _audioSource.clip = _soundEffect;

        _audioSource.Play();
    }
}