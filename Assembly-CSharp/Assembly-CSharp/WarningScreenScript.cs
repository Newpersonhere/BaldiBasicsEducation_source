// WarningScreenScript
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotWarningScreenScript : MonoBehaviour
{
	private void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
