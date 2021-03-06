﻿using UnityEngine;
using System.Collections;

public class MissingNoScript : TileController {

    int chances;

    protected override void doRules()
    {
        chances = 3;
        GUIController.Instance.DisplayDialog("Shit, a Missingno. appeared! Better hope this doesn't fuck up your save file.", takeChance);
    }

    private void takeChance() {
        roller.doNormalDiceRollWithMessage("Roll to protect your save file! You have " + chances + " rolls.", (int result) =>
        {
            if (result < 5)
            {
                if (chances > 1)
                {
                    chances--;
                    GUIController.Instance.DisplayDialog("Shit, it didn't work. Try again!", takeChance);
                }
                else 
                {
                    lose();
                }
            }
            else 
            {
                win();
            }
        });
    }

    private void win() {
        GUIController.Instance.DisplayDialog("Looks like your save file is alright. Everybody drink in relief.", returnControlToPlayer);
    }

    private void lose() {
        GUIController.Instance.DisplayDialog("Fuck! Your save file is corrupted. Better start a new file. Go back to Pallett Town! And drink the whole way there!", () => {
            currentPlayer.MoveTo(0, returnControlToPlayer); 
        });
    }
}
