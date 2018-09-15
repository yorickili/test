using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{

	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
    public Vector4[] parts;

    private Vector2 maxXAndY;       // The maximum x and y coordinates the camera can have.
    private Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.


	static private Transform player;		// Reference to the player's transform.
    static private float width, height, ratio;

    static public float GetScreenRatio() { return ratio; }
    static public float GetScreenWidth() { return width; }
    static public float GetScreenHeight() { return height; }

    static public Transform GetPlayerTransform() { return player; }

    private bool isStopped = false;

    void SetBasicValues()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;
        //the up right corner
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        width = rightBorder - leftBorder;
        height = topBorder - downBorder;

        ratio = width / height;
    }

	private void Start()
	{
        SetBasicValues();
        ChangePart(2);
    }

    public void Stop()
    {
        isStopped = true;
    }

    public void ChangePart(int i)
    {
        if (i >= parts.Length) return;
        maxXAndY = new Vector2(parts[i].x, parts[i].y);
        minXAndY = new Vector2(parts[i].z, parts[i].w);
    }

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
        isStopped = false;

    }


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}

    void ChangeCameraSize()
    {
        GetComponent<Camera>().orthographicSize = 11;
        //GameObject.
    }

    /*
    void FixedUpdate ()
	{
        if (!isStopped)
            TrackPlayer();
	}
    */
	
	public void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
        float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
