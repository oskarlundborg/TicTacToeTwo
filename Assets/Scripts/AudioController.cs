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

    //BGM Fade Time
    public float fadeTime = 3.0f;

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

    public void SwapMusic(string player)
    {
        StopAllCoroutines();

        if(player == "X")
        {
            StartCoroutine(SwapToUnholyMusic());
        } else if( player == "O")
        {
            StartCoroutine(SwapToHolyMusic());
        } else
        {
            StartCoroutine(SwapToDefaultMusic());
        }
           
    }


    //FULKOD
    private IEnumerator SwapFromDefaultToUnholyMusic()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            unholySource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            holySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            mainSource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed+= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SwapFromDefaultToHolyMusic()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            holySource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            unholySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            mainSource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed+= Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator SwapFromUnholyToDefaultMusic()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            mainSource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            unholySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed+= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SwapFromHolyToDefaultMusic()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            mainSource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            holySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SwapFromUnholyToHolyMusic()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            holySource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            unholySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SwapFromHolyToUnholyMusic()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < fadeTime)
        {
            holySource.volume = Mathf.Lerp(1, 0, timeElapsed / fadeTime);
            unholySource.volume = Mathf.Lerp(0, 1, timeElapsed / fadeTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

}
