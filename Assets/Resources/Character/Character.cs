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
    protected int force;

    public int Health
    {
        get { return health; }
        set
        {
            if (value <= 0)
                value = 0;

            if (health == value)
                return;

            health = value;
            if (health == 0)
                Dead();
            OnHealthChanged?.Invoke(health);
        }
    }
    public int MaxHealth { get { return maxHealth; } set { } }

    public int Force
    {
        get { return force; }
        set { force = value; }
    }
    //=============================================================================================

    //=============================================================================================
    //Анимации
    protected Animator animator;
    //=============================================================================================

    //=============================================================================================
    //События персонажа
    public UnityAction<Character> OnDestroyEvent;
    public UnityAction OnDeadEvent;
    public UnityAction<int> OnHealthChanged;
    //=============================================================================================

    //=============================================================================================
    //Unity методы
    private void Awake()
    {
        CharacterAwake();
    }
    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
    protected virtual void CharacterAwake()
    {
        animator = GetComponent<Animator>();
    }
    //=============================================================================================

    //=============================================================================================
    //Методы объекта
    public void PlayAnimation(string animName)
    {
        animator.Play(animName);
    }
    public virtual void Dead()
    {
        OnDeadEvent?.Invoke();
    }
    //=============================================================================================

}
