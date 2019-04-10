using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
	public Animator RedCircleAnim;
	public CharacterController2D characterController2D;
	public AudioSource closeDoor;
	public AudioSource doorSlam;
	public AudioSource secretRoomBGM;
	public Vector3 targetPosDoor;
	public float speed;
	public Timer timer;
	public Timer endTimer;
	public Canvas canvas;

	private const string C_TAG_PLAYER = "Player";

	private bool m_canClose;

	private void Awake()
	{
		m_canClose = false;
		Timer.OnTimerDone += OnTimerDone;
		endTimer.OnTimerDoneLocal += OnEndTimerDone;
	}

	// Update is called once per frame
	void Update()
	{
		if (m_canClose)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPosDoor, step);

			if (!closeDoor.isPlaying)
			{
				closeDoor.Play();
			}

			if (transform.position.y == targetPosDoor.y)
			{
				closeDoor.Stop();
				doorSlam.Play();
				CameraShake.OnShakeCamTrigger();
				RedCircleAnim.SetBool("canPlay", true);
				m_canClose = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == C_TAG_PLAYER)
		{
			characterController2D.gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
			characterController2D.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			characterController2D.enabled = false;
			secretRoomBGM.Play();
			timer.enabled = true;
			m_canClose = true;
		}
	}

	private void OnTimerDone()
	{
		canvas.gameObject.SetActive(true);
		endTimer.enabled = true;
	}

	private void OnEndTimerDone()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		SceneManager.LoadScene("0", LoadSceneMode.Single);
#else
		Application.Quit();
#endif
	}
}
