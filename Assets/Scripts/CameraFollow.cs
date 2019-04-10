using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform characterTransform;
	public float positionY;
	public float smoothTimeX = 0.02f;
	public bool bounds;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	private Vector2 velocity;

	private void Awake()
	{
		ConsoleBehaviour.OnUnlock += ChangeRightClamp;
	}

	private void FixedUpdate()
	{
		float posX = Mathf.SmoothDamp(transform.position.x, characterTransform.position.x, ref velocity.x, smoothTimeX);
		transform.position = new Vector3(posX, positionY, transform.position.z);
	}

	// LateUpdate is called after Update each frame
	void LateUpdate()
	{
		if (bounds)
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
				Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
				Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
	}

	private void ChangeRightClamp()
	{
		maxCameraPos = new Vector3(45f, maxCameraPos.y, maxCameraPos.z);
	}
}
