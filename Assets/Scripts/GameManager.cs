using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<int> bowls = new List<int>();
    private Ball ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;

    // Use this for initialization
    void Start () {
        ball = FindObjectOfType<Ball>();
        pinSetter = FindObjectOfType<PinSetter>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }
	
    public void Bowl (int pinFall)
    {
        try
        {
            ball.Reset();
            bowls.Add(pinFall);
            ActionMaster3.Action nextAction = ActionMaster3.NextAction(bowls);
            pinSetter.PerformAction(nextAction);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Something bad happened in Bowl");
        }

        try
        {
            scoreDisplay.FillRolls(bowls);
            scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(bowls));
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Error in FillRollCard");
        }
        
    }
}
