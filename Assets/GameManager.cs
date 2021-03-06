using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player player;
    public Player playerPrefab;
    public Room currentRoom;
    public static GameManager singleton;
    public DiceMob[] enemyPrefabs; //referenced by room class to spawn random enemies
    public Room[] roomPrefabs;
    public Door door;
    public List<Door> doors;

    Targetable[] targets;
    bool isSelectingTarget = false;
    int targetIndex = 0;
    public Targetable currentTarget;
    public Transform roomPositioner;
    public Transform[] doorPositioners;
    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;//Sam told me to put this here for somethign about dont destroy on load
        if(roomPrefabs.Length > 0)
        {
            currentRoom = Instantiate(roomPrefabs[0]);
        }
    }
    public void RestartGame()
    {
        DiceAnimate dieani = player.GetComponent<DiceAnimate>();
        Destroy(player.gameObject);
        player = Instantiate(playerPrefab);
        player.Health = dieani.dieSprites.Length-1;
        dieani.ShowFace(dieani.dieSprites.Length);
        foreach(DiceMob enemy in currentRoom.enemies.ToArray())
        {
            currentRoom.enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
        
        Destroy(currentRoom.gameObject);
        currentRoom = Instantiate(roomPrefabs[0]);
    }
    
    void Update()
    {
        if (isSelectingTarget) // While selecting target, player may use arrow keys to cycle their target index, corresponding with the list of targets
        { 

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ++targetIndex;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    --targetIndex;
                }
                if (targetIndex < 0) targetIndex = targets.Length - 1;
                targetIndex = targetIndex % targets.Length;
                if (currentTarget)
                {
                    currentTarget.EnableTargetedVisual(false);
                }
                currentTarget = targets[targetIndex];
                currentTarget.EnableTargetedVisual(true);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                currentTarget.EnableTargetedVisual(false);
                targets[targetIndex].WhenSelectedBy(player);
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
    public IEnumerator PickTarget(Targetable[] possibleTargets, int targIndx = 0) // Waits for player to select a target.
    {
        targets = possibleTargets;
        isSelectingTarget = true;
        targetIndex = targIndx;
        currentTarget = targets[targetIndex];
        currentTarget.EnableTargetedVisual(true);
        while (isSelectingTarget)
        {
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("Turn ended");
        yield return currentTarget;
    }
    public void SpawnDoors()
    {
        for(int i = 0;i<doorPositioners.Length;i++)
        {
            Door _door = Instantiate(door);
            _door.transform.position = doorPositioners[i].position;
            _door.transform.rotation = Quaternion.Euler(0, 0, 90*i);
            doors.Add(_door);
        }
    }
}
