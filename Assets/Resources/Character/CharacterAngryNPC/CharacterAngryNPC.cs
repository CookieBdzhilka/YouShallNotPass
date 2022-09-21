using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAngryNPC : Character
{
    GameObject Target;
    public StateMachine stateMachine { get; private set; }

    public static CharacterAngryNPC CreateMe(Vector3 StartPos = new Vector3())
    {
        CharacterAngryNPC NewObject = Resources.Load<CharacterAngryNPC>("Character/CharacterAngryNPC/objCharacterAngryNPC");
        NewObject.transform.position = StartPos;
        return Instantiate(NewObject);
    }

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Initialize(new AngryNPCStateIdle(this));
    }
    public void AttackTarget(GameObject target)
    {
        Target = target;
        stateMachine.ChangeState(new AngryNPCStateAttack(this));
    }
    public void CalmDown()
    {
        Target = null;
        stateMachine.ChangeState(new AngryNPCStateIdle(this));
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
    public void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
