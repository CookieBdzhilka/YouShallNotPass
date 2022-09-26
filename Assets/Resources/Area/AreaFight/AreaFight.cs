using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaFight : Area, ICharacterPlayerVisitor
{
    //=============================================================================================
    List<CharacterAngryNPC> Enemies;
    public Character Intrudor { get; private set; } // Объект, который вошел в поле
    //=============================================================================================

    //=============================================================================================
    //Машина состояний
    public StateMachine stateMachine { get; private set; }
    //=============================================================================================

    //=============================================================================================
    //События
    public UnityAction<Character> CommandAttack;
    public UnityAction CommandCalm;
    //=============================================================================================

    //=============================================================================================
    //Методы Unity
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
    //=============================================================================================

    //=============================================================================================
    //Методы объекта
    public void DeleteEnemy(Character angryNPC)
    {
        CharacterAngryNPC Enemy = angryNPC as CharacterAngryNPC;
        CommandAttack -= Enemy.CommandToFollow;
        CommandCalm -= Enemy.CalmDown;
        Enemies.Remove(Enemy);
    }
    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        Intrudor = characterPlayer;
        stateMachine.Initialize(new AreaFightStateWar(this));
        characterPlayer.StartShooting();
        CommandAttack?.Invoke(Intrudor);
    }
    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
        Intrudor = null;
        stateMachine.Initialize(new AreaFightStateIdle(this));
        characterPlayer.StopShooting();
        CommandCalm?.Invoke();
    }
    //=============================================================================================

    //=============================================================================================
    //Корутины
    IEnumerator CreateTimer()
    {
        while (true)
        {
            while (Enemies.Count < 5)
            {
                Vector3 RandomPos;
                Vector3 MyBoundSize = GetComponent<MeshRenderer>().bounds.size;
                RandomPos.x = Random.Range(-MyBoundSize.x / 3, MyBoundSize.x / 3);
                RandomPos.y = MyBoundSize.y;
                RandomPos.z = Random.Range(-MyBoundSize.z / 3, MyBoundSize.z / 3);

                CharacterAngryNPC NewNPC = CharacterAngryNPC.CreateMe(1, RandomPos);
                Enemies.Add(NewNPC);
                NewNPC.OnDestroyEvent += DeleteEnemy;
                CommandAttack += NewNPC.CommandToFollow;
                CommandCalm += NewNPC.CalmDown;

                (stateMachine.CurrentState as AreaFightState).CurrentTask();
                yield return new WaitForSeconds(2);
            }
            yield return null;
        }
    }
    //=============================================================================================
}
