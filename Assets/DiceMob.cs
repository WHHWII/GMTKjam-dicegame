using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiceAnimate))]
public class DiceMob : MonoBehaviour
{
    
    protected int baseHealth;
    protected int curHealth;
    public DiceMob target;
    string name;
    DiceAnimate diceAnimator;
   
    
    
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
        diceAnimator.NextFace(-damage);
        //cycle die face down to damage number;
    }

    void Attack()
    {

    }

    void Die()
    {

    }
}
