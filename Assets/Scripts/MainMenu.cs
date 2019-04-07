using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayButton()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
