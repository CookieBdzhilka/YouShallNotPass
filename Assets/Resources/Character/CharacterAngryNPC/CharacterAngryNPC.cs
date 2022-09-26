using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAngryNPC : Character, IMissileVisitor
{
    //=============================================================================================
    //Ссылки на другие объекты
    public Character Target { get; private set; }
    public int AttackDistance { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //Машина состояний
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //Статические методы
    public static CharacterAngryNPC CreateMe(int CaracterForce, Vector3 StartPos = new Vector3())
    {
        CharacterAngryNPC NewObject = Instantiate(Resources.Load<CharacterAngryNPC>("Character/CharacterAngryNPC/objAngryNPC"));
        NewObject.transform.position = StartPos;
        NewObject.force = CaracterForce;
        return NewObject;
    }
    //=============================================================================================

    //=============================================================================================
    //Методы Unity
    protected override void CharacterAwake()
    {
        base.CharacterAwake();
        AttackDistance = 4;
        stateMachine = new StateMachine();
        stateMachine.Initialize(new AngryNPCStateIdle(this));
    }
    public void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    public void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    //=============================================================================================

    //=============================================================================================
    //Методы объекта

    //Комманды, которые может исполинть объект
    public void CommandToFollow(Character target)
    {
        (stateMachine.CurrentState as AngryNPCState).CommandToFollow(target);
    }

    //Методы для передвижения
    public void FollowTarget(Character target)
    {
        Target = target;
        stateMachine.ChangeState(new AngryNPCStateFollow(this));
    }
    public void MoveToTarget()
    {
        if (Target == null)
            return;

        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dir = (Target.transform.position - rb.transform.position).normalized * WalkSpeed;
        rb.MovePosition(transform.position + dir * Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.LookRotation(dir * Time.fixedDeltaTime));
    }

    //Методы для атаки
    public void StartAttacking()
    {
        stateMachine.ChangeState(new AngryNPCStateAttacking(this));
    }
    //Вызывается на событие в анимации
    public void HitTarget()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) > AttackDistance)
        {
            FollowTarget(Target);
            return;
        }
        Target.Health = Target.Health - force;
    }

    //Метод для спокойствия
    public void CalmDown()
    {
        Target = null;
        stateMachine.ChangeState(new AngryNPCStateIdle(this));
    }

    //Реакции объекта
    public void Shooted(Missile missile)
    {
        Health = Health - missile.Damage;
    }

    //Переопределение метода отца
    public override void Dead()
    {
        Destroy(gameObject);

        int Casino = Random.Range(1, 100);
        if(Casino >= 1 && Casino <= 50)
            Bonus.CreateMe(transform.position + new Vector3(0,2,0), 2);
    }
    //=============================================================================================
}
