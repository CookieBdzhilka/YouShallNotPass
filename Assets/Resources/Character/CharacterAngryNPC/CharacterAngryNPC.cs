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
    //��������
    Animator animator;
    //=============================================================================================

    //=============================================================================================
    //����������� ������
    public static CharacterAngryNPC CreateMe(Vector3 StartPos = new Vector3())
    {
        CharacterAngryNPC NewObject = Resources.Load<CharacterAngryNPC>("Character/CharacterAngryNPC/objCharacterAngryNPC");
        NewObject.transform.position = StartPos;
        return Instantiate(NewObject);
    }
    //=============================================================================================

    //=============================================================================================
    //������ Unity
    private void Awake()
    {
        animator = GetComponent<Animator>();

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

    //������������ �������
    public void PlayAnimation(string animName)
    {
        animator.Play(animName);
    }

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

        Vector3 RotationVector = new Vector3(Target.transform.position.x, 0f, Target.transform.position.z);
        RotationVector = Vector3.ClampMagnitude(RotationVector, WalkSpeed);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(RotationVector));

        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, WalkSpeed * Time.deltaTime);
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
