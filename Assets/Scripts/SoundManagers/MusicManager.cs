using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    private static AudioClip titleMusic2;
    private static AudioClip profSpeechMusic;
    private static MusicManager instance = null;
    private static float fadeSpeed = 1.3f;

    private AudioSource source;

    public static MusicManager Instance
    {
        get { return instance; }
    }

    void Update() {
        if (Input.GetKeyDown("m")) {
            ToggleMute();
        }
    }

    void Awake()
    {

        titleMusic2 = Resources.Load<AudioClip>("sounds/titleMusic2");
        profSpeechMusic = Resources.Load<AudioClip>("sounds/profSpeechMusic");

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        source = GetComponents<AudioSource>()[1];
    }

    public void PlayInstantly(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void playTitleMusic()
    {
        PlayInstantly(titleMusic2);
    }

    public void playSpeechMusic()
    {
        PlayInstantly(profSpeechMusic);
    }

    public void PlayTileMusic(AudioClip tileMusic) {
        if (tileMusic != source.clip) {
            StartCoroutine(fadeToTrack(tileMusic));
        }
    }

    public void ToggleMute() {
        source.mute = !source.mute;
    }

    public IEnumerator fadeToTrack(AudioClip track)
    {
        while (source.volume > 0.1) {
            source.volume -= fadeSpeed * Time.deltaTime;
            yield return 0;
        }
        source.clip = track;
        source.time = Random.Range(0, track.length);
        source.Play();
        while (source.volume < 0.9)
        {
            source.volume += fadeSpeed * Time.deltaTime;
            yield return 0;
        }
        
    }
}