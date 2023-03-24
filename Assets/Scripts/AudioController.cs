// Oskar Lundborg - oslu6451

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //BGM Sources
    private AudioSource mainSource;
    private AudioSource unholySource;
    private AudioSource holySource;

    //BGM Clips (temp)
    public AudioClip mainLoop;
    public AudioClip unholyLoop;
    public AudioClip holyLoop;

    //BGM Fade Time
    public float fadeTime = 1f;

    //SFX
    //Sources and clips
    private List<AudioSource> CarvingSources = new List<AudioSource>();
    private AudioSource waterDropSource;
    private AudioSource flameOngoingSource;
    private AudioSource flameLightUpSource;
    private AudioSource caveWindSource;
    public AudioClip[] knifeClips;
    public AudioClip[] boneClips;
    public AudioClip[] droppletClips;
    public AudioClip flameLghtUp;
    public AudioClip flameOngoing;
    public AudioClip caveWind;

    //Droplet Delay
    float dropletDelay = 0f;

    //Carving pitch bounds
    private float pitchFloor = 1.0f;
    private float pitchCeil = 1.5f;

    

    public void Start()
    {
        StartBackgroundMusic();
        StartAmbience();
    }

    private void Update()
    {
        CleanUpCarvingSources();
        PlayWaterDropSound();
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

    private void PlayWaterDropSound()
    {   
        if (!waterDropSource.isPlaying && dropletDelay <= 0)
        {
            dropletDelay = Random.Range(1, 6);
            waterDropSource.pitch = Random.Range(pitchFloor, pitchCeil);
            waterDropSource.clip = droppletClips[Random.Range(0, 2)];
            waterDropSource.Play();
        }
        dropletDelay -= Time.deltaTime;
        Debug.Log(dropletDelay);
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

    private void StartAmbience()
    {
        waterDropSource = gameObject.AddComponent<AudioSource>();
        waterDropSource.volume = 0.2f;
        caveWindSource = gameObject.AddComponent<AudioSource>();
        caveWindSource.loop = true;
        caveWindSource.clip = caveWind;
        caveWindSource.Play();
    }

    public void StartFlames()
    {
        flameOngoingSource = gameObject.AddComponent<AudioSource>();
        flameLightUpSource = gameObject.AddComponent<AudioSource>();
        flameOngoingSource.loop = true;
        flameOngoingSource.clip = flameOngoing;
        flameOngoingSource.volume = 0.5f;
        flameOngoingSource.Play();
        FlareFlames();
    }

    public void FlareFlames()
    {
        flameLightUpSource.pitch = Random.Range(pitchFloor, pitchCeil);
        flameLightUpSource.PlayOneShot(flameLghtUp);
    }

    public void PutOutFlames()
    {
        flameOngoingSource.Pause();
    }

    private void StartBackgroundMusic()
    {
        //Create sources
        mainSource = gameObject.AddComponent<AudioSource>();
        unholySource = gameObject.AddComponent<AudioSource>();
        holySource = gameObject.AddComponent<AudioSource>();
        //Select clips
        mainSource.clip = mainLoop;
        unholySource.clip = unholyLoop;
        holySource.clip = holyLoop;
        //Turn on looping
        mainSource.loop = true;
        unholySource.loop = true;
        holySource.loop = true;
        //Initialize volume
        unholySource.volume = 0.0f;
        holySource.volume = 0.0f;
        //Play BGM
        mainSource.Play();
        unholySource.Play();
        holySource.Play();
    }


    // Starts and stops correct fade in/out coroutine
    public void SwapMusic(string newPlayer, string prevPlayer)
    {
        if(newPlayer == "X")
        {
            StopCoroutine(FadeOutUnholy());
            StartCoroutine(FadeInUnholy());
            if(prevPlayer == "O") 
            {
                StopCoroutine(FadeInHoly());
                StartCoroutine(FadeOutHoly());
            } else
            {
                StopCoroutine(FadeInDefault());
                StartCoroutine(FadeOutDefault());
            }
        } else if( newPlayer == "O")
        {
            StopCoroutine(FadeOutHoly());
            StartCoroutine(FadeInHoly());
            if (prevPlayer == "X")
            {
                StopCoroutine(FadeInUnholy());
                StartCoroutine(FadeOutUnholy());
            } else
            {
                StopCoroutine(FadeInDefault());
                StartCoroutine(FadeOutDefault());
            }
        } else
        {
            StopCoroutine(FadeOutDefault());
            StartCoroutine(FadeInDefault());
            if(prevPlayer == "X")
            {
                StopCoroutine(FadeInUnholy());
                StartCoroutine(FadeOutUnholy());
            } else
            {
                StopCoroutine(FadeInHoly());
                StartCoroutine(FadeOutHoly()); 
            }
        }  
    }


    //Fading IEnumerators
    private IEnumerator FadeOutDefault()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            mainSource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutUnholy()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            unholySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutHoly()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            holySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInDefault()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            mainSource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInUnholy()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            unholySource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeInHoly()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            holySource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
