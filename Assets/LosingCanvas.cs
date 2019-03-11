using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosingCanvas : MonoBehaviour {

    public Image image;
    public Text text;
    bool fadeIn;

	// Use this for initialization
	void Awake () {
        image.CrossFadeAlpha(0.0f, 0.0f, false);
        text.CrossFadeAlpha(0.0f, 0.0f, false);
	}
	
    public void FadeIn()
    {
        fadeIn = true;
    }

	// Update is called once per frame
	void Update () {
        if (fadeIn == true)
        {
            image.CrossFadeAlpha(1, 3.0f, false);
            text.CrossFadeAlpha(1, 3.0f, false);
        }
    }
}
