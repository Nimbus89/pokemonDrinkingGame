using UnityEngine;
using System.Collections;

public class CinnabarGymScript : DicerollTileController
{

    public override bool IS_GOLD { get { return true; } }

    private int evensRolled;

    protected override void doRules()
    {
        evensRolled = 0;
        base.doRules();
    }

    protected override string initialModalText()
    {
        return "You challenge Gym Leader Blaine! Keep rolling until you roll an odd number.";
    }

    protected override void reactToDiceRoll(int rollResult)
    {
        if((rollResult % 2) == 0){
            evensRolled++;
            roller.doNormalDiceRoll(reactToDiceRoll);
        } else {
            GUIController.Instance.DisplayBasicModal("Take " + evensRolled * 2 + " drinks.", returnControlToPlayer);
        }
       
    }

}