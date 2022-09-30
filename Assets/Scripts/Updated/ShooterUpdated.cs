using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Raiken.ShooterShip
{
    public class ShooterUpdated
    {
        public ShooterUpdated(MonoBehaviour monoBehaviour, ShooterSystemReferences shooterSystemReferences,
            AudioManager audioManager, string TargetLayer, Player player = null)
        {
            _monoBehaviour = monoBehaviour;
            
            _projectile = shooterSystemReferences.projectile;
            _projectileSpeed = shooterSystemReferences.projectileSpeed;
            _projectileLifeTime = shooterSystemReferences.projectileLifeTime;
            _fireRate = shooterSystemReferences.fireRate;
            _useAI = player == null;
            _player = player;
            _fireRateAI = Random.Range(shooterSystemReferences.fireRateAIMin, shooterSystemReferences.fireRateAIMax);
            _audioManager = audioManager;
            _targetLayer = TargetLayer;
            Initialize();
        }


        private readonly GameObject _projectile;
        private readonly float _projectileSpeed;
        private readonly float _projectileLifeTime;
        private readonly float _fireRate;

        private readonly bool _useAI;
        private readonly float _fireRateAI;

        private Coroutine _firingCoroutine;
        private bool _firingInProgress;
        private readonly AudioManager _audioManager;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly Player _player;
        private readonly string _targetLayer;

        private void Initialize()
        {
            if (_useAI)
            {
                Fire(true, _fireRateAI);
            }
            else
            {
                _player.OnFiringEvent += OnFired; // x => Fire(x, _fireRate);
            }
        }

        private void OnFired(bool isFiring)
        {
            Fire(isFiring, _fireRate);
        }

        private void Fire(bool isFiring, float fireRate)
        {
            if (isFiring && !_firingInProgress)
            {
                _firingCoroutine = _monoBehaviour.StartCoroutine(FireContinuously(fireRate));
                _firingInProgress = true;
            }
            else if (!isFiring && _firingInProgress)
            {
                _monoBehaviour.StopCoroutine(_firingCoroutine);
                _firingInProgress = false;
            }
        }

        IEnumerator FireContinuously(float fireRate)
        {
            do
            {
                _audioManager.PlayShootingSound(_useAI);
                GameObject instance = Object.Instantiate(_projectile, _monoBehaviour.transform.position, Quaternion.identity);
                instance.GetComponent<Rigidbody2D>().velocity = Vector2.up * _projectileSpeed;
                instance.layer = LayerMask.NameToLayer(_targetLayer);
                Object.Destroy(instance, _projectileLifeTime);
                yield return new WaitForSeconds(fireRate);
            } while (true);
        }
    }
}