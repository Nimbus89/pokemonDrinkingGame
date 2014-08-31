using UnityEngine;
using System.Collections;

public class FuschiaGymScript : BasicModalTileController
{
    public override bool IS_GOLD { get { return true; } }

    protected override string getModalMessage()
    {
        return "You challenge Gym Leader Koga! Poison Pokemon are Toxic, better get intoxicated! Drink 3.";
    }
}
