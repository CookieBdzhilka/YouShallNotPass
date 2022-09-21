using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character, IControllObject
{
    public PlayerPrefab playerPrefab;
    public StateMachine stateMachine { get; private set; }

    private Rigidbody PlayerRB;

    private void Awake()
    {
        PlayerRB = playerPrefab.GetComponent<Rigidbody>();
        stateMachine = new StateMachine();
        stateMachine.Initialize(new PlayerStateAlive(this));
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
    private void IEnter(ICharacterPlayerVisitor other)
    {
        (stateMachine.CurrentState as PlayerState).IEnter(other);
    }
    private void IExit(ICharacterPlayerVisitor other)
    {
        (stateMachine.CurrentState as PlayerState).IExit(other);
    }
    public void MoveObject(Vector2 MoveVector)
    {
        Vector3 NewPos = new Vector3(MoveVector.x, 0f, MoveVector.y) * WalkSpeed;
        NewPos = Vector3.ClampMagnitude(NewPos, WalkSpeed);
        PlayerRB.MoveRotation(Quaternion.LookRotation(NewPos));

        NewPos.x = transform.position.x - MoveVector.y;
        NewPos.z = transform.position.z + MoveVector.x;
        NewPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, NewPos, WalkSpeed * MoveVector.magnitude * Time.deltaTime);
    }
}
