using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    internal bool disable;

    void Start()
    {
        DontDestroyOnLoad(this);
        //GM = FindObjectOfType<GameMaster>();
        audioSource = GetComponent<AudioSource>();

        //audioSource.loop = true;
        //audioSource.Stop();

        disable = false;
    }

    internal void Play(float time = 0)
    {
        StopAllCoroutines();
        if (!disable && !audioSource.isPlaying)
        {
            StartCoroutine(UnmuteAndStartPlaying(time));
        }
    }

    private IEnumerator UnmuteAndStartPlaying(float time)
    {
        yield return new WaitForSeconds(time);
        Unmute();
        yield return new WaitForSeconds(0.5f);
        audioSource.Play();
    }

    internal void Stop(float time = 0)
    {
        StopAllCoroutines();
        if (audioSource.isPlaying)
        {
            StartCoroutine(MuteAndStopPlaying(time));
        }
    }

    private IEnumerator MuteAndStopPlaying(float time)
    {
        yield return new WaitForSeconds(time);
        Mute();
        yield return new WaitForSeconds(0.5f);
        audioSource.Stop();
    }

    internal void Unmute()
    {
        if (Mathf.Approximately(audioSource.volume, 0))
        {
            StartCoroutine(ChangeVolumeOverTime(1, 0.5f));
        }
    }

    internal void Mute()
    {
        if (!Mathf.Approximately(audioSource.volume, 0))
        {
            StartCoroutine(ChangeVolumeOverTime(0, 0.5f));
        }
    }

    private IEnumerator ChangeVolumeOverTime(float target, float time)
    {
        float t = 0;

        float baseVolume = audioSource.volume;

        while (t < time)
        {
            t += Time.deltaTime;

            audioSource.volume = Mathf.Lerp(baseVolume, target, t / time);
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }
}
