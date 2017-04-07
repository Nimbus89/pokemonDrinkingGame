using UnityEngine;
using System.Collections;

public class GUIController : BaseGUIManager<GUIController>
{

    public GUISkin skin;
	
	private string buttonText = "";
	private CallbackDelegate callback;

    public void DoSingleDiceRoll(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(DialogManager.Instance.ShowDialog(text, true, (int[] results) =>
        {
            cb(results[0]);
        }));
    }

    public void DoDokkanRoll(string text, PlayerController player, DicerollCallbackDelegate cb)
    {
        StartCoroutine(GUIController.Instance.DisplayDialogThenDokkanButtons_CR(text, player, (int result) =>
        {
            cb(result);
        }));
    }

    public void DoMultiDiceRoll(string text, MultiDicerollCallbackDelegate cb)
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(text, true, (int[] results) =>
        {
            cb(results);
        }));
    }

    public void DisplaySixNumberButtons(DicerollCallbackDelegate cb)
    {
        StartCoroutine(NumberButtonsManager.Instance.ShowButtons(cb));
    }

    public void DisplayDialog(string text, CallbackDelegate cb)
    {
        StartCoroutine(DisplayDialog_CR(text, cb));
    }

    public IEnumerator DisplayDialogThenStarterPicker_CR(string text, PokemonCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine(PokemonSelectorManager.Instance.ShowButtons((Pokemon selected) => {
            DialogManager.Instance.enabled = false;
            cb(selected);
        }));
    }

    public void DisplayDialogThenYesNoButtons(string text, BooleanCallbackDelegate cb)
    {
        StartCoroutine(DisplayDialogThenYesNoButtons_CR(text, cb));
    }

    public IEnumerator DisplayDialogThenYesNoButtons_CR(string text, BooleanCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine( YesNoButtonManager.Instance.ShowButtons((bool result) =>
        {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public void DisplayDialogThenNumberPicker(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(DisplayDialogThenNumberPicker_CR(text, cb));
    }

    private IEnumerator DisplayDialogThenDokkanButtons_CR(string text, PlayerController player, DicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine(DokkanButtonsManager.Instance.ShowButtons(player, (int result) => {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    private IEnumerator DisplayDialogThenNumberPicker_CR(string text, DicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text, false));
        yield return StartCoroutine(NumberButtonsManager.Instance.ShowButtons((int result) => {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public IEnumerator DisplayBasicDialog_CR(string text)
    {
        return DialogManager.Instance.ShowDialog(text);
    }

    public void DisplayBasicSkippableDialogs(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        StartCoroutine(DisplayBasicSkippableDialogs_CR(texts, cb, hideAfter));
    }

    public void DisplayBasicDialogs(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        StartCoroutine(DisplayBasicDialogs_CR(texts, cb, hideAfter));
    }

    public IEnumerator DisplayBasicDialogs_CR(string[] texts, CallbackDelegate cb, bool hideAfter = true)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialogs(texts, hideAfter, null, false));
        cb();
    }

    public IEnumerator DisplayBasicSkippableDialogs_CR(string[] texts, CallbackDelegate cb = null, bool hideAfter = true)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialogs(texts, hideAfter, null, true));
        if (cb != null) {
            cb();
        }
    }

    public IEnumerator DisplayDialog_CR(string text, CallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.ShowDialog(text));
        cb();
    }
	
	public override void Awake (){
        base.Awake();
	}
}
