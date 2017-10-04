using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject gameOverUI;
	public GameObject pauseMenuUI;

	void Awake(){
		instance = this;
	}

	void Start(){}

	void Update(){}

	public void UnPause(){}

	public void StartGame(){
		SceneManager.LoadScene ("");
	}

	public void EndGame(){
		Application.Quit ();
	}

	public void GoToMain(){}
}
