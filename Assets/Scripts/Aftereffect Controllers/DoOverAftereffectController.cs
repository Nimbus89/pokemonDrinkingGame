using UnityEngine;
using System.Collections;

public class DoOverAftereffectController : AftereffectController
{

    public DoOverAftereffectController(PlayerController player, GameController game): base(player, game){}

    public override void applyEffect()
    {
        player.move(0);
    }
}
