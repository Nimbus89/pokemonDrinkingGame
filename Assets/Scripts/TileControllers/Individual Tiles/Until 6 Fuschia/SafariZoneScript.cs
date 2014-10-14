using UnityEngine;
using System.Collections;

public class SafariZoneScript : BasicModalTileController, StartOfTurnEffectTileController
{
    
    protected override string getModalMessage()
    {
        return "On your way into the Safari Zone, you are given 30 safari balls and 3 drinks!";
    }

    public void doSafariRules(PlayerController player, CallbackDelegate cb)
    {
        roller.doNormalDiceRollWithMessage("You encounter a wild POKeMON in the Safari Zone! Quick, roll a die!", (int result) => {
            if (result < 3) 
            {
                GUIController.Instance.DisplayDialog("You throw bait. Give 1 drink to someone. Now roll to move.", cb);
            }
            else if (result < 5)
            {
                GUIController.Instance.DisplayDialog("You throw a rock. Dick. Lose a turn and drink 2", player.endTurn);
            }
            else
            {
                GUIController.Instance.DisplayDialog("You throw a Safari Ball. Drink 2, because Safari Balls are just awful. Now roll to move.", cb);
            }
        });
    }

    public void doStartOfTurnRules(PlayerController player, CallbackDelegate cb)
    {
        doSafariRules(player, cb);
    }
}
