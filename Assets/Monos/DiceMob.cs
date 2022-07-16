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
        if(curHealth <= 0 && alive)
        {
            alive = false;
            Die();
        }
    }

    void TakeDamage(int damage)
    {
        if (!alive)
        {
            Debug.Log("he dead");
            return;
        }
        curHealth -= damage;
        diceAnimator.ShowFace(curHealth - damage);
        Debug.Log($"{gameObject.name} took {damage} damage and now has {curHealth} health remaining!");
        //cycle die face down to damage number;
    }
    
    void Attack(DiceMob foe)
    {
        int damage = Random.Range(1, curHealth);
        Debug.Log($"you rolled a {damage}!");
        diceAnimator.ShowFace(damage);
        foe.TakeDamage(damage);
    }

    void Die()
    {
        StartCoroutine(diceAnimator.PlayDeath());
        //Destroy(gameObject);
    }
    public override void WhenSelected(DiceMob selector)
    {
        selector.Attack(this);
    }
}
