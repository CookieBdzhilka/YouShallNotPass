using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterPlayer : Character, IControllObject
{
    //=============================================================================================
    //Ссылки на другие объекты
    public Canvas PlayerHUD;


    //=============================================================================================
    //Анимация
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //Личные поля
    private Rigidbody PlayerRB;
    [SerializeField]
    private int bonusCount = 0;
    //=============================================================================================

    //=============================================================================================
    //Свойства
    public int BonusCount
    {
        get { return bonusCount; }
        set
        {
            bonusCount = value;
            OnBonusChanged?.Invoke(bonusCount);
        }
    }
    //=============================================================================================

    //=============================================================================================
    //События
    public UnityAction<int> OnBonusChanged;
    public UnityAction<UpdateFather> OnUpdateAreaEnter;
    public UnityAction OnUpdateAreaExit;
    //=============================================================================================

    //=============================================================================================
    //Методы Unity
    protected override void CharacterAwake()
    {
        base.CharacterAwake();

        PlayerRB = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();
        stateMachine.Initialize(new PlayerStateIdle(this));

        force = 3;
        maxHealth = 20;
        health = maxHealth;
        shootSpeed = 1f;
    }
    private void Start()
    {
        //Задаю длемент контролирующий передвижение игрока
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ICharacterPlayerVisitor>() != null)
        {
            IEnter(other.gameObject.GetComponent<ICharacterPlayerVisitor>());
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<ICharacterPlayerVisitor>() != null)
        {
            IExit(other.gameObject.GetComponent<ICharacterPlayerVisitor>());
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
    //Команды
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
        (stateMachine.CurrentState as PlayerState).DieCommand();
    }

    //Возможности объекта
    public void Move(Vector2 MoveVector)
    {
        stateMachine.ChangeState(new PlayerStateWalk(this));
        Vector3 NewPos = new Vector3(-MoveVector.y, 0f, MoveVector.x) * WalkSpeed;
        PlayerRB.MovePosition(transform.position + NewPos * Time.fixedDeltaTime);
        PlayerRB.MoveRotation(Quaternion.LookRotation(NewPos * Time.fixedDeltaTime));
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
    //=============================================================================================

    //=============================================================================================
    //Корутины
    public IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootSpeed);
            CharacterAngryNPC[] NPCArray = FindObjectsOfType<CharacterAngryNPC>();

            if (NPCArray.Length <= 0)
            {
                yield return null;
                continue;
            }

            CharacterAngryNPC ClosiestEnemy = NPCArray[0];
            foreach (var Enemy in NPCArray)
            {
                if (Vector3.Distance(transform.position, ClosiestEnemy.transform.position) > Vector3.Distance(transform.position, Enemy.transform.position))
                {
                    ClosiestEnemy = Enemy;
                }
            }

            Missile.CreateMe(ClosiestEnemy.gameObject, transform.position + new Vector3(0, transform.GetComponent<CapsuleCollider>().bounds.size.y, 0), force);
        }
    }
    //=============================================================================================
}
