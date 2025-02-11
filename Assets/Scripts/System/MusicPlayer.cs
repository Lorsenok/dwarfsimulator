using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] private AudioSource defaultMusic;
    [SerializeField] private AudioSource battleMusic;
    [SerializeField] private float transitionSpeed = 1;
    private AudioSource lastAudioSource;
    private AudioSource currentAudioSource;
    private bool isTransition;

    private void Awake()
    {
        Instance = this;
        defaultMusic.Play();
        battleMusic.Stop();
        defaultMusic.loop = true;
        battleMusic.loop = true;
        currentAudioSource = defaultMusic;
    }

    public void PlayBattleMusic()
    {
        if(currentAudioSource != battleMusic)
        {
            lastAudioSource = currentAudioSource;
            currentAudioSource = battleMusic;
            StartCoroutine(SwitchingMusic());
        }
            
    }
    public void PlayDefaultMusic()
    {
        if (currentAudioSource != defaultMusic && MusicPlayer.Instance != null)
        {
            lastAudioSource = currentAudioSource;
            currentAudioSource = defaultMusic;
            StartCoroutine(SwitchingMusic());
        }   
    }
    private IEnumerator SwitchingMusic()
    {
        isTransition = true;
        lastAudioSource.volume = Config.Music;
        currentAudioSource.volume = 0;
        currentAudioSource.Play();
        while (currentAudioSource.volume < Config.Music & lastAudioSource.volume>0)
        {
            lastAudioSource.volume -= Time.deltaTime * transitionSpeed;
            currentAudioSource.volume += Time.deltaTime * transitionSpeed;

            yield return new WaitForEndOfFrame();
        }
        lastAudioSource.volume = 0;
        currentAudioSource.volume = Config.Music;
        lastAudioSource.Stop();
        isTransition = false;
    }
    private void Update()
    {
        if(!isTransition)
        {
            defaultMusic.volume = Config.Music;
            battleMusic.volume = Config.Music;
        }
    }
}
