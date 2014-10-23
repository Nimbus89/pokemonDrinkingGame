using UnityEngine;
using System.Collections;

public class BaseGUIManager<T> : MonoBehaviour where T : MonoBehaviour
{

    private static MonoBehaviour _instance;
    public static GUISkin DEFAULT_SKIN;

    public static T Instance
    {
        get { return (T) _instance; }
    }

    public virtual void Awake() {
        this.enabled = false;

        if (DEFAULT_SKIN == null) { 
            DEFAULT_SKIN = Resources.Load<GUISkin>("Skin");
        }

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    protected const float virtualWidth = 480.0f;
    protected const float virtualHeight = 320.0f;
    public static Matrix4x4 MATRIX;
    public virtual void Start()
    {
        UpdateMatrix();
    }

    public static void UpdateMatrix() {
        MATRIX = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / virtualWidth, Screen.height / virtualHeight, 1.0f));
    }

    virtual public void OnGUI()
    {
        GUI.matrix = MATRIX;
        GUI.skin = DEFAULT_SKIN;
    }
}
