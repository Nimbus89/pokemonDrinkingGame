using UnityEngine;
using System.Collections;

public class ElectrodeScript : BasicModalTileController
{
    private bool hasExploded = false;

    override protected void doRules()
    {
        base.doRules();
        hasExploded = true;
    }

    protected override string getModalMessage()
    {
        if (hasExploded)
        {
            return "Woah, looks like there was an explosion here. Drink twice and walk around the crater.";
        }
        else 
        {
            return "Oh shit. Electrode used Explosion! Everybody finishes their drink.";
        }
        
    }
}
