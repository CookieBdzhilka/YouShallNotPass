using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaFight : Area, ICharacterPlayerVisitor
{
    List<CharacterAngryNPC> Enemies;
    public StateMachine stateMachine { get; private set; }
    GameObject Intrudor;

    public void Awake()
    {
        Enemies = new List<CharacterAngryNPC>();
        stateMachine = new StateMachine();
        stateMachine.Initialize(new AreaFightStateIdle(this));
    }
    public void Start()
    {
        StartCoroutine(nameof(CreateTimer));
    }
    IEnumerator CreateTimer()
    {
        while (true)
        {
            while (Enemies.Count < 10)
            {
                Vector3 RandomPos;
                Vector3 MyBoundSize = GetComponent<MeshRenderer>().bounds.size;
                RandomPos.x = Random.Range(-MyBoundSize.x / 3, MyBoundSize.x / 3);
                RandomPos.y = MyBoundSize.y;
                RandomPos.z = Random.Range(-MyBoundSize.z / 3, MyBoundSize.z / 3);

                CharacterAngryNPC NewNPC = CharacterAngryNPC.CreateMe(RandomPos);
                Enemies.Add(NewNPC);
                NewNPC.OnDestroyEvent += DeleteEnemy;
                (stateMachine.CurrentState as AreaFightState).CurrentTask();
                yield return new WaitForSeconds(2);
            }
            yield return null;
        }
    }
    public void AttackCommand()
    {
        if (Intrudor == null)
            return;

        foreach (var Enemy in Enemies)
        {
            Enemy.AttackTarget(Intrudor);
        }
    }    
    public void CalmCommand()
    {
        foreach (var Enemy in Enemies)
        {
            Enemy.CalmDown();
        }
    }
    public void DeleteEnemy(Character angryNPC)
    {
        Enemies.Remove(angryNPC as CharacterAngryNPC);
    }
    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        Intrudor = characterPlayer.gameObject;
        stateMachine.Initialize(new AreaFightStateWar(this));
        AttackCommand();
    }
    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
        Intrudor = null;
        stateMachine.Initialize(new AreaFightStateIdle(this));
        CalmCommand();
    }
}
