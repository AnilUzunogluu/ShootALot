using UnityEngine;

public class AudioManager : Managers.Singleton<AudioManager>
{

    [Header("Shooting")]
    [SerializeField] private AudioClip shooting;
    [SerializeField] [Range(0, 1)] private float shootingVolume;

    [Header("Explosion")] 
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip bossExplosion;
    [SerializeField] [Range(0, 1)] private float explosionVolume;

    [Header("GameState")] 
    [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip game;
    [SerializeField] private AudioClip gameOver;

    private AudioSource _musicPlayer;

    private void Start()
    {
        _musicPlayer = GetComponent<AudioSource>();
    }


    private Vector3 _cameraPos;

    private void Awake()
    {
        _cameraPos = Camera.main.transform.position;
        LevelManager.Instance.SwitchGameMusic += PlayGameMusic;
    }


    public void PlayShootingSound(bool AI)
    {
        if (!AI)
        {
            AudioSource.PlayClipAtPoint(shooting, _cameraPos, shootingVolume);
        }
    }

    public void PlayExplosionSound(string objectTag)
    {
        AudioSource.PlayClipAtPoint(objectTag == "Boss" ? bossExplosion : explosion, _cameraPos, explosionVolume);
    }

    private void PlayGameMusic(string sceneName)
    {
        if (_musicPlayer == null) return;
        switch (sceneName)
        {
            case "Game":
                _musicPlayer.clip = game;
                _musicPlayer.Play();
                break;
            
            case "Game Over":
                _musicPlayer.clip = gameOver;
                _musicPlayer.Play();
                break;
            
            case "Main Menu":
                _musicPlayer.clip = mainMenu;
                _musicPlayer.Play();
                break;
            default:
                break;
        }
    }
    
}
