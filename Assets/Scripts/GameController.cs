using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour {

 
    public List<LevelController> levelsList;
    [HideInInspector]
    public readonly float levelHeight = 10.0f;
    public GameObject mainCamera;
    [HideInInspector]
    public int levelInd;
    [HideInInspector]
    public GameObject mainCharacter;

    public PointsCounter pointsCounter;

    public GameObject levelPrefab;
    public GameObject doorPrefab;
    public float cameraSpeed;

    [HideInInspector]
    public bool inElevator;

    public int createdLevels = 0;
    private int levelsOnScene;

    public bool waited;

    private void Awake()
    {
        mainCharacter = GameObject.Find("MainCharacter");
        mainCamera = GameObject.Find("MainCamera");
        GetComponentsInChildren<LevelController>(levelsList);
        pointsCounter = GameObject.Find("PointsCanvas").GetComponent<PointsCounter>();
    }

    public void Wait()
    {
        Invoke("Waited", 2.0f);
    }

    private void Waited()
    {
        waited = true;
    }

    GameObject InstantiateNewLevel()
    {
        GameObject newGameObject = Instantiate(levelPrefab, transform);
        newGameObject.transform.position = new Vector3(newGameObject.transform.position.x, levelsList.Last().transform.position.y + levelHeight, newGameObject.transform.position.z );
        levelsList.Add(newGameObject.GetComponent<LevelController>());
        return newGameObject;
    }

    public void SetLevelActive(LevelController activatedLevelController)
    {
        foreach(LevelController levelController in levelsList)
        {
            if(levelController == activatedLevelController)
            {
                levelController.levelIsActive = true;
            }
            else
            {
                levelController.levelIsActive = false;
            }
        }
        levelInd += 1;
    }

    public IEnumerator SpeedUpForTime(float additionalSpeed,float time)
    {
        float buffSpeed = cameraSpeed;
        cameraSpeed += additionalSpeed;
        yield return new WaitForSeconds(time);
        cameraSpeed = buffSpeed + 0.4f;
    }

	// Use this for initialization
	void Start () {
        InstantiateNewLevel();
        InstantiateNewLevel();

        //SetLevelActive(levelsList[0]);
	}

    public void TurnOffLevel()
    {
        foreach (LevelController level in levelsList)
        {
            level.levelIsActive = false;
        }
    }

    // Update is called once per frame
    void Update () {
		if(mainCamera.transform.position.y / levelHeight >= levelsList.Count() - 2)
        {
            InstantiateNewLevel();
        }
	}
}
