using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour {

    private Text pointsText;

    public Text scoreField;

    public int points;

    private float timePassed = 0.0f;
    private float startTime;
    private int totalPoints;

    public bool gameStarted;

	// Use this for initialization
	void Awake () {
        pointsText = GetComponentInChildren<Text>();
        startTime = Time.time;
	}
	
    void AddTimePoints()
    {
        timePassed += Time.deltaTime;
        float pointedTime = 10 * (timePassed - startTime);
        totalPoints = (int)pointedTime + points;
    }

    void DrawPoints()
    {
        pointsText.text = totalPoints.ToString();
    }

    public void SetScoreField()
    {
        scoreField.text = "Score: " + totalPoints;
    }

	// Update is called once per frame
	void Update () {
        if (gameStarted == true)
        {
            AddTimePoints();
            DrawPoints();
        }
	}
}
