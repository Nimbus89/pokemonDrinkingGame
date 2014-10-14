using UnityEngine;
using System.Collections;

public class SaffronGymScript : TileController {

    public override bool IS_GOLD { get { return true; } }

    protected override void doRules()
    {
        PlayerController player = gameController.getCurrentPlayer();
        GUIController.Instance.DisplayDialogThenNumberPicker("Use your psychic powers to choose a number!", (int numberPicked) => {
            roller.doNormalDiceRollWithMessage("Now roll a die!", (int numberRolled) =>
            {
                if (numberRolled == numberPicked)
                {
                    GUIController.Instance.DisplayDialog("You guessed correctly! Must be those psychic powers. Take an extra turn!", () =>
                    {
                        player.getExtraTurns(1);
                        returnControlToPlayer();
                    });
                }
                else 
                {
                    GUIController.Instance.DisplayDialog("Too bad! I guess you must be a fighting type. Take 2 drinks.", () =>
                    {
                        returnControlToPlayer();
                    });
                }
            });
        });
    }
}
