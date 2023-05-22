using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability List", menuName = "Ability List")]
public class AbilityList : ScriptableObject
{
    public List<Ability> Abilities;
}
