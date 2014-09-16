using UnityEngine;
using System.Collections;

public class PlayerNameInputManager : MonoBehaviour {

    public static PlayerNameInputManager Instance
    {
        get { return instance; }
    }

    private static PlayerNameInputManager instance;

    public void Awake()
    {
        this.enabled = false;
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

    private string currentText = "";

    public string GetText() {
        return this.currentText;
    }

    public void SetText(string newText) {
        this.currentText = newText;
    }

    public void Show() {
        this.enabled = true;
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
        GUI.skin = GUIController.Instance.skin;
        currentText = GUI.TextField(new Rect(0, 0, Screen.width, 50), currentText);
    }


}
