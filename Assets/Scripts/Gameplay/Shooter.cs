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
    [SerializeField] private float minFireRate;
    [SerializeField] private float maxFireRate;
    private float _fireRateAI; 
    
    
    private Coroutine _firingCoroutine;
    private bool _firingInProgress;
    private AudioManager _audioManager;


    private void Start()
    {
        _audioManager = AudioManager.Instance;

        if (useAI)
        {
            _fireRateAI = Random.Range(minFireRate, maxFireRate);
            StartCoroutine(FireContinuously(_fireRateAI));
        }
        else
        {
            StartCoroutine(FireContinuously(fireRate)); // x => Fire(x, _fireRate);
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
