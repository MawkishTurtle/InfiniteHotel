using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCanvas : MonoBehaviour {

    GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameController.SetLevelActive(gameController.levelsList[0]);
            gameController.pointsCounter.gameStarted = true;
            this.gameObject.SetActive(false);
        }
	}
}
