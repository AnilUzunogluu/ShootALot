using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private float fireRate = 1f;
    
    [Header("AI")]
    [SerializeField] private bool useAI; 
    private float fireRateAI; 
    
    [HideInInspector] public bool isFiring;

    private Coroutine _firingCoroutine;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (useAI)
        {
            isFiring = true;
            fireRateAI = Random.Range(0.9f, 1.5f);
        }
    }

    private void Update()
    {
     Fire();
    }

    private void Fire()
    {
        if (isFiring && _firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && _firingCoroutine != null)
        {
            StopCoroutine(_firingCoroutine);
            _firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        do
        {
            _audioManager.PlayShootingSound(useAI);
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
            Destroy(instance, projectileLifeTime);
            if (useAI)
            {
                yield return new WaitForSeconds(fireRateAI);
            }
            else
            {
                yield return new WaitForSeconds(fireRate);
            }
            
        } while (true);
    }
}
