using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAlive : PlayerState
{
    public PlayerStateAlive(CharacterPlayer Object) : base(Object)
    {
    }

    public override void Enter()
    {
        CharacterPlayer characterPlayer = (ContextObject as CharacterPlayer);
        characterPlayer.StartCoroutine(nameof(characterPlayer.Shoot));
    }

    public override void IEnter(ICharacterPlayerVisitor playerVisitor)
    {
        playerVisitor.CharacterPlaerEnter(ContextObject as CharacterPlayer);
    }

    public override void IExit(ICharacterPlayerVisitor playerVisitor)
    {
        playerVisitor.CharacterPlayerExit(ContextObject as CharacterPlayer);
    }

}
