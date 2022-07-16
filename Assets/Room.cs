using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Room : MonoBehaviour
{
    Player player;
    public List<DiceMob> enemies;
    private async void Awake()
    {
        player = GameManager.singleton.player;
        //populate list of enemies and spawn enemies based on prefab
        player.target = enemies[0];
        //pass turn to player.
        while(enemies.Count > 0)
        {
            await ProccessTurn();
        }
    }
    async Task ProccessTurn()
    {
        await player.PickTarget(enemies.ToArray());
        return;
    }
    
}
