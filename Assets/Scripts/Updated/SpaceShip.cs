using UnityEngine;

namespace Raiken.ShooterShip
{
    public class SpaceShip : MonoBehaviour
    {
        [SerializeField] private ShooterSystemReferences shooterSystemReferences;
        [SerializeField] private Player player;

        private ShooterUpdated _shooterUpdated;


        private void Awake()
        {
            _shooterUpdated = new ShooterUpdated(this, shooterSystemReferences, FindObjectOfType<AudioManager>(), player != null ? "Player" : "Enemy", player);
        }
    }
}