using UnityEngine;
using System.Collections;

public abstract class TypedTileScript : MonoBehaviour
{
    public static string MESSAGE = "You're at a type disadvantage, take a drink!";

    public abstract void ApplyTypeRules(PokemonType type, CallbackDelegate cb);

    public void showMessage(CallbackDelegate cb) {
        GUIController.Instance.DisplayDialog(MESSAGE, cb);
    }
}
