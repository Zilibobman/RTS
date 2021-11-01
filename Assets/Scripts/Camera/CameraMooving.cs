using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMooving : MonoBehaviour, ICameraController
{
	public float speed = 5;

	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;
	public KeyCode rotCamA = KeyCode.Q;
	public KeyCode rotCamB = KeyCode.E;

	protected int rotationX = 70;
	protected float maxHeight = 15;
	protected float minHeight = 5;
	protected int rotationLimit = 240;
	protected CameraSettings cs;
	[SerializeField] protected CameraSettings standartSettings;

	private float camRotation;
	private float height;
	private float tmpHeight;
	private float h, v;
	private bool L, R, U, D;


    public void CursorTriggerEnter(string triggerName)
	{
		switch (triggerName)
		{
			case "L":
				L = true;
				break;
			case "R":
				R = true;
				break;
			case "U":
				U = true;
				break;
			case "D":
				D = true;
				break;
		}
	}

	public void CursorTriggerExit(string triggerName)
	{
		switch (triggerName)
		{
			case "L":
				L = false;
				break;
			case "R":
				R = false;
				break;
			case "U":
				U = false;
				break;
			case "D":
				D = false;
				break;
		}
	}
	void Start()
	{
		ChangeSettings(standartSettings);
	}
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
        {
			ChangeSettings(standartSettings);
			return;
		}
		if (Input.GetKey(left) || L) h = -1; else if (Input.GetKey(right) || R) h = 1; else h = 0;
		if (Input.GetKey(down) || D) v = -1; else if (Input.GetKey(up) || U) v = 1; else v = 0;

		if (Input.GetKey(rotCamB)) camRotation -= 3; else if (Input.GetKey(rotCamA)) camRotation += 3;
		camRotation = Mathf.Clamp(camRotation, 0, rotationLimit);

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (height < maxHeight) tmpHeight -= 1;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (height > minHeight) tmpHeight += 1;
		}

		tmpHeight = Mathf.Clamp(tmpHeight, minHeight, maxHeight);
		height = Mathf.Lerp(height, tmpHeight, 3 * Time.deltaTime);

		Vector3 direction = new Vector3(h, v, 0);
		transform.Translate(direction * speed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		transform.rotation = Quaternion.Euler(rotationX, camRotation, 0);
	}

    public void ChangeSettings(CameraSettings cameraSettings)
    {
		IChangeSettings?.Invoke(this, cs, cameraSettings);
		cs = cameraSettings;
		maxHeight = cameraSettings.StartPoint.position.y + cameraSettings.MaxHeight;
		minHeight = cameraSettings.StartPoint.position.y + cameraSettings.MinHeight;
		rotationLimit = cameraSettings.RotationLimit;
		rotationX = cameraSettings.RotationX;
		speed = cameraSettings.Speed;
		height = (maxHeight + minHeight) / 2;
		tmpHeight = height;
		camRotation = rotationLimit / 2;
		Camera.main.farClipPlane = cameraSettings.Far;
		transform.position = new Vector3(cameraSettings.StartPoint.position.x, height, cameraSettings.StartPoint.position.z);
	}
	public event ICameraController.EventChangeSettingsHandler IChangeSettings;
}
