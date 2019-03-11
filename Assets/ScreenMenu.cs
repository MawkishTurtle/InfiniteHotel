using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenMenu : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
    }
	
	// Update is called once per frame
	public void CloseGame() {
        Application.Quit();
	}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseGame();
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
