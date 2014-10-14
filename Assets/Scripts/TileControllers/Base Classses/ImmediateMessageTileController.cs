using UnityEngine;
using System.Collections;

public abstract class ImmediateMessageTileController : BasicModalTileController {

    public IEnumerator showImmediatedMessage() {
        PlayMyMusic();
        return GUIController.Instance.DisplayBasicSkippableDialogs_CR(new string[] {getImmediateMessage()});
    }

    public abstract string getImmediateMessage();

}
