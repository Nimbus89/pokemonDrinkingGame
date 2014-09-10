using UnityEngine;
using System.Collections;

public class PokemonSelectorManager : MonoBehaviour
{

    private static PokemonSelectorManager instance;

    public GUISkin skin;

    private static int BUTTON_INTERNAL_MARGIN = 30;
    private static int BUTTON_WIDTH = 100;
    private static int BUTTON_HEIGHT = 100;
    private int BUTTON_VERTICAL_MARGIN;
    private int BUTTON_HORIZONTAL_MARGIN;

    private PokemonCallbackDelegate callback;

    public static PokemonSelectorManager Instance
    {
        get { return instance; }
    }

    public void Awake()
    {
        BUTTON_VERTICAL_MARGIN = (Screen.height - (BUTTON_HEIGHT)) / 2;
        BUTTON_HORIZONTAL_MARGIN = (Screen.width - (BUTTON_WIDTH * 3)) / 2 - BUTTON_INTERNAL_MARGIN;
        this.enabled = false;
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
    }

    public IEnumerator ShowButtons(PokemonCallbackDelegate cb){
        this.enabled = true;
        this.callback = cb;
        while (this.enabled) {
            yield return 0;
        }
    }

    private void choosePokemon(Pokemon starter) {
        this.enabled = false;
        this.callback(starter);
    }



    public void OnGUI()
    {
        GUI.skin = skin;

        Pokemon[] mons = new Pokemon[] { Pokemon.bulbasaur, Pokemon.charmander, Pokemon.squirtle };

        for (int i = 0; i < 3; i++) {
            if (GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + (BUTTON_WIDTH + BUTTON_INTERNAL_MARGIN) * i, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), PokemonSprites.getTexture(mons[i])))
            {
                choosePokemon(mons[i]);
            }
        }
    }
}
