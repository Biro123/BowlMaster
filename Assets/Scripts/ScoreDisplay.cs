using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollTexts, frameTexts;

    public void FillRolls(List<int> rolls)
    {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++)
        {
            //rollTexts[i].text = rolls[i].ToString();
            rollTexts[i].text = scoresString.Substring(i,1);
        }
    }

    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameTexts[i].text = frames[i].ToString();
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        string output = "";
        int bowlInFrame = 0;

        for (int i = 0; i < rolls.Count; i++)
        {
            bowlInFrame++;
            if (bowlInFrame > 2 && output.Length < 18)  // Not Last Frame
            {
                bowlInFrame = 1;
            }
            
            if (output.Length >= 18)  // Last Frame
            {
                if (rolls[i] == 10)
                {
                    output = output + "X";
                }
                else
                {
                    if (rolls[i] > 0)
                    {
                        output = output + rolls[i].ToString();
                    }
                    else
                    {
                        output = output + "-";
                    }                    
                }
            } 
            else if (bowlInFrame == 1 && rolls[i] == 10)  // STRIKE
                {
                    output = output + "X ";
                    bowlInFrame = 0;
                }
                else if (bowlInFrame == 2 && rolls[i - 1] + rolls[i] == 10)   // SPARE
                    {
                        output = output + "/";
                        bowlInFrame = 0;
                    }
                    else if (rolls[i] > 0)
                        {
                            output = output + rolls[i].ToString();
                        }
                        else
                        {
                            output = output + "-";
                        }

        }

        return output;
    }
}
