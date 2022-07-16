using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiceAnimate))]
public class DiceMob : Targetable
{
    
    protected int baseHealth;
    protected int curHealth;
    public Targetable target;
    DiceAnimate diceAnimator;
    static int mobCounter = 0;
   
    
    
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
        if(curHealth <= 0)
        {
            Die();
        }
    }

    void TakeDamage(int damage)
    {
        curHealth -= damage;
        diceAnimator.CycleFace(-damage);
        //cycle die face down to damage number;
    }

    void Attack()
    {

    }

    void Die()
    {

    }
}
