using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public delegate void ShakeCamAction();
	public static event ShakeCamAction OnShakeCam;

	public float magnitude = 2;
	public float duration = 0.5f;
	public float decreaseFactor = 0.5f;
	public float smoothTime = 0.5f;

	private float m_durationLeft;
	private bool m_canShake;
	private Vector3 m_posInit;
	private Vector3 m_target;
	private Vector3 velocity;

	private void Awake()
	{
		Init();
		OnShakeCam += Shake;
	}

	// Update is called once per frame
	void Update()
    {
		if (m_canShake && m_durationLeft > 0)
		{
			m_durationLeft -= Time.deltaTime * decreaseFactor;

			m_target = m_posInit + Random.insideUnitSphere * magnitude;
			float posX = Mathf.SmoothDamp(transform.position.x, m_target.x, ref velocity.x, smoothTime);
			float posY = Mathf.SmoothDamp(transform.position.y, m_target.y, ref velocity.y, smoothTime);
			float posZ = Mathf.SmoothDamp(transform.position.z, m_target.z, ref velocity.z, smoothTime);

			transform.position = new Vector3(posX, posY, posZ);
		}

		if (m_durationLeft == 0)
		{
			Init();
			transform.position = m_posInit;
		}
    }

	private void Init()
	{
		m_durationLeft = -1;
		m_canShake = false;
	}

	private void Shake()
	{
		m_canShake = true;
		m_posInit = transform.position;
		m_durationLeft = duration;
	}

	public static void OnShakeCamTrigger()
	{
		OnShakeCam?.Invoke();
	}
}
