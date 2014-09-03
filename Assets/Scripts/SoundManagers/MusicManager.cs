using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    private static AudioClip titleMusic2 = Resources.Load<AudioClip>("sounds/titleMusic2");
    private static AudioClip profSpeechMusic = Resources.Load<AudioClip>("sounds/profSpeechMusic");
    private static AudioClip paletteTownMusic = Resources.Load<AudioClip>("sounds/paletteTownMusic");
    private static MusicManager instance = null;

    private AudioSource source;

    public static MusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
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

    public void playTitleMusic()
    {
        source.clip = titleMusic2;
        source.Play();
    }

    public void playSpeechMusic()
    {
        source.clip = profSpeechMusic;
        source.Play();
    }

    public void playPaletteTownMusic()
    {
        source.clip = paletteTownMusic;
        source.Play();
    }
}