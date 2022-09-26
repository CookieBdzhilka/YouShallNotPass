using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character, IControllObject
{
    //=============================================================================================
    //Ссылки на другие объекты
    public Canvas PlayerHUD;
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //Личные поля
    private Rigidbody PlayerRB;
    private Vector3 StartPos;
    //=============================================================================================

    //=============================================================================================
    //Методы Unity
    protected override void CharacterAwake()
    {
        base.CharacterAwake();

        PlayerRB = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();
        stateMachine.Initialize(new PlayerStateIdle(this));

        StartPos = transform.position;
    }
    private void Start()
    {
        FindObjectOfType<MoveController>().controllObject = this;
    }
    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICharacterPlayerVisitor>() != null)
        {
            IEnter(other.GetComponent<ICharacterPlayerVisitor>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ICharacterPlayerVisitor>() != null)
        {
            IExit(other.GetComponent<ICharacterPlayerVisitor>());
        }
    }
    //=============================================================================================

    //=============================================================================================
    //Методы объекта
    //Префаб с чем-то столкнулся
    private void IEnter(ICharacterPlayerVisitor other)
    {
        (stateMachine.CurrentState as PlayerState).IEnter(other);
    }
    private void IExit(ICharacterPlayerVisitor other)
    {
        (stateMachine.CurrentState as PlayerState).IExit(other);
    }
    //Команда двигаться
    public void MoveObject(Vector2 MoveVector)
    {
        (stateMachine.CurrentState as PlayerState).MoveObjectCommand(MoveVector);
    }
    public void StopObject()
    {
        (stateMachine.CurrentState as PlayerState).StopCommand();
    }
    public override void Dead()
    {
        (stateMachine.CurrentState as PlayerState).Die();
    }

    //Возможности объекта
    public void Move(Vector2 MoveVector)
    {
        stateMachine.ChangeState(new PlayerStateWalk(this));
        Vector3 NewPos = new Vector3(-MoveVector.y, 0f, MoveVector.x) * WalkSpeed; 
        PlayerRB.velocity = NewPos;
        PlayerRB.MoveRotation(Quaternion.LookRotation(NewPos));
    }
    public void Stop()
    {
        stateMachine.ChangeState(new PlayerStateIdle(this));
    }
    public void StartShooting()
    {
        StopCoroutine(nameof(Shoot));
        StartCoroutine(nameof(Shoot));
    }
    public void StopShooting()
    {
        StopCoroutine(nameof(Shoot));
    }
    public void Ressurect()
    {
        transform.position = StartPos;
        health = 10;
        stateMachine.ChangeState(new PlayerStateIdle(this));
    }

    //=============================================================================================

    //=============================================================================================
    //Корутины
    public IEnumerator Shoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            CharacterAngryNPC[] NPCArray = FindObjectsOfType<CharacterAngryNPC>();

            if (NPCArray.Length <= 0)
            {
                yield return null;
                continue;
            }

            CharacterAngryNPC ClosiestEnemy = NPCArray[0];
            foreach (var Enemy in NPCArray)
            {
                if(Vector3.Distance(transform.position, ClosiestEnemy.transform.position) > Vector3.Distance(transform.position, Enemy.transform.position))
                {
                    ClosiestEnemy = Enemy;
                }
            }

            Missile.CreateMe(ClosiestEnemy.gameObject, transform.position + new Vector3(0, transform.GetComponent<CapsuleCollider>().bounds.size.y, 0), 2);
        }
    }
    //=============================================================================================
}
