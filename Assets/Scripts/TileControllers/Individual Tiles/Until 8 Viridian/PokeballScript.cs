﻿using UnityEngine;
using System.Collections;

public class PokeballScript : TileController
 {
     protected override void doRules()
     {
         GUIController.Instance.DisplayDialogThenYesNoButtons("Is your favourite POKeMON on the board?", (bool result) => {
             if (result)
             {
                 roller.doDiceRollWithMessage("Awesome, throw a POKeBALL and roll a die.", (int rollResult) => {
                     if (rollResult < 4)
                     {
                         GUIController.Instance.DisplayBasicModal("Damn, should've used an Ultra Ball. It burst out. Drink 3.", returnControlToPlayer);
                     }
                     else 
                     {
                         GUIController.Instance.DisplayBasicModal("Badass, you caught it! Everybody dirnks in celebration!", returnControlToPlayer);
                     }
                 });
             }
             else 
             {
                 GUIController.Instance.DisplayBasicModal("Seriously? All the best ones are here. Whatever, drink 3.", returnControlToPlayer);
             }
         });
     }
 }
