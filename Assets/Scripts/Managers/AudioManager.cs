using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Shooting")]
    [SerializeField] private AudioClip shooting;
    [SerializeField] [Range(0, 1)] private float playerShootingVolume;
    [SerializeField] [Range(0, 1)] private float AIShootingVolume;

    [Header("Explosion")] 
    [SerializeField] private AudioClip explosion;
    [SerializeField] [Range(0, 1)] private float explosionVolume;


    private Vector3 cameraPos;

    private static AudioManager instance;

    private void Awake()
    {
        ManageSingleton();
        if (Camera.main != null) cameraPos = Camera.main.transform.position;
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayShootingSound(bool AI)
    {
        if (shooting != null && !AI)
        {
            AudioSource.PlayClipAtPoint(shooting, cameraPos, playerShootingVolume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(shooting, cameraPos, AIShootingVolume);
        }
    }

    public void PlayExplosionSound()
    {
        if (explosion !=null)
        {
            AudioSource.PlayClipAtPoint(explosion, cameraPos, explosionVolume);
        }
    }
}
