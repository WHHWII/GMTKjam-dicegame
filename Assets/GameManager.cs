using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public Room currentRoom;
    public static GameManager singleton;
    public DiceMob[] enemyPrefabs; //referenced by room class to spawn random enemies
    public Room[] roomPrefabs;

    Targetable[] targets;
    bool isSelectingTarget = false;
    int targetIndex = 0;
    Targetable selectedTarget;
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

    
    void Update()
    {
        if (isSelectingTarget) // While selecting target, player may use arrow keys to cycle their target index, corresponding with the list of targets
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (targetIndex > 0)
                {
                    --targetIndex;
                    selectedTarget = targets[targetIndex];
                }

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (targetIndex < targets.Length - 1)
                {
                    ++targetIndex;
                    selectedTarget = targets[targetIndex];
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                targets[targetIndex].WhenSelected(player);
                isSelectingTarget = false;
            }
        }
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
    public async Task PickTarget(Targetable[] possibleTargets, int targIndx = 0) // Waits for player to select a target.
    {
        targets = possibleTargets;
        isSelectingTarget = true;
        targetIndex = targIndx;
        while (isSelectingTarget)
        {
            await Task.Delay(50);
        }
        Debug.Log("Turn ended");
        return;
    }
}
