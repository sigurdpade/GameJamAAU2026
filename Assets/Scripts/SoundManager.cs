using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Volume")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    float lastSFXtime = 0f;
    float sfxCooldown = 0.05f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sfxSource.volume = sfxVolume;
        musicSource.volume = musicVolume;
    }

    public void PlaySFX(AudioClip clip, bool randomizePitch = false)
    {
        if (clip == null) return;
        if (Time.time - lastSFXtime < sfxCooldown) return;

        lastSFXtime = Time.time;

        float originalPitch = sfxSource.pitch;

        sfxSource.pitch = randomizePitch ? Random.Range(0.9f, 1.1f) : 1f;
        sfxSource.PlayOneShot(clip, sfxVolume);

        sfxSource.pitch = originalPitch;
    }

    public void PlayImportantSFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(clip, sfxVolume * 1.2f);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicSource.clip == music) return;

        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
