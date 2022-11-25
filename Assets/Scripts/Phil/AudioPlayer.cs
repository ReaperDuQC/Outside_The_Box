using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioClip[] clips; 

    public void StopMusic()
    {
        StartCoroutine(Fade(musicSource.volume, 0f, 2f, true));
    }
    public void PlayMusic()
	{
        musicSource.volume = 0;
        musicSource.UnPause();
        StartCoroutine(Fade(0f, 1f, 2f, false));
	}
    IEnumerator Fade(float start, float end, float duration, bool stop)
    {
        float timer = 0;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start,end, timer / duration);
            yield return null;
        }

        musicSource.volume = end;

        if (stop)
        {
            musicSource.Pause();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
