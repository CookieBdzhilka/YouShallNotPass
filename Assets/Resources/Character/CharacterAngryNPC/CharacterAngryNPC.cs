using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAngryNPC : Character, IMissileVisitor
{
    //=============================================================================================
    //������ �� ������ �������
    public Character Target { get; private set; }
    public AreaMelee MeleeArea;
    //=============================================================================================

    //=============================================================================================
    //������ ���������
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //����������� ������
    public static CharacterAngryNPC CreateMe(Vector3 StartPos = new Vector3())
    {
        CharacterAngryNPC NewObject = Resources.Load<CharacterAngryNPC>("Character/CharacterAngryNPC/objAngryNPC");
        NewObject.transform.position = StartPos;
        return Instantiate(NewObject);
    }
    //=============================================================================================

    //=============================================================================================
    //������ Unity
    protected override void CharacterAwake()
    {
        base.CharacterAwake();
        stateMachine = new StateMachine();
        stateMachine.Initialize(new AngryNPCStateIdle(this));
    }
    public void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    private void OnEnable()
    {
        MeleeArea.AtRange += StartAttacking;
        MeleeArea.OutOfRange += FollowTarget;
    }
    private void OnDisable()
    {
        MeleeArea.AtRange -= StartAttacking;
        MeleeArea.OutOfRange -= FollowTarget;
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
        stateMachine.ChangeState(new AngryNPCStateAttack(this));
    }
    public void MoveToTarget()
    {
        if (Target == null)
            return;

        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dir = (Target.transform.position - rb.transform.position).normalized * WalkSpeed;
        rb.MovePosition(transform.position + dir * Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.LookRotation(dir));
    }

    //������ ��� �����
    public void StartAttacking(Character character)
    {
        stateMachine.ChangeState(new AngryNPCStateAttacking(this));
    }
    public void HitTarget()
    {
        Character CharacterTarget = Target.GetComponent<Character>();
        CharacterTarget.SetHealth(CharacterTarget.Health - force);
    }

    //����� ��� �����������
    public void CalmDown()
    {
        Target = null;
        MeleeArea.Sleep = true;
        stateMachine.ChangeState(new AngryNPCStateIdle(this));
    }

    //������� �������
    public void Shooted(Missile missile)
    {
        SetHealth(health - missile.Damage);
    }

    //��������������� ������ ����
    public override void Dead()
    {
        Destroy(gameObject);
    }
    //=============================================================================================
}
