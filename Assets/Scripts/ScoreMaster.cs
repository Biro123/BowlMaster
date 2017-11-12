using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    static int[,] bowlsToIncludeInFrames; 
    private enum BowlType { strike, spare, normal };


    // Returns a list of cumulative scores - like a normal scorecard
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls) )
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }

    // returns a list of individual frame scores
    public static List<int> ScoreFrames(List<int> bowls)
    {
        bowlsToIncludeInFrames = new int[11, 3];
        List<int> frameList = new List<int>();

        int bowlOne = 0;
        int currentFrame = 1;
        int frameScore = 0;
        int bowlInFrame = 1;
        int bowlCount = 0;

        // Reset the list of bowls to include in each frame
        for (int i = 1; i < 11; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                bowlsToIncludeInFrames[i, j] = 0;
            }
        }

        // first loop is to determine which bowls are to be added to which frame - bowlsToIncludeInFrames
        foreach (int bowl in bowls)
        {
            bowlCount++;
            if (bowlInFrame == 1)
            {
                bowlOne = bowl;
                if (bowl == 10) {
                    AddBowlToFrame(currentFrame, bowlCount, BowlType.strike);
                } else  {
                    AddBowlToFrame(currentFrame, bowlCount, BowlType.normal);
                }
            }

            if (bowlInFrame == 2)
            {
                if (bowl + bowlOne == 10)  {
                    AddBowlToFrame(currentFrame, bowlCount, BowlType.spare);
                } else  {
                    AddBowlToFrame(currentFrame, bowlCount, BowlType.normal);
                }
            }

            if (bowlInFrame == 1) {
                if (bowl == 10) { // skip 2nd shot on strike                
                    if (currentFrame < 10) {
                        currentFrame++;
                    }
                } else {
                    bowlInFrame = 2;
                }
            }    
            else if (bowlInFrame == 2) {
                if (currentFrame < 10) {
                    bowlInFrame = 1;
                    currentFrame++;
                } else {
                    bowlInFrame = 3;
                }
            }            
        }

        // 2nd loop is to add the values of each bowl which pertains to the current frame
        for (int i = 1; i < 11; i++)
        {
            frameScore = 0;
            int bowlsFound = 0;
            bool allBowlsFound = true;
            for (int j = 0; j < 3; j++)
            {
                if (bowlsToIncludeInFrames[i, j] != 0)
                {
                    if (bowlsToIncludeInFrames[i, j] <= bowls.Count)
                    {
                        frameScore += bowls[bowlsToIncludeInFrames[i, j] - 1];
                        bowlsFound++;
                    }
                    else
                    {
                        allBowlsFound = false;
                    }
                }
            }

            if (bowlsFound == 3)
            {   // maximum bowls - so frame finished.
                frameList.Add(frameScore);
            }
            else if (bowlsFound == 2 && allBowlsFound)
            {
                frameList.Add(frameScore);
            }

        }
 
        return frameList;
    }

    private static void AddBowlToFrame(int frameNumber, int bowlNum, BowlType bowlType)
    {
        for (int i = 0; i < 3; i++)
        {
            if (bowlsToIncludeInFrames[frameNumber, i] == 0)
            {
                bowlsToIncludeInFrames[frameNumber, i] = bowlNum;
                i = 3;
            }
        }

        if (bowlType == BowlType.spare || bowlType == BowlType.strike)
        {
            for (int i = 0; i < 3; i++)
            {
                if (bowlsToIncludeInFrames[frameNumber, i] == 0)
                {
                    bowlsToIncludeInFrames[frameNumber, i] = bowlNum + 1;
                    i = 3;
                }
            }
        }

        if (bowlType == BowlType.strike)
        {
            for (int i = 0; i < 3; i++)
            {
                if (bowlsToIncludeInFrames[frameNumber, i] == 0)
                {
                    bowlsToIncludeInFrames[frameNumber, i] = bowlNum + 2;
                    i = 3;
                }
            }
        }
    }


}
