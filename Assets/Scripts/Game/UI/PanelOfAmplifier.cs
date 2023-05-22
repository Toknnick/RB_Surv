using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelOfAmplifier : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI Level;

    public void TakeAbillity(Ability ability)
    {
        Icon.sprite = ability.Icon;
        Level.text = ability.Level.ToString();
    }
}
