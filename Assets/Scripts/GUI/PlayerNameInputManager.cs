﻿using UnityEngine;
using System.Collections;

public class PlayerNameInputManager : BaseGUIManager<PlayerNameInputManager>
{
    private string currentText = "";

    public string GetText() {
        return this.currentText;
    }

    public void SetText(string newText) {
        this.currentText = newText;
    }

    public void Show() {
        this.enabled = true;
        TouchScreenKeyboard.hideInput = true;
    }

    public void Show(string newText) {
        SetText(newText);
        Show();
    }

    public string Finsihed() {
        this.enabled = false;
        return GetText();
    }

    public void OnGUI() {
        base.OnGUI();
        if (Time.timeScale > 0.0f)
        {
            currentText = GUI.TextField(new Rect(0, 0, virtualWidth, 50), currentText);
        }
    }


}
