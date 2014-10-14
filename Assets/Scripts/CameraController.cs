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
    }

	public static float READ_TILE_SIZE = 3;
	public static float READ_RULES_SIZE = 6;
	public static float DEFAULT_SIZE = 8;
	public static float MAX_SIZE = 22;
	public static float ALL_TILES_SIZE = 17;
	public static float ZOOM_SPEED = 5;
	public static float MOVE_SPEED = 5;
	public static float MY_Z_POSITION = -5;

	public GameObject rulesTarget;	
	private GameObject target;
	private float targetSize;

	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, MY_Z_POSITION);
		camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, Time.deltaTime * ZOOM_SPEED);
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * MOVE_SPEED);
	}
	
	public void FocusOnPlayer(PlayerController player){
		target = player.gameObject;
		SmoothZoom(DEFAULT_SIZE);
	}
	
	void SmoothZoom(float zoomLevel){
		targetSize = zoomLevel;
	}
}
