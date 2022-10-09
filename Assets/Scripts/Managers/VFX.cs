using Unity.Mathematics;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] private GameObject explosionVFX;

    private void OnEnable()
    {
        GetComponent<Health>().OnDestroyed += PlayExplosionVFX;
    }
    private void OnDisable()
    {
        GetComponent<Health>().OnDestroyed -= PlayExplosionVFX;
    }

    private void PlayExplosionVFX()
    {
       var instance =  Instantiate(explosionVFX, transform.position, quaternion.identity);
       Destroy(instance.gameObject, 3f);
    }
}
