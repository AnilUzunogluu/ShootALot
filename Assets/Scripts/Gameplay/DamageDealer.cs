using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] private float damage = 10f;

    public float Damage => damage;

    public void Hit()
    {
        Destroy(gameObject);
    }
}
