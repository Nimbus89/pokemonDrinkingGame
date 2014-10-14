using UnityEngine;
using System.Collections;

public class PokemonSelectorManager : BaseGUIManager<PokemonSelectorManager>
{

    private static int BUTTON_INTERNAL_MARGIN = 30;
    private static int BUTTON_WIDTH = 100;
    private static int BUTTON_HEIGHT = 100;
    private float BUTTON_VERTICAL_MARGIN;
    private float BUTTON_HORIZONTAL_MARGIN;

    private PokemonCallbackDelegate callback;

    public override void Awake()
    {
        BUTTON_VERTICAL_MARGIN = (virtualHeight - (BUTTON_HEIGHT)) / 2;
        BUTTON_HORIZONTAL_MARGIN = (virtualWidth - (BUTTON_WIDTH * 3)) / 2 - BUTTON_INTERNAL_MARGIN;
        base.Awake();
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
        base.OnGUI();

        Pokemon[] mons = new Pokemon[] { Pokemon.bulbasaur, Pokemon.charmander, Pokemon.squirtle };

        for (int i = 0; i < 3; i++) {
            if (GUI.Button(new Rect(BUTTON_HORIZONTAL_MARGIN + (BUTTON_WIDTH + BUTTON_INTERNAL_MARGIN) * i, BUTTON_VERTICAL_MARGIN, BUTTON_WIDTH, BUTTON_HEIGHT), PokemonSprites.getTexture(mons[i])))
            {
                choosePokemon(mons[i]);
            }
        }
    }
}
