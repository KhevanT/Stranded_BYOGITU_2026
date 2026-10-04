using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource SFXSource;

    public AudioClip bgm;
    public AudioClip walk1;
    public AudioClip walk2;

    [SerializeField] private float bgmFadeInDuration = 3f;
    [SerializeField] private float musicVolume = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Gradually fades in bgm over time
    void PlayBGM()
    {
        musicSource.clip = bgm;
        musicSource.volume = 0f;
        musicSource.Play();

        StartCoroutine(FadeInMusic());
    }

    IEnumerator FadeInMusic()
    {
        float elapsed = 0f;

        while (elapsed < bgmFadeInDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / bgmFadeInDuration;

            // Ease-in: starts slowly, then gradually gets louder
            t = t * t;

            musicSource.volume = Mathf.Lerp(0f, musicVolume, t);

            yield return null;
        }

        musicSource.volume = musicVolume;
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
