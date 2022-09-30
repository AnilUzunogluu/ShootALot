using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private float _fireRate = 1f;
    
    [Header("AI")]
    [SerializeField] private bool useAI; 
    private float fireRateAI; 
    
    private Coroutine _firingCoroutine;
    private bool _firingInProgress;
    private AudioManager _audioManager;


    private void Start()
    {
        _audioManager = AudioManager.Instance;

        if (useAI)
        {
            fireRateAI = Random.Range(0.9f, 1.5f);
            Fire(true, fireRateAI);
        }
        else
        {
            GetComponent<Player>().OnFiringEvent += OnFired; // x => Fire(x, _fireRate);
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
            _firingCoroutine = StartCoroutine(FireContinuously(fireRate));
            _firingInProgress = true;
        }
        else if (!isFiring && _firingInProgress)
        {
            StopCoroutine(_firingCoroutine);
            _firingInProgress = false;
        }
    }

    IEnumerator FireContinuously(float fireRate)
    {
        do
        {
            _audioManager.PlayShootingSound(useAI);
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
            Destroy(instance, projectileLifeTime);
            yield return new WaitForSeconds(fireRate);
        } while (true);
    }
}
