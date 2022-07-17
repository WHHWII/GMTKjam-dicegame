using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Room : MonoBehaviour
{
    Player player;
    public List<DiceMob> enemies;
    static Vector3 roomPosition = new Vector3(0, 0, 5);

    bool turnInProgress = false;

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
    }

    private void Update()
    {
        if (turnInProgress == false && enemies.Count > 0)
        {
            turnInProgress = true;
            StartCoroutine(ProccessTurn());
        }
    }

    int nextAttackingEnemyIndex = 0;
    IEnumerator ProccessTurn()
    {
        //pass turn to player.
        List<Targetable> targetableComponents = new List<Targetable>();
        foreach (DiceMob enemy in enemies)
        {
            targetableComponents.Add(enemy.GetComponent<Targetable>());
        }
        yield return StartCoroutine(GameManager.singleton.PickTarget(targetableComponents.ToArray()));
        DiceMob targetedEnemy = GameManager.singleton.currentTarget.GetComponent<DiceMob>();
        yield return player.Attack(targetedEnemy);
        Debug.Log("Reseting face");
        yield return new WaitForSeconds(0.3f);
        yield return player.SetFace(player.Health);
        Debug.Log("Waiting 1 second");
        yield return new WaitForSeconds(1f);

        foreach (DiceMob enemy in enemies.ToArray())
        {
            if (!enemy.alive)
            {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }
        if (enemies.Count > 0)
        {
            Debug.Log("Enemy is attacking");
            nextAttackingEnemyIndex = (nextAttackingEnemyIndex + 1) % enemies.Count;
            DiceMob attackingEnemy = enemies[nextAttackingEnemyIndex];
            yield return attackingEnemy.Attack(player);
            yield return attackingEnemy.SetFace(attackingEnemy.Health);

        }
        turnInProgress = false;
        yield return null;
    }
    
}
