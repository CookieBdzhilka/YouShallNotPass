using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character, IControllObject
{
    //=============================================================================================
    //������ �� ������ �������
    public Canvas PlayerHUD;
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //������ ����
    private Rigidbody PlayerRB;
    private Vector3 StartPos;
    //=============================================================================================

    //=============================================================================================
    //������ Unity
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();
        stateMachine.Initialize(new PlayerStateAlive(this));

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
    //������ �������
    //������ � ���-�� ����������
    private void IEnter(ICharacterPlayerVisitor other)
    {
        (stateMachine.CurrentState as PlayerState).IEnter(other);
    }
    private void IExit(ICharacterPlayerVisitor other)
    {
        (stateMachine.CurrentState as PlayerState).IExit(other);
    }
    //������� ���������
    public void MoveObject(Vector2 MoveVector)
    {
        (stateMachine.CurrentState as PlayerState).MoveObjectCommand(MoveVector);
    }

    //����������� �������
    public void Move(Vector2 MoveVector)
    {
        Vector3 NewPos = new Vector3(-MoveVector.y, 0f, MoveVector.x) * WalkSpeed; 
        PlayerRB.velocity = NewPos;
        PlayerRB.MoveRotation(Quaternion.LookRotation(NewPos));
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
    public override void Dead()
    {
        (stateMachine.CurrentState as PlayerState).Die();
    }
    public void Ressurect()
    {
        transform.position = StartPos;
        health = 10;
        stateMachine.ChangeState(new PlayerStateAlive(this));
    }
    //=============================================================================================

    //=============================================================================================
    //��������
    public IEnumerator Shoot()
    {
        while(true)
        {
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

            Missile.CreateMe(ClosiestEnemy.gameObject, transform.position);
            yield return new WaitForSeconds(1);
        }
    }
    //=============================================================================================
}
