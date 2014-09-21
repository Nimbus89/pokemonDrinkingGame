using UnityEngine;
using System.Collections;

public class FightingGymScript : PickPlayerTileController
{

    protected override string initialModalText()
    {
        return "Pick someone to challenge to a chugging contest!";
    }

    protected override PlayerController[] playersToPickFrom()
    {
        return gameController.players;
    }

    protected override void reactToPlayerPicked(PlayerController challengedPlayer)
    {
        GUIController.Instance.DisplayBasicDialogs(new string[] { 
            "Now, " + this.currentPlayer.getName() + " and " + challengedPlayer.getName() + ", race to finish a drink."
        }, () => {
            GUIController.Instance.DisplayDialogThenYesNoButtons("Did " + this.currentPlayer.getName() + " win?", (bool currentPlayerWon) =>
            {
                reactToOutcome(currentPlayerWon, challengedPlayer);
            });
        });
    }

    private void reactToOutcome(bool currentPlayerWon, PlayerController challenged)
    {
        if (currentPlayerWon)
        {
            applyEffects(currentPlayer, challenged);
        }
        else 
        {
            applyEffects(challenged, currentPlayer);
        }
    }

    private void applyEffects(PlayerController winner, PlayerController loser) {
        winner.getExtraTurns(1);
        loser.skipTurns(1, "You're still knocked out from being beaten in the fighting gym. Lose a turn.");
        GUIController.Instance.DisplayBasicDialogs(new string[] { 
            winner.getName() + " gets an extra turn for being the winner.",
            loser.getName() + " must lose a turn."
        }, returnControlToPlayer);
    }
}
