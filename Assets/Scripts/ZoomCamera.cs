using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
	public float minZoom = 5f;
	public float maxZoom = 2f;
	public float smoothTime = 0.5f;

	private bool m_canZoom;
	private Camera m_cam;
	private float m_velocity;

	private void Awake()
	{
		CharacterController2D.OnLookPainting += CanZoom;
	}

	// Start is called before the first frame update
	void Start()
    {
		m_canZoom = false;
		m_cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
		if (m_canZoom)
		{
			// change value of size on the camera
			float l_size = Mathf.SmoothDamp(m_cam.orthographicSize, maxZoom, ref m_velocity, smoothTime);

			m_cam.orthographicSize = l_size;
		}
		else
		{
			// change value of size on the camera
			float l_size = Mathf.SmoothDamp(m_cam.orthographicSize, minZoom, ref m_velocity, smoothTime);

			m_cam.orthographicSize = l_size;
		}
    }

	private void CanZoom(bool value)
	{
		m_canZoom = value;
	}
}
