using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelOfModificator : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI Level;

    public void TakeAbillity(Ability ability)
    {
        icon.sprite = ability.Icon;
        Level.text = ability.Level.ToString();
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => { ability.Use(); });
    }
}
