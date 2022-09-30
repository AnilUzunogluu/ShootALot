using UnityEngine;

namespace Raiken.ShooterShip
{
    [CreateAssetMenu(fileName = "ShooterSystemReferences", menuName = "New ShooterSystemReferences")]
    public class ShooterSystemReferences : ScriptableObject
    {
        public GameObject projectile;
        public float projectileSpeed;
        public float projectileLifeTime;
        public float fireRate;
        public float fireRateAIMin;
        public float fireRateAIMax;
    }
}