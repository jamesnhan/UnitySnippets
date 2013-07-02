/*
 * Name: RTS Camera Movement
 * Author: James 'Sevion' Nhan
 * Date: 02/07/2013
 * Version: 1.0.0.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		arrow keys, mouse edge, and scroll wheel zooming.
 */

using UnityEngine;
using System.Collections;

public class RTSCameraMovement : MonoBehaviour {
	public const int SCROLLDISTANCE = 5;
	public const float SCROLLSPEED = 1.0f;
	public const float ZOOMSPEED = 5.0f;
	public const float MAXSCROLL = 50.0f;
	private float DEFAULTFOV = 0.0f;
	
	void Start() {
		DEFAULTFOV = Camera.main.fieldOfView;
	}
	
	// Update is called once per frame
	void Update() {
		// The camera's X rotation in radians
		float CameraAngleFromPerpendicular = Camera.main.transform.eulerAngles.x * Mathf.PI / 180.0f;
		
		// Get the input data in the respective axes
		float xAxisValue = Input.GetAxis("Horizontal");
		// The Y and Z axes need to be adjusted if there's any X rotation in the camera
		// Y corresponds to Sin and Z corresponds to Cos
	    float yAxisValue = Input.GetAxis("Vertical") * Mathf.Sin(CameraAngleFromPerpendicular);
		float zAxisValue = Input.GetAxis("Vertical") * Mathf.Cos(CameraAngleFromPerpendicular);
		
		// Check that there is a current camera and then transform it to the new vector
	    if(Camera.main != null){
	        Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
	    }
		
		// Get the mouse's coordinates
		float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
		
		// Reset these values
		xAxisValue = 0.0f;
		yAxisValue = 0.0f;
		zAxisValue = 0.0f;
 
		// X axis transformation
        if ((mousePosX < SCROLLDISTANCE)) {
			xAxisValue = -SCROLLSPEED;
		} else if ((mousePosX >= Screen.width - SCROLLDISTANCE)) {
			xAxisValue = SCROLLSPEED;
        }
		// Y axis transformation
		// The Y and Z axes need to be adjusted if there's any X rotation in the camera
		// Y corresponds to Sin and Z corresponds to Cos
        if ((mousePosY < SCROLLDISTANCE)) {
			yAxisValue = -SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular);
			zAxisValue = -SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular);
        } else if ((mousePosY >= Screen.height - SCROLLDISTANCE)) {
			yAxisValue = SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular);
			zAxisValue = SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular);
        }
		
		// Check that there is a current camera and then transform it to the new vector
	    if(Camera.main != null){
	        Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
		}
		
		// Scrolling Zoom
		// Scrolling up or down
		if (Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")) == 1) {
			// If it's in the zoom range, then allow to zoom else set to min zoom
			if (Camera.main.fieldOfView > DEFAULTFOV - MAXSCROLL) {
				Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ZOOMSPEED;
			} else {
				Camera.main.fieldOfView = DEFAULTFOV - MAXSCROLL;
			}
		} else {
			// If it's in the zoom range, then allow to zoom else set to max zoom
			if (Camera.main.fieldOfView < DEFAULTFOV) {
				Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ZOOMSPEED;
			} else {
				Camera.main.fieldOfView = DEFAULTFOV;
			}
		}
	}
}
