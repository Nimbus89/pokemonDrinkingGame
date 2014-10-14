using UnityEngine;
using System.Collections;

public class ChampionGaryAftereffectController : AftereffectController
{

    public ChampionGaryAftereffectController(PlayerController player, GameController game):base(player, game)
    {
	}

    public override void applyEffect()
    {
        GUIController.Instance.DisplayDialogThenYesNoButtons("Have you finished your drink yet?", (bool answeredYes) => {
            if (answeredYes)
            {
                GUIController.Instance.DisplayDialog("You took down his last POKeMON! You are the new POKeMON League Champion!", () =>
                {
                    player.move(1);
                });
            }
            else 
            {
                GUIController.Instance.DisplayDialog("You couldn't defeat him this time. Try again next turn!", () =>
                {
                    player.endTurn();
                });
            }
        });
    }
}
