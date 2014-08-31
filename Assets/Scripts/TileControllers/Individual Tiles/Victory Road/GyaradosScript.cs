using UnityEngine;
using System.Collections;

public class GyaradosScript : BasicModalTileController
{
    protected override string getModalMessage()
    {
        if (gameController.getCurrentPlayer().landedOnMagikarp())
        {
            return "Gyarados used Dragon Rage! Since you landed on Magikarp, give 4 drinks.";
        }
        else 
        {
            return "Gyarados used Dragon Rage! Since you didn't land on Magikarp, take 4 drinks.";
        }
    }
}

