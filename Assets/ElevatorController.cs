using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

	public enum ElevatorSide { Left, Right}
    public ElevatorSide elevatorSide;

    private GameController gameController;
    private Collider2D colli;

    public void ChooseActiveElevator(LevelController.LevelDirection? levelDirection)
    {
        colli = GetComponent<Collider2D>();
        if (levelDirection == LevelController.LevelDirection.LeftToRight && elevatorSide == ElevatorSide.Left)
        {
           colli.enabled = false;
        }
        if (levelDirection == LevelController.LevelDirection.RightToLeft && elevatorSide == ElevatorSide.Right)
        {
            colli.enabled = false;
        }
    }

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        colli = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (collision.name == "MainCharacter")
            {
                gameController.inElevator = true;
                collision.GetComponent<MainCharacterController>().ToggleRenderer(false);
                gameController.SetLevelActive(gameController.levelsList[gameController.levelInd]);
                colli.enabled = false;
            }
        }
    }
}
