using Managers;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("Shooting")]
    [SerializeField] private AudioClip shooting;
    [SerializeField] [Range(0, 1)] private float playerShootingVolume;
    [SerializeField] [Range(0, 1)] private float AIShootingVolume;

    [Header("Explosion")] 
    [SerializeField] private AudioClip explosion;
    [SerializeField] [Range(0, 1)] private float explosionVolume;


    private Vector3 _cameraPos;

    private void Awake()
    {
        if (Camera.main != null) _cameraPos = Camera.main.transform.position;
    }


    public void PlayShootingSound(bool AI)
    { 
        AudioSource.PlayClipAtPoint(shooting, _cameraPos, 
            !AI ? playerShootingVolume : AIShootingVolume);
    }

    public void PlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(explosion, _cameraPos, explosionVolume);
    }
}
