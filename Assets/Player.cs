using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : DiceMob
{
    bool myTurn = false;
    DiceMob[] targets;
    int targetIndex = 0;
    int sleepInterval = 50;
    public async Task PickTarget(DiceMob[] possibleTargets, int targIndx= 0)
    {
        targets = possibleTargets;
        myTurn = true;
        targetIndex = targIndx;
        while (myTurn)
        {
            await Task.Delay(sleepInterval);
        }
        Debug.Log("Turn ended");
        return;
    }
    private void Update()
    {
        if (myTurn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(targetIndex > 0)
                {
                    --targetIndex;
                    target = targets[targetIndex];
                }
                
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(targetIndex < targets.Length-1)
                {
                    ++targetIndex;
                    target = targets[targetIndex];
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                myTurn = false;
            }
        }
            
    }
    
}
