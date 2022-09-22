using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character, IControllObject
{
    //=============================================================================================
    //Ссылки на другие объекты
    public PlayerPrefab playerPrefab;
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
    private void Awake()
    {
        PlayerRB = playerPrefab.GetComponent<Rigidbody>();

        stateMachine = new StateMachine();
        stateMachine.Initialize(new PlayerStateAlive(this));

        StartPos = transform.position;
    }
    private void Start()
    {
        FindObjectOfType<MoveController>().controllObject = this;
    }
    private void OnEnable()
    {
        playerPrefab.OnTriggerEnterEvent += IEnter;
        playerPrefab.OnTriggerExitEvent += IExit;
    }
    private void OnDisable()
    {
        playerPrefab.OnTriggerEnterEvent -= IEnter;
        playerPrefab.OnTriggerExitEvent -= IExit;
    }
    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
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

    //Возможности объекта
    public void Move(Vector2 MoveVector)
    {
        Vector3 NewPos = new Vector3(MoveVector.x, 0f, MoveVector.y) * WalkSpeed;
        NewPos = Vector3.ClampMagnitude(NewPos, WalkSpeed);
        PlayerRB.MoveRotation(Quaternion.LookRotation(NewPos));

        NewPos.x = transform.position.x - MoveVector.y;
        NewPos.z = transform.position.z + MoveVector.x;
        NewPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, NewPos, WalkSpeed * MoveVector.magnitude * Time.deltaTime);
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
    //Корутины
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
