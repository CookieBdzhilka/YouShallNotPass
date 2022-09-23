using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaMelee : Area, ICharacterPlayerVisitor
{
    public bool Sleep;
    public UnityAction<Character> AtRange;
    public UnityAction<Character> OutOfRange;

    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        if (Sleep)
            return;

        AtRange?.Invoke(characterPlayer);
    }

    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
        if (Sleep)
            return;

        OutOfRange?.Invoke(characterPlayer);
    }
}
