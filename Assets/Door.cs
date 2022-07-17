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
        Room currentRoom = GameManager.singleton.currentRoom;
        GameManager.singleton.currentRoom = Instantiate(GameManager.singleton.roomPrefabs[roomVal-1]);
        Destroy(currentRoom);
        foreach(Door door in GameManager.singleton.doors.ToArray())
        {
            GameManager.singleton.doors.Remove(door);
            Destroy(door);
        }

    }
}
