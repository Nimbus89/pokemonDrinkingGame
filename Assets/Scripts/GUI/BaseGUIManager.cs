using UnityEngine;
using System.Collections;

public class BaseGUIManager : MonoBehaviour {

    protected const float virtualWidth = 480.0f;
    protected const float virtualHeight = 320.0f;
    public static Matrix4x4 MATRIX;
    void Start()
    {
        UpdateMatrix();
    }

    public static void UpdateMatrix() {
        MATRIX = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / virtualWidth, Screen.height / virtualHeight, 1.0f));
    }

    virtual public void OnGUI()
    {
        GUI.matrix = MATRIX;
        GUI.skin = GUIController.Instance.skin;
    }
}
