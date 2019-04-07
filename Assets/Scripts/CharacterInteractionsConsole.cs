using UnityEngine;

public class CharacterInteractionsConsole : MonoBehaviour
{
	public delegate void PushRedButtonAction();
	public static event PushRedButtonAction OnPushRedButton;

	public delegate void PushGreenButtonAction();
	public static event PushGreenButtonAction OnPushGreenButton;

	public delegate void PushBlueButtonAction();
	public static event PushBlueButtonAction OnPushBlueButton;

	public delegate void PushYellowButtonAction();
	public static event PushYellowButtonAction OnPushYellowButton;

	private const string C_NAME_BUTTON_RED		= "Red";
	private const string C_NAME_BUTTON_GREEN	= "Green";
	private const string C_NAME_BUTTON_BLUE		= "Blue";
	private const string C_NAME_BUTTON_YELLOW	= "Yellow";
	private const string C_TAG_BUTTON			= "button";

	private bool m_isFrontButtonRed;
	private bool m_isFrontButtonGreen;
	private bool m_isFrontButtonBlue;
	private bool m_isFrontButtonYellow;

	// Start is called before the first frame update
	void Start()
    {
		m_isFrontButtonRed		= false;
		m_isFrontButtonGreen	= false;
		m_isFrontButtonBlue		= false;
		m_isFrontButtonYellow	= false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (m_isFrontButtonRed)
			{
				OnPushRedButton?.Invoke();
			}
			else if (m_isFrontButtonGreen)
			{
				OnPushGreenButton?.Invoke();
			}
			else if (m_isFrontButtonBlue)
			{
				OnPushBlueButton?.Invoke();
			}
			else if (m_isFrontButtonYellow)
			{
				OnPushYellowButton?.Invoke();
			}
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == C_TAG_BUTTON)
		{
			switch (collision.gameObject.name)
			{
				case C_NAME_BUTTON_RED:
					m_isFrontButtonRed = true;
				break;
				case C_NAME_BUTTON_GREEN:
					m_isFrontButtonGreen = true;
				break;
				case C_NAME_BUTTON_BLUE:
					m_isFrontButtonBlue = true;
				break;
				case C_NAME_BUTTON_YELLOW:
					m_isFrontButtonYellow = true;
				break;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == C_TAG_BUTTON)
		{
			switch (collision.gameObject.name)
			{
				case C_NAME_BUTTON_RED:
					m_isFrontButtonRed = false;
					break;
				case C_NAME_BUTTON_GREEN:
					m_isFrontButtonGreen = false;
					break;
				case C_NAME_BUTTON_BLUE:
					m_isFrontButtonBlue = false;
					break;
				case C_NAME_BUTTON_YELLOW:
					m_isFrontButtonYellow = false;
					break;
			}
		}
	}
}
