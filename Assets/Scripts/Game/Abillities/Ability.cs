using System;
using UnityEngine;

[Serializable]
public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected new string name;
    [SerializeField] protected string description;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected int level = 1;

    public virtual void SetDefLevel()
    {
        level = 1;
    }

    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;
    public int Level => level;

    public abstract void Use();
    public abstract void AddLevel();
}
