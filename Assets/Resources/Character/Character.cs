using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    //=============================================================================================
    //Харрактеристики персонажа
    protected float WalkSpeed = 5;
    protected int health = 10;
    protected int force = 2;

    public int Health { get { return health; } set { } }
    //=============================================================================================

    //=============================================================================================
    //События персонажа
    public UnityAction<Character> OnDestroyEvent;
    public UnityAction OnDeadEvent;
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
        if (health < 0)
            Dead();
    }
    public virtual void Dead()
    {
        OnDeadEvent?.Invoke();
    }
    //=============================================================================================

}
