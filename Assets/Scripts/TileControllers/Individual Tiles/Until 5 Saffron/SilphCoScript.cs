using UnityEngine;
using System.Collections;

public class SilphCoScript : ImmediateMessageTileController
{

    public TileController[] SilphCoTiles;

    protected override string getModalMessage()
    {
        return "Take a drink on your way into Silph Co.";
    }

    public override string getImmediateMessage() {
        return "You've infiltrated the headquarters of the infamous Team Rocket! You will need all your courage to make it to their leader. Drink an extra 2 every turn to calm your nerves.";
    }

    public bool IsPlayerInZone() {
        foreach (TileController tile in SilphCoTiles) {
            if (tile.GetPlayersOnMe().Length != 0) {
                return true;
            }
        }
        return false;
    }
}
