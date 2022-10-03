using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] private float damage = 10f;

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Enemy") || col.CompareTag("Boss"))
        {
            var health = col.GetComponent<Health>();
            health.TakeDamage(damage);
        }

        if (CompareTag("Projectile") && !col.CompareTag("Projectile"))
        {
            Hit();
        }
    }
    
    private void Hit()
    {
        Destroy(gameObject);
    }
}
