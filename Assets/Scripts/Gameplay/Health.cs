using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 50f;
    [SerializeField] private float scoreValue;
    
    [SerializeField] private bool applyCameraShake;
    public event Action OnDestroyed;
    public static event Action<float> OnPlayerHit;
    public static event Action<float> OnPlayerHealthInitialized;
    
    private void Start()
    {
        if (applyCameraShake)
        {
            OnPlayerHealthInitialized?.Invoke(health);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
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
            PlayDeathFX(gameObject.tag);
            if (CompareTag("Enemy"))
            {
                ScoreKeeper.ModifyScore(scoreValue);
            }
            else if (CompareTag("Boss"))
            {
                LevelManager.Instance.isGameWon = true; 
                ScoreKeeper.ModifyScore(scoreValue);
                LevelManager.GameOver();
            }
            else
            {
                LevelManager.Instance.isGameWon = false;
               LevelManager.GameOver();
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(gameObject.tag) && col.CompareTag("Enemy"))
        {
            PlayDeathFX(gameObject.tag);
            Destroy(col.gameObject);
        } 
    }

    private void PlayDeathFX(string objectTag)
    {
        OnDestroyed?.Invoke();
        AudioManager.Instance.PlayExplosionSound(objectTag);
    }
}
