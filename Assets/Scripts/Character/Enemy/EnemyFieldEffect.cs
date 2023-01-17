using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyFieldEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _visualEffect;
    [SerializeField] private AudioClip _soundEffect;
    [SerializeField] private AudioSource _audioSource;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
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

        _audioSource.pitch = 1f;
        _audioSource.clip = _soundEffect;

        _audioSource.Play();
    }
}