using UnityEngine;

public class PaintingBehaviour : MonoBehaviour
{
	private const string C_TAG_PLAYER = "Player";

	private bool m_canPlaySound;
	private AudioSource m_source;

	private void Awake()
	{
		m_canPlaySound = false;
		Timer.OnTimerDone += OnCanPlaySound;
		m_source = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
    {
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == C_TAG_PLAYER)
		{
			if (m_canPlaySound)
			{
				playSound();
			}
		}
	}

	private void playSound()
	{
		m_source.Play();
	}

	private void OnCanPlaySound()
	{
		m_canPlaySound = true;
	}
}
