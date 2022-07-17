using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiceAnimate))]
public class DiceMob : Targetable
{

    protected int baseHealth = 6;
    protected int curHealth;
    public Targetable target;
    DiceAnimate diceAnimator;
    static int mobCounter = 0;
    public bool alive { get; private set; } = true;
   
    
    
    public int Health
    {
        get
        {
            return curHealth;
        }
    }

    void Start()
    {
        curHealth = baseHealth;
        ++mobCounter;
        gameObject.name = gameObject.name + mobCounter.ToString();
        diceAnimator = gameObject.GetComponent<DiceAnimate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(int damage)
    {
        if (!alive)
        {
            return;
        }
        curHealth -= damage;
        if (curHealth <= 0 && alive)
        {
            curHealth = 0;
            alive = false;
            Die();
            Debug.Log($"{gameObject.name} Died");
        }
        diceAnimator.ShowFace(curHealth);
        Debug.Log($"{gameObject.name} took {damage} damage and now has {curHealth} health remaining!");
        //cycle die face down to damage number;
    }
    
    public Coroutine Attack(DiceMob foe)
    {
        diceAnimator.spriteRenderer.color = Color.red;
        int damage = Random.Range(1, curHealth);
        Debug.Log($"{gameObject.name} rolled a {damage}!");
        foe.TakeDamage(damage);
        return diceAnimator.ShowFace(damage); 
    }

    public Coroutine SetFace(int face)
    {
        diceAnimator.spriteRenderer.color = Color.white;
        return diceAnimator.ShowFace(face);
    }

    void Die()
    {
        StartCoroutine(diceAnimator.PlayDeath());
        //Destroy(gameObject);
    }
    public override void WhenSelectedBy(DiceMob selector)
    {
        //selector.Attack(this);
    }
}
