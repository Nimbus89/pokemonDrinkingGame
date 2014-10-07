using UnityEngine;
using System.Collections;

public class MewTwoScript : BasicModalTileController
{

    public virtual bool CAN_BE_COPIED { get { return false; } }

    public override bool IS_GOLD { get { return true; } }

    protected override string getModalMessage()
    {
        return "Throw that Master Ball and catch the most powerful POKeMON ever created! All other players toast to your glory! You are now a POKeMON master!";
    }

    override protected void afterModal()
    {
        gameController.endGame();
    }
}
