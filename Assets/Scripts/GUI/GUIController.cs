﻿using UnityEngine;
using System.Collections;

public enum GUIState { DISPLAYING_BUTTON, NONE};

public class GUIController : MonoBehaviour {

    public static GUIController Instance{
        get { return instance; }
    }

    private static GUIController instance;

    public GUISkin skin;

	private Rect BottomOfScreenButtonPosition = new Rect(50, Screen.height - 50, Screen.width - 100, 50);
	
	private string buttonText = "";
	private CallbackDelegate callback;

    private GUIState state;

    public void DoSingleDiceRoll(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(DialogManager.Instance.showDialog(text, true, (int[] results) =>
        {
            cb(results[0]);
        }));
    }

    public void DisplaySixNumberButtons(DicerollCallbackDelegate cb)
    {
        StartCoroutine(NumberButtonsManager.Instance.ShowButtons(cb));
    }

    public void DisplayBasicModal(string text, CallbackDelegate cb)
    {
        StartCoroutine(displayBasicModal(text, cb));
    }

    public void DisplayBasicButton(string text, CallbackDelegate cb)
    {
        buttonText = text;
        callback = cb;
        state = GUIState.DISPLAYING_BUTTON;
    }

    public IEnumerator DisplayDialogThenStarterPicker(string text, PokemonCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, false));
        yield return StartCoroutine(PokemonSelectorManager.Instance.ShowButtons((Pokemon selected) => {
            DialogManager.Instance.enabled = false;
            cb(selected);
        }));
    }


    public void DisplayDialogThenNumberPicker(string text, DicerollCallbackDelegate cb) {
        StartCoroutine(displayDialogThenNumberPicker(text, cb));
    }
    private IEnumerator displayDialogThenNumberPicker(string text, DicerollCallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text, false));
        yield return StartCoroutine(NumberButtonsManager.Instance.ShowButtons((int result) => {
            DialogManager.Instance.enabled = false;
            cb(result);
        }));
    }

    public IEnumerator DisplayBasicDialog(string text)
    {
        return DialogManager.Instance.showDialog(text);
    }

    public void DisplayBasicDialogs(string[] texts, CallbackDelegate cb)
    {
        StartCoroutine(displayBasicDialogs(texts, cb));
    }

	void Awake (){
        state = GUIState.NONE;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
	}

    void OnGUI()
    {
        GUI.skin = skin;
		switch(state){
			case(GUIState.DISPLAYING_BUTTON):
				if(GUI.Button(BottomOfScreenButtonPosition, buttonText)){
					finsihed();
				}
				break;
			default:
				break;
		}
	}

    private IEnumerator displayBasicDialogs(string[] texts, CallbackDelegate cb)
    {
        foreach(string text in texts){
            yield return StartCoroutine(DialogManager.Instance.showDialog(text));
        }
        cb();
    }

    private IEnumerator displayBasicModal(string text, CallbackDelegate cb)
    {
        yield return StartCoroutine(DialogManager.Instance.showDialog(text));
        cb();
    }
	
	private void finsihed(){
		state = GUIState.NONE;
        SFXManager.Instance.playBeep();
		callback();
	}		
}