using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    int score = 0;
    Text scoreBoardText;

	// Use this for initialization
	void Start () {
        scoreBoardText = GetComponent<Text>();
        scoreBoardText.text = score.ToString();
	}

    public void ScoreHit(int scorePerHit)
    {
        score = score + scorePerHit;
        scoreBoardText.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
