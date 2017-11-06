using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster : MonoBehaviour {

    public enum Action {Tidy, Reset, EndTurn, EndGame}

    private int[] bowls = new int[22];
    private int bowl = 1;
    private bool extraBowl = false;
    

    public Action Bowl (int pins)
    {
        if (pins < 0 || pins > 10) { throw new UnityException("Invalid Pins value"); }
        bowls[bowl] = pins;

        if (bowl >= 21)  {
            return Action.EndGame;
        }

        if (bowl > 18) // last frame
        {
            if (pins == 10) {  // Strike in last frame
                bowl += 1;
                extraBowl = true;
                return Action.Reset;
            }

            if (bowl % 2 == 0)  {
                if (pins + bowls[bowl-1] == 10 && pins != 0)   // Spare in last frame
                {
                    bowl += 1;
                    extraBowl = true;
                    return Action.Reset;
                } else if (extraBowl == true)     // had strike in last frame 
                {
                    bowl += 1;
                    return Action.Tidy;
                }
                return Action.EndGame;            // no strikes or spares in last frame 
            }           
        }

        if (pins == 10)  // Strike
        {
            bowl += 2;
            return Action.EndTurn;
        }

        if (bowl %2 == 0)    //remainder of (bowl / 2), ie end of frame
        {
            bowl += 1;
            return Action.EndTurn;
        } else  {            // First bowl in frame  
            bowl += 1;
            return Action.Tidy;
        }


        throw new UnityException ("Not sure what action to return"); 
    }

}
