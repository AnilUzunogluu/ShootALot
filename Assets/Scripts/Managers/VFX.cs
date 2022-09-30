using Unity.Mathematics;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionVFX;

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
       var instance =  Instantiate(ExplosionVFX, transform.position, quaternion.identity);
       Destroy(instance.gameObject, 3f);
    }
}
