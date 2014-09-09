using UnityEngine;
using System.Collections;

public class MewTwoScript : BasicModalTileController
{

    protected override string getModalMessage()
    {
        return "Throw that Master Ball and catch the most powerful POKeMON ever created! All other players toast to your glory! You are now a POKeMON master!";
    }

    override protected void afterModal()
    {
        gameController.endGame();
    }
}
