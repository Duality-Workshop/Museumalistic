using System;
using UnityEngine;

public class CharacterInteractionsPainting : MonoBehaviour
{
	public delegate void FrontOfExitPaintingAction(bool value);
	public static event FrontOfExitPaintingAction OnFrontOfExitPainting;

	private const string C_TAG_PAINTING = "painting";

	public GameObject toolTip;

	private void Awake()
	{
		CharacterController2D.OnLookPainting += disableToolTip;
	}
	
	private void disableToolTip(bool value)
	{
		toolTip.SetActive(!value);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == C_TAG_PAINTING)
		{
			OnFrontOfExitPainting?.Invoke(true);
			disableToolTip(false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == C_TAG_PAINTING)
		{
			OnFrontOfExitPainting?.Invoke(false);
			disableToolTip(true);
		}
	}
}
