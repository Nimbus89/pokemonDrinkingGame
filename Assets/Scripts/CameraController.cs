using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    public static CameraController Instance
    {
        get { return instance; }
    }

    private static CameraController instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        SmoothZoom(DEFAULT_SIZE);
    }

	public static float READ_TILE_SIZE = 3;
	public static float READ_RULES_SIZE = 6;
	public static float DEFAULT_SIZE = 10;
	public static float MAX_SIZE = 22;
	public static float ALL_TILES_SIZE = 17;
	public static float ZOOM_SPEED = 5;
	public static float MOVE_SPEED = 5;
	public static float MY_Z_POSITION = -5;

    const int CAMERA_MAX_SIZE = 20;
    const int CAMERA_MIN_SIZE = 5;
    const int CAMERA_STEP = 2;

    public GameObject rulesTarget;	
	private GameObject target;
	public float targetSize;

	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, MY_Z_POSITION);
		GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetSize, Time.deltaTime * ZOOM_SPEED);
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * MOVE_SPEED);

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
        {
            targetSize += CAMERA_STEP;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // back
        {
            targetSize -= CAMERA_STEP;
        }

        targetSize = Mathf.Clamp(targetSize, CAMERA_MIN_SIZE, CAMERA_MAX_SIZE);
    }

	public void FocusOnPlayer(PlayerController player){
        target = player.gameObject;
	}
	
	void SmoothZoom(float zoomLevel){
		targetSize = zoomLevel;
	}
}
