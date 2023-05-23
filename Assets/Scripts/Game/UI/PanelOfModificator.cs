using System.Collections;
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
        button.onClick.AddListener(() => { ability.Use(); StartCoroutine(Wait(button)); });
    }

    private IEnumerator Wait(Button button)
    {
        button.enabled = false;
        yield return new WaitForSecondsRealtime(3);
        button.enabled = true;
    }
}
