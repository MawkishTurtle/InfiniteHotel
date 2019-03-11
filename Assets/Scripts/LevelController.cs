using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public enum LevelDirection { LeftToRight, RightToLeft }

    public LevelDirection? levelDirection = LevelDirection.LeftToRight;
    public GameObject[] cameraTraceEnds;

    public bool levelIsActive;
    public bool cameraOnLevel;

    private GameObject mainCamera;
    private Vector3 startingPos;
    private Vector3 endingPos;
   
    private GameController gameController;
    private bool waited;
    private float cameraAdditionalHeight = 3.0f;

	void Awake () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        mainCamera = gameController.mainCamera;

    }

    private void OnEnable()
    {
        if (gameController.createdLevels != 0)
        {
            if (gameController.levelsList[gameController.createdLevels - 1].levelDirection == LevelDirection.LeftToRight)
            {
                levelDirection = LevelDirection.RightToLeft;
            }
        }

        mainCamera = gameController.mainCamera;
        cameraTraceEnds = new GameObject[] { new GameObject("CameraLeftEnd"), new GameObject("CameraRightEnd") };
        foreach(GameObject cameraTraceEnd in cameraTraceEnds)
        {
            cameraTraceEnd.transform.SetParent(this.transform);
        }
        cameraTraceEnds[0].transform.localPosition = new Vector3(0.0f, cameraAdditionalHeight, -10.0f);
        cameraTraceEnds[1].transform.localPosition = new Vector3(80.0f, cameraAdditionalHeight, -10.0f);
        print(levelDirection);
        if (levelDirection == LevelDirection.LeftToRight)
        {
            startingPos = cameraTraceEnds[0].transform.position;
            startingPos += new Vector3(0.0f, gameController.createdLevels * gameController.levelHeight);
            endingPos = cameraTraceEnds[1].transform.position;
            endingPos += new Vector3(0.0f, gameController.createdLevels * gameController.levelHeight);
        }
        else
        {
            startingPos = cameraTraceEnds[1].transform.position;
            startingPos += new Vector3(0.0f, gameController.createdLevels * gameController.levelHeight);
            endingPos = cameraTraceEnds[0].transform.position;
            endingPos += new Vector3(0.0f, gameController.createdLevels * gameController.levelHeight);
        }
        gameController.createdLevels += 1;
        ElevatorController[] elevators = GetComponentsInChildren<ElevatorController>();
        foreach (ElevatorController elevator in elevators)
        {
            elevator.ChooseActiveElevator(levelDirection);
        }
        CreateDoors();
    }

    void CreateDoors()
    {
        for(int i = 1; i < 8; i++)
        {
            GameObject door = Instantiate(gameController.doorPrefab, new Vector3(i * 10.0f,1.25f, 0.0f), Quaternion.identity, transform);
        }
    }

    void Update () {
        if(mainCamera == null)
        {
            mainCamera = gameController.mainCamera;
        }

        if (levelIsActive)
        {
            if (cameraOnLevel == false)
            {
                float step = gameController.cameraSpeed * 2.0f * Time.deltaTime;
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, startingPos, step);
                if (mainCamera.transform.position == startingPos)
                {
                    cameraOnLevel = true;
                    if (gameController.inElevator == true)
                    {
                        gameController.mainCharacter.GetComponent<MainCharacterController>().ToggleRenderer(true);
                        gameController.mainCharacter.transform.position = mainCamera.transform.position + Vector3.forward * 10;
                        gameController.cameraSpeed += 0.2f;
                    }
                    else
                    {
                        //You lose.
                        gameController.mainCharacter.GetComponent<MainCharacterController>().ToggleRenderer(true);
                    }
                    gameController.inElevator = false;
                }
            }
            else if(cameraOnLevel == true)
            {
                float step = gameController.cameraSpeed * Time.deltaTime;
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, endingPos, step);
                if ( (gameController.inElevator && gameController.waited == true))
                {
                    gameController.SetLevelActive(gameController.levelsList[gameController.levelInd]);
                    gameController.waited = false;
                    gameController.Wait();
                }
            }
        }
	}


}
