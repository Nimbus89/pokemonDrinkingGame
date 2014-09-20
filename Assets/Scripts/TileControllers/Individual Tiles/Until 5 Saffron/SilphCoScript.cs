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
        return "While in Silph Co, take an extra drink for every drink that you recieve.";
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
