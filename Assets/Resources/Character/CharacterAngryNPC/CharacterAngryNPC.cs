using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAngryNPC : Character, IMissileVisitor
{
    //=============================================================================================
    //������ �� ������ �������
    public Character Target { get; private set; }
    public int AttackDistance { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //������ ���������
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //����������� ������
    public static CharacterAngryNPC CreateMe(int CaracterForce, Vector3 StartPos = new Vector3())
    {
        CharacterAngryNPC NewObject = Instantiate(Resources.Load<CharacterAngryNPC>("Character/CharacterAngryNPC/objAngryNPC"));
        NewObject.transform.position = StartPos;
        NewObject.force = CaracterForce;
        return NewObject;
    }
    //=============================================================================================

    //=============================================================================================
    //������ Unity
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
    //������ �������

    //��������, ������� ����� ��������� ������
    public void CommandToFollow(Character target)
    {
        (stateMachine.CurrentState as AngryNPCState).CommandToFollow(target);
    }

    //������ ��� ������������
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

    //������ ��� �����
    public void StartAttacking()
    {
        stateMachine.ChangeState(new AngryNPCStateAttacking(this));
    }
    //���������� �� ������� � ��������
    public void HitTarget()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) > AttackDistance)
        {
            FollowTarget(Target);
            return;
        }
        Target.Health = Target.Health - force;
    }

    //����� ��� �����������
    public void CalmDown()
    {
        Target = null;
        stateMachine.ChangeState(new AngryNPCStateIdle(this));
    }

    //������� �������
    public void Shooted(Missile missile)
    {
        Health = Health - missile.Damage;
    }

    //��������������� ������ ����
    public override void Dead()
    {
        Destroy(gameObject);

        int Casino = Random.Range(1, 100);
        if(Casino >= 1 && Casino <= 50)
            Bonus.CreateMe(transform.position + new Vector3(0,2,0), 2);
    }
    //=============================================================================================
}
