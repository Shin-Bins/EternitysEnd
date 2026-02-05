using UnityEngine;

public class RandomAmbientSound : MonoBehaviour
{
    public AudioClip[] soundClips;
    public float minDelay = 3f;
    public float maxDelay = 10f;

    private AudioSource audioSource;
    private float nextPlayTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        ScheduleNextSound();
    }

    void Update()
    {
        if (Time.time >= nextPlayTime && !audioSource.isPlaying)
        {
            PlayRandomSound();
            ScheduleNextSound();
        }
    }

    void PlayRandomSound()
    {
        if (soundClips.Length > 0)
        {
            // Pick random clip and assign it to the audio source
            audioSource.clip = soundClips[Random.Range(0, soundClips.Length)];
            audioSource.Play();  // Use Play() instead of PlayOneShot()
        }
    }

    void ScheduleNextSound()
    {
        float delay = Random.Range(minDelay, maxDelay);
        nextPlayTime = Time.time + delay;
    }
}