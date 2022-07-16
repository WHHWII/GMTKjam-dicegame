using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Targetable
{
    // Start is called before the first frame update
    int roomVal;
    public DiceAnimate dieDisplay;
    void Start()
    {
        roomVal = Random.Range(0,GameManager.singleton.roomPrefabs.Length);
        Debug.Log(roomVal);
        dieDisplay.ShowFace(roomVal);
        Debug.Log("door awake");
    }

    public override void WhenSelected(DiceMob Selector)
    {
        GameManager.singleton.currentRoom = GameManager.singleton.roomPrefabs[roomVal];
    }
}
