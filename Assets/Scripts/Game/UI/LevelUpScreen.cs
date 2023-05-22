using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUpScreen : MonoBehaviour
{
    [SerializeField] private AbilityList modificators;
    [SerializeField] private AbilityList amplifiers;
    [SerializeField] private List<AbillityButton> abillityButtons;
    [Space(5)]
    [SerializeField] private Button regenerateButton;
    [SerializeField] private List<Image> weaponsImages;
    [SerializeField] private List<Image> modificatorsImages;
    [SerializeField] private List<PanelOfAmplifier> amplifiersPanels;
    [Space(5)]
    [SerializeField] private int maxCountOfModificators = 6;
    [SerializeField] private int maxCountOfAmplifiers = 5;

    private PlayerAI playerAI;

    private List<Ability> modificatorsList;
    private List<Ability> amplifiersList;

    private int countOfLearnedAmplifiers = 0;
    private int countOfLearnedModificators = 0;

    private List<Ability> learnedModificatorsList = new();
    private List<Ability> learnedAmplifiersList = new();


    private void Start()
    {
        for (int i = 0; i < modificatorsList.Count; i++)
            modificatorsList[i].GetComponent<Modificator>().SetDefault();

        for (int i = 0; i < amplifiersList.Count; i++)
            modificatorsList[i].GetComponent<Ability>().SetDefLevel();

        GameManager.instance.PlayerAI.OnLearnedAbility += AddLearnedAbility;
        regenerateButton.onClick.AddListener(() => { GenerateAbilities(); });
    }

    private void OnDestroy()
    {
        GameManager.instance.PlayerAI.OnLearnedAbility -= AddLearnedAbility;
    }

    private void OnEnable()
    {
        modificatorsList = modificators.Abilities;
        amplifiersList = amplifiers.Abilities;

        playerAI = GameManager.instance.PlayerAI;
        int numberOfModificator = 0;
        Time.timeScale = 0;

        if (playerAI.Modificators.Count != 0)
            foreach (var ability in playerAI.Modificators)
            {
                modificatorsImages[numberOfModificator].sprite = ability.Icon;
                numberOfModificator++;
            }

        GenerateAbilities();
    }

    private void AddLearnedAbility(Ability ability, bool isAmplifier, int numberInList)
    {
        if (isAmplifier)
        {
            if (countOfLearnedAmplifiers < maxCountOfAmplifiers)
            {
                learnedAmplifiersList.Add(ability);
                countOfLearnedAmplifiers++;
            }
            else
            {
                Debug.LogError(" ол-во изученных способностей на пределе!");
            }
        }
        else
        {
            if (countOfLearnedModificators < maxCountOfModificators)
            {
                learnedModificatorsList.Add(ability);
                countOfLearnedModificators++;
            }
            else
            {
                Debug.LogError(" ол-во изученных способностей на пределе!");
            }
        }
    }

    private void GenerateAbilities()
    {
        if (countOfLearnedAmplifiers < maxCountOfAmplifiers)
        {
            abillityButtons[2].GetAbillity(amplifiersList[Random.Range(0, amplifiersList.Count)], true);
            abillityButtons[3].GetAbillity(amplifiersList[Random.Range(0, amplifiersList.Count)], true);
        }
        else
        {
            abillityButtons[2].GetAbillity(learnedAmplifiersList[Random.Range(0, learnedAmplifiersList.Count)], true);
            abillityButtons[3].GetAbillity(learnedAmplifiersList[Random.Range(0, learnedAmplifiersList.Count)], true);
        }

        if (countOfLearnedModificators < maxCountOfModificators)
        {
            abillityButtons[0].GetAbillity(modificatorsList[Random.Range(0, modificatorsList.Count)], false);
            abillityButtons[1].GetAbillity(modificatorsList[Random.Range(0, modificatorsList.Count)], false);
        }
        else
        {
            abillityButtons[0].GetAbillity(learnedModificatorsList[Random.Range(0, learnedModificatorsList.Count)], false);
            abillityButtons[1].GetAbillity(learnedModificatorsList[Random.Range(0, learnedModificatorsList.Count)], false);
        }
    }
}
