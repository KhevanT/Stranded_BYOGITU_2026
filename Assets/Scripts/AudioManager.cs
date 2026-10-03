using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource SFXSource;

    public AudioClip bgm;
    public AudioClip walk1;
    public AudioClip walk2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayBGM()
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }

    void StopBGM()
    {
        musicSource.Stop();
    }

    void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
