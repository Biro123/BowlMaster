using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster3 {

    public enum Action {Tidy, Reset, EndTurn, EndGame}

    private int[] bowls = new int[22];
    private int bowl = 1;
    private bool extraBowl = false;
    
    public static Action NextAction(List<int> pinFalls  )
    {
        ActionMaster3 am = new ActionMaster3();
        Action eachAction = new Action();

        foreach (int pinFall in pinFalls)
        {
            eachAction = am.Bowl(pinFall);
        }
        return eachAction;
    }

    private Action Bowl (int pins)  
    {
        if (pins < 0 || pins > 10) { throw new UnityException("Invalid Pins value: " + pins); }
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
        
        if (bowl %2 == 0)  {    //remainder of (bowl / 2), ie end of frame
            bowl += 1;
            return Action.EndTurn;
        } else  {            // First bowl in frame  
            if (pins == 10)  { // Strike (score 10 on first bowl)            
                bowl += 2;
                return Action.EndTurn;
            } else {
                bowl += 1;
                return Action.Tidy;
            }
        }
        
        throw new UnityException ("Not sure what action to return"); 
    }

}
