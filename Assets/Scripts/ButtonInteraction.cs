using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
	public delegate void PushButtonAction(ButtonColor buttonColor);
	public static event PushButtonAction OnPushButton;

	[System.Serializable]
	public enum ButtonColor
	{
		RED,
		GREEN,
		BLUE,
		YELLOW
	}

	public ButtonColor buttonColor;
	public Sprite buttonUp;
	public Sprite buttonDown;

	private bool m_buttonUp;
	private SpriteRenderer m_spriteRenderer;
	private AudioSource m_sound;

	private void Awake()
	{
		switch (buttonColor)
		{
			case ButtonColor.RED:
				CharacterInteractionsConsole.OnPushRedButton += changeButton;
			break;
			case ButtonColor.GREEN:
				CharacterInteractionsConsole.OnPushGreenButton += changeButton;
			break;
			case ButtonColor.BLUE:
				CharacterInteractionsConsole.OnPushBlueButton += changeButton;
			break;
			case ButtonColor.YELLOW:
				CharacterInteractionsConsole.OnPushYellowButton += changeButton;
			break;
		}

		m_sound = GetComponent<AudioSource>();

		ConsoleBehaviour.OnResetButton += resetButton;
	}

	// Start is called before the first frame update
	void Start()
    {
		m_buttonUp = true;
		m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

	private void changeButton()
	{
		if (m_buttonUp)
		{
			m_spriteRenderer.sprite = buttonDown;
			m_buttonUp = false;
			m_sound.Play();
			OnPushButton?.Invoke(buttonColor);
		}
	}

	private void resetButton()
	{
		m_spriteRenderer.sprite = buttonUp;
		m_buttonUp = true;
	}
}
