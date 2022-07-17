using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : DiceMob
{
    bool myTurn = false;
    Targetable[] targets;
    int targetIndex = 0;
    int sleepInterval = 50;
    public GameObject extradie1;
    public GameObject extradie2;
    private void Update()
    {
        if(GameManager.singleton.player.Health <= 12)
        {
            extradie1.SetActive(false);
        }if (GameManager.singleton.player.Health <= 6)
        {
            extradie2.SetActive(false);
        }
        
    }
    
}
