using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 50f;


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
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
