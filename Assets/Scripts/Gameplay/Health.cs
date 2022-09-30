using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private float scoreValue;
    public float GetHealth => health;
    
    [SerializeField] private bool applyCameraShake;
    public event Action OnDestroyed;
    public event Action OnDamage;
    public static event Action<float> OnPlayerHit;
    public static event Action<float> OnPlayerHealthInitialized;

    private void Start()
    {
        if (applyCameraShake)
        {
            Debug.Log("start");
            OnPlayerHealthInitialized?.Invoke(health);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        OnDamage?.Invoke();
        if (applyCameraShake)
        {
            OnPlayerHit?.Invoke(health);
        }
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            OnDestroyed?.Invoke();
            AudioManager.Instance.PlayExplosionSound();
            if (CompareTag("Enemy"))
            {
                ScoreKeeper.ModifyScore(scoreValue);
            }
            else
            {
               LevelManager.GameOver();
            }
            
            Destroy(gameObject);
        }
    }
}
