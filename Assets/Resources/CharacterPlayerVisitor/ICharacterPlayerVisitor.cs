using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterPlayerVisitor
{
    void CharacterPlaerEnter(CharacterPlayer characterPlayer);
    void CharacterPlayerExit(CharacterPlayer characterPlayer);
}
