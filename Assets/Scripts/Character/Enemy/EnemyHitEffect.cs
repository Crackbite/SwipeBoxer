using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(AudioSource))]
public class EnemyHitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _visualEffect;
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private float _hitVolume = 0.4f;
    [SerializeField] private AudioClip _ouchSound;
    [SerializeField] private float _ouchVolume = 1f;
    [SerializeField] private float _pitchMin = 0.8f;
    [SerializeField] private float _pitchMax = 1.3f;

    private Enemy _enemy;
    private AudioSource _audioSource;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _enemy.PreDied += EnemyOnPreDied;
    }

    private void OnDisable()
    {
        _enemy.PreDied -= EnemyOnPreDied;
    }

    private void CreateEffect(Vector3 position)
    {
        if (_visualEffect != null)
        {
            Instantiate(_visualEffect, position, Quaternion.identity);
        }
    }

    private void EnemyOnPreDied(Vector3 lastHitPosition)
    {
        PlaySound();
        CreateEffect(lastHitPosition);
    }

    private void PlaySound()
    {
        _audioSource.pitch = Random.Range(_pitchMin, _pitchMax);

        if (_hitSounds.Length > 0)
        {
            int randomHitEffectIndex = Random.Range(0, _hitSounds.Length);
            _audioSource.PlayOneShot(_hitSounds[randomHitEffectIndex], _hitVolume);
        }

        if (_ouchSound != null)
        {
            _audioSource.PlayOneShot(_ouchSound, _ouchVolume);
        }
    }
}