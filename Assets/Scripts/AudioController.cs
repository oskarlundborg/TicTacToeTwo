using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //BGM Sources
    private AudioSource melodySource;
    private AudioSource bassSource;
    private AudioSource drumsSource;
    private AudioSource instrumentsSource;

    //BGM Clips (temp)
    public AudioClip melody;
    public AudioClip bass;
    public AudioClip drums;
    public AudioClip instruments;

    //SFX
    //Sources and clips
    private List<AudioSource> CarvingSources = new List<AudioSource>();
    AudioSource flameOngoing;
    AudioSource flameLightUp;
    public AudioClip[] knifeClips;
    public AudioClip[] boneClips;
    public AudioClip flameOn;
    public AudioClip flameGoing;

    //Carving pitch bounds
    private float pitchFloor = 1.0f;
    private float pitchCeil = 1.5f;

    public void Start()
    {
        StartBackgroundMusic();
    }

    private void Update()
    {
        CleanUpCarvingSources();
    }

    public void PlayCarvingSounds()
    {
        AudioSource newKnifeSource = gameObject.AddComponent<AudioSource>();
        AudioSource newBoneSource = gameObject.AddComponent<AudioSource>();
        int random = Random.Range(0, 2);
        newKnifeSource.clip = knifeClips[random];
        newBoneSource.clip = boneClips[random];
        newKnifeSource.pitch = Random.Range(pitchFloor + random, pitchCeil + random);
        newBoneSource.pitch = Random.Range(pitchFloor + random, pitchCeil + random);
        newKnifeSource.Play();
        newBoneSource.Play();
        CarvingSources.Add(newKnifeSource);
        CarvingSources.Add(newBoneSource);
    }

    

    //Clean up unused AudioSources
    public void CleanUpCarvingSources()
    {
        foreach (AudioSource source in CarvingSources)
        {
            if (!source.isPlaying)
            {
                CarvingSources.Remove(source);
                Destroy(source);
                break;
            }
        }
    }

    public void StartFlames()
    {
        flameOngoing = gameObject.AddComponent<AudioSource>();
        flameLightUp = gameObject.AddComponent<AudioSource>();
        flameOngoing.loop = true;
        flameOngoing.clip = flameGoing;
        flameOngoing.volume = 0.25f;
        flameOngoing.Play();
        FlareFlames();
    }

    public void FlareFlames()
    {
        flameLightUp.pitch = Random.Range(pitchFloor, pitchCeil);
        flameLightUp.PlayOneShot(flameOn);
    }

    public void PutOutFlames()
    {
        flameOngoing.Pause();
    }

    private void StartBackgroundMusic()
    {
        //Create sources
        AudioSource melodySource = gameObject.AddComponent<AudioSource>();
        AudioSource bassSource = gameObject.AddComponent<AudioSource>();
        AudioSource drumsSource = gameObject.AddComponent<AudioSource>();
        AudioSource instrumentsSource = gameObject.AddComponent<AudioSource>();
        //Select clips
        melodySource.clip = melody;
        bassSource.clip = bass;
        drumsSource.clip = drums;
        instrumentsSource.clip = instruments;
        //Turn on looping
        instrumentsSource.loop = true;
        melodySource.loop = true;
        drumsSource.loop = true;
        bassSource.loop = true;
        //Play BGM
        melodySource.Play();
        bassSource.Play();
        drumsSource.Play();
        instrumentsSource.Play();
    }
}
