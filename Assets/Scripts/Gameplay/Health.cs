using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private float scoreValue;
    public float GetHealth => health;
    
    [SerializeField] private bool applyCameraShake;
    private CameraShake _cameraShake;

    private AudioManager _audioManager;
    private ScoreKeeper _scoreKeeper;
    public event Action OnDestroyed;

    private void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _audioManager = FindObjectOfType<AudioManager>();
        _cameraShake = FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DamageDealer damageDealer = col.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.Damage);
            damageDealer.Hit();
        }
    }


    private void TakeDamage(float damage)
    {
        health -= damage;
        ShakeCamera();
        CheckDeath();
    }

    private void ShakeCamera()
    {
        if (applyCameraShake)
        {
            _cameraShake.Play();
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            _audioManager.PlayExplosionSound();
            OnDestroyed?.Invoke();
            if (CompareTag("Enemy"))
            {
                _scoreKeeper.ModifyScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}
