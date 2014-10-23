using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{

    private static AudioClip ButtonPressBeep;
    private static AudioClip MenuClack;
    private static SFXManager instance = null;

    private AudioSource source;

    public static SFXManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {

        ButtonPressBeep = Resources.Load<AudioClip>("sounds/ButtonPressBeep");
        MenuClack = Resources.Load<AudioClip>("sounds/clack");

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

    public void playClack()
    {
        source.clip = MenuClack;
        source.Play();
    }
}