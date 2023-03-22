using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    // Old variables
    /*
    public AudioSource musicSource;
    public AudioSource knifeSource;
    public AudioSource boneSource;
    public AudioClip bgm;
    public AudioClip[] knife;
    public AudioClip[] bone;
    private int clipIndex;
    private float pitchFloor = 1.0f;
    private float pitchCeil = 1.5f;
    */

    //Music
    public AudioSource melodySource;
    public AudioSource bassSource;
    public AudioSource drumsSource;
    public AudioSource instrumentsSource;

    public AudioClip melody;
    public AudioClip bass;
    public AudioClip drums;
    public AudioClip instruments;

    //SFX
    //Sources and clips
    public List<AudioSource> CarvingSources;
    public AudioClip[] knifeClips;
    public AudioClip[] boneClips;

    //Pitch
    private float pitchFloor = 1.0f;
    private float pitchCeil = 1.5f;


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
        Debug.Log(newKnifeSource.pitch.ToString());
        Debug.Log(newBoneSource.pitch.ToString());
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

    public void StartBackgroundMusic()
    {
        // Old music
        /*
        musicSource.clip = bgm;
        musicSource.loop = true;
        musicSource.Play();
        */

        melodySource = gameObject.AddComponent<AudioSource>();
        melodySource.clip = melody;
        melodySource.loop = true;
        bassSource = gameObject.AddComponent<AudioSource>(); 
        bassSource.clip = bass;
        bassSource.loop = true;
        drumsSource = gameObject.AddComponent<AudioSource>();  
        drumsSource.clip = drums;
        drumsSource.loop = true;
        instrumentsSource = gameObject.AddComponent<AudioSource>();
        instrumentsSource.clip = instruments;
        instrumentsSource.loop = true;

        melodySource.Play();
        bassSource.Play();
        drumsSource.Play();
        instrumentsSource.Play();
    }


    // Old sounds
    /*
    public void PlayCarvingSounds()
    {
        clipIndex = 1; //Random.Range(0, 2);
        knifeSource.pitch = Random.Range(pitchFloor, pitchCeil);
        boneSource.pitch = knifeSource.pitch + 0.5f;
        //knifeSource.pitch = Random.Range(0.5f, 1.0f);
        //boneSource.pitch = Random.Range(0.75f, 1.5f);
        knifeSource.clip = knife[clipIndex];
        boneSource.clip = bone[clipIndex];
        knifeSource.Play();
        boneSource.Play();
    }
    */


}
