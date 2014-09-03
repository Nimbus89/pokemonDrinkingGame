using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{

    private static AudioClip ButtonPressBeep = Resources.Load<AudioClip>("sounds/ButtonPressBeep");
    private static SFXManager instance = null;

    private AudioSource source;

    public static SFXManager Instance
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
        source = GetComponents<AudioSource>()[0];
    }

    public void playBeep() {
        source.clip = ButtonPressBeep;
        source.Play();
    }

    // any other methods you need
}