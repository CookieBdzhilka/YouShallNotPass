using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaMelee : Area, ICharacterPlayerVisitor
{
    public UnityAction<Character> AtRange;
    public UnityAction<Character> OutOfRange;

    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        AtRange?.Invoke(characterPlayer);
    }

    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
        OutOfRange?.Invoke(characterPlayer);
    }
}
