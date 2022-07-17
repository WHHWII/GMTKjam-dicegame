using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Targetable
{
    // Start is called before the first frame update
    int roomVal;
    public DiceAnimate dieDisplay;
    void Awake()
    {
        roomVal = Random.Range(1,GameManager.singleton.roomPrefabs.Length);
        dieDisplay.ShowFace(roomVal);
    }
    
    public override void WhenSelectedBy(DiceMob Selector)
    {
        GameManager.singleton.currentRoom = GameManager.singleton.roomPrefabs[roomVal];
    }
}
