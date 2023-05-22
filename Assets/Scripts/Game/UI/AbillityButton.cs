using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbillityButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image icon;

    private Ability abillity;
    private bool isAmplifier;

    public void GetAbillity(Ability abillity, bool isAmplifier)
    {
        this.abillity = abillity;
        icon.sprite = this.abillity.Icon;
        name.text = this.abillity.Name;
        description.text = this.abillity.Description;
        this.isAmplifier = isAmplifier;
        GetComponent<Button>().onClick.AddListener(() => { LernAbillity(); });
    }

    private void LernAbillity()
    {
        PlayerAI playerAI = GameManager.instance.PlayerAI;

        abillity.SetDefLevel();

        if (isAmplifier)
        {
            if (IsHaveAbility(playerAI.Amplifiers, out int numberOfPosition))
                playerAI.UpgrageAbility(true, numberOfPosition);
            else
                playerAI.AddAbillity(abillity, true);
        }
        else
        {
            if (IsHaveAbility(playerAI.Modificators, out int numberOfPosition))
                playerAI.UpgrageAbility(false, numberOfPosition);
            else
                playerAI.AddAbillity(abillity, false);
        }

        IngameUI.instance.CloseLevelUpPanel();
    }

    private bool IsHaveAbility(List<Ability> abilList, out int numberOfPosition)
    {
        bool isHaveAbility = false;
        numberOfPosition = 0;

        for (int i = 0; i < abilList.Count; i++)
        {
            if (abillity == abilList[i])
            {
                numberOfPosition = i;
                isHaveAbility = true;
            }
        }

        return isHaveAbility;
    }
}
