using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	public float targetPosY;
	public float speed;
	public AudioSource openSound;
	public AudioSource slamSound;

	private bool m_canOpen;
	private Vector3 m_posTarget;
	private float velocity;

	private void Awake()
	{
		m_canOpen = false;
		ConsoleBehaviour.OnOpenDoor += OnOpenDoor;
		m_posTarget = new Vector3(transform.position.x, targetPosY, transform.position.z);
	}

	// Update is called once per frame
	void Update()
    {
        if (m_canOpen)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, m_posTarget, step);

			if (!openSound.isPlaying)
			{
				openSound.Play();
			}

			if (transform.position.y == targetPosY)
			{
				openSound.Stop();
				slamSound.Play();
				CameraShake.OnShakeCamTrigger();
				m_canOpen = false;
			}
		}
    }

	private void OnOpenDoor()
	{
		m_canOpen = true;
	}
}
