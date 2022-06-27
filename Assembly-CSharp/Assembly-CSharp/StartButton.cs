// StartButton
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
	private Button button;

	private void Start()
	{
		button = base.GetComponent<Button>();
		button.onClick.AddListener(StartGame);
	}

	private void StartGame()
	{
		if (base.name == "StoryButton")
		{
			PlayerPrefs.SetString("CurrentMode", "story");
		}
		else if (base.name == "")
		{
			PlayerPrefs.SetString("CurrentMode", "");
		}
		SceneManager.LoadScene("School");
	}
}
