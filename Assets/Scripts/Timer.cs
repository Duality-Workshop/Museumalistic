using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
	public delegate void TimerDoneAction();
	public static event TimerDoneAction OnTimerDone;

	public float seconds = 300;

	private TextMeshProUGUI m_text;
	private bool m_done;

    // Start is called before the first frame update
    void Start()
    {
		m_text = GetComponent<TextMeshProUGUI>();
		m_done = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!m_done)
		{
			seconds -= Time.deltaTime;

			if (seconds <= 0)
			{
				m_done = true;
				OnTimerDone?.Invoke();
			}
		}
    }
}
