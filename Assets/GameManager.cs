using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public Room currentRoom;
    public static GameManager singleton;
    public DiceMob[] enemyPrefabs; //referenced by room class to spawn random enemies
    public Room[] roomPrefabs;
    public Transform roomPositioner;
    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;//Sam told me to put this here for somethign about dont destroy on load
        if(roomPrefabs.Length > 0)
        {
            currentRoom = Instantiate(GetRandomRoom());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Room GetRandomRoom()
    {
        return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    }
    public DiceMob GetRandomEnemy()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }
    public Room GetRoom(int index)
    {
        return roomPrefabs[index];
    }
}
