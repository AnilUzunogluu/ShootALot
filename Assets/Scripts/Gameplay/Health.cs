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
    private LevelManager _levelManager;
    public event Action OnDestroyed;
    public event Action OnDamage;

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
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
        OnDamage?.Invoke();
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
                Destroy(gameObject);
            }
            else
            {
               _levelManager.LoadGameOver();
               Destroy(gameObject);
            }
        }
    }
}
