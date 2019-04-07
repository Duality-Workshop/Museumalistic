using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	public delegate void LookPaintingAction(bool value);
	public static event LookPaintingAction OnLookPainting;

	public delegate void FlipAction(bool value);
	public static event FlipAction OnFlip;

	public float maxSpeed = 3f;
	private bool m_facingRight;
	private bool m_frontOfExitPainting;
	private bool m_lookingPainting;
	private bool m_releaseFirst;
	private Rigidbody2D m_rigidbody;
	private Animator m_animator;

	private void Awake()
	{
		CharacterInteractionsPainting.OnFrontOfExitPainting += SetFrontOfExitPainting;
	}

	// Start is called before the first frame update
	void Start()
	{
		m_facingRight = true;
		m_frontOfExitPainting = false;
		m_lookingPainting = false;
		m_releaseFirst = false;
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (m_frontOfExitPainting)
		{
			if (m_lookingPainting)
			{
				float l_move = Mathf.Abs(Input.GetAxisRaw("Horizontal"));

				if (l_move == 1f && m_releaseFirst)
				{
					m_animator.SetBool("Look", false);
					m_lookingPainting = false;
					m_releaseFirst = false;
					OnLookPainting?.Invoke(false);
				}

				if (l_move == 0f)
				{
					m_releaseFirst = true;
				}
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (!m_lookingPainting)
				{
					m_animator.SetFloat("Speed", 0f);
					m_animator.SetBool("Look", true);
					OnLookPainting?.Invoke(true);
				}
				else
				{
					m_animator.SetBool("Look", false);
					m_releaseFirst = false;
					OnLookPainting?.Invoke(false);
				}

				m_lookingPainting = !m_lookingPainting;
			}
		}
	}

	// FixedUpdate is called on per physics call
	void FixedUpdate()
	{
		if (!m_lookingPainting)
		{
			float l_move = Input.GetAxisRaw("Horizontal");

			m_animator.SetFloat("Speed", Mathf.Abs(l_move));

			m_rigidbody.velocity = new Vector2(l_move * maxSpeed, m_rigidbody.velocity.y);

			if (l_move > 0 && !m_facingRight)
			{
				Flip();
			}
			else if (l_move < 0 && m_facingRight)
			{
				Flip();
			}
		}
		else
		{
			m_rigidbody.velocity = new Vector2(0f, m_rigidbody.velocity.y);
		}
	}

	private void Flip()
	{
		m_facingRight = !m_facingRight;
		Vector3 l_LocalScale = transform.localScale;
		l_LocalScale.x *= -1;
		transform.localScale = l_LocalScale;

		OnFlip?.Invoke(!m_facingRight);
	}

	private void SetFrontOfExitPainting(bool value)
	{
		m_frontOfExitPainting = value;
	}
}
