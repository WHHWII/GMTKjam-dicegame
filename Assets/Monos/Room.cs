using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Room : MonoBehaviour
{
    Player player;
    public List<DiceMob> enemies;
    static Vector3 roomPosition = new Vector3(0, 0, 5);
    private async void Awake()
    {
        roomPosition = GameManager.singleton.roomPositioner.position;
        transform.position = roomPosition;
        player = GameManager.singleton.player;
        foreach(Transform s in transform) // spawn the enemies at the spawn points in the room.
        {
            DiceMob enemy = Instantiate(GameManager.singleton.GetRandomEnemy());
            enemy.transform.position = s.position;
            enemies.Add(enemy);
            s.gameObject.SetActive(false); // hides the spawn icon used for creating the prefabs
        }
        player.target = enemies[0];
        
        while(enemies.Count > 0)
        {
            await ProccessTurn();
        }
    }
    async Task ProccessTurn()
    {
        //pass turn to player.
        await player.PickTarget(enemies.ToArray());
        //pass turn to enemies
        return;
    }
    
}
