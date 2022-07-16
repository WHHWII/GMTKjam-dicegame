using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Room : MonoBehaviour
{
    Player player;
    public Transform[] spawns;
    public List<DiceMob> enemies;
    static Vector2 RoomPosition = Vector2.zero;
    private async void Awake()
    {
        transform.position = RoomPosition;
        player = GameManager.singleton.player;
        spawns = transform.GetComponentsInChildren<Transform>();
        foreach(Transform s in spawns) // spawn the enemies at the spawn points in the room //For some reason it spawns 1 extra enemy.
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
