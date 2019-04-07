using UnityEngine;

public class ToolTipBehaviour : MonoBehaviour
{
	private SpriteRenderer m_spriteRenderer;

	private void Awake()
	{
		CharacterController2D.OnFlip += Flip;
	}

	// Start is called before the first frame update
	void Start()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Flip(bool value)
	{
		if (transform.localScale.x < 0)
		{
			Vector3 l_localScale = transform.localScale;
			l_localScale.x *= -1;
			transform.localScale = l_localScale;
		}

		m_spriteRenderer.flipX = value;
	}
}
