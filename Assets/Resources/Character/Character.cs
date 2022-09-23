using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    //=============================================================================================
    //Харрактеристики персонажа
    protected float WalkSpeed = 5;
    protected int maxHealth = 10;
    protected int health = 10;
    protected int force = 2;

    public int Health { get { return health; } set { } }
    public int MaxHealth { get { return maxHealth; } set { } }
    //=============================================================================================

    //=============================================================================================
    //События персонажа
    public UnityAction<Character> OnDestroyEvent;
    public UnityAction OnDeadEvent;
    public UnityAction<int> OnHealthChanged;
    //=============================================================================================

    //=============================================================================================
    //Unity методы
    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
    //=============================================================================================

    //=============================================================================================
    //Методы объекта
    public void SetHealth(int NewHealth)
    {
        health = NewHealth;
        if (health <= 0)
        {
            Dead();
            health = 0;
        }
        OnHealthChanged?.Invoke(health);
    }
    public virtual void Dead()
    {
        OnDeadEvent?.Invoke();
    }
    //=============================================================================================

}
