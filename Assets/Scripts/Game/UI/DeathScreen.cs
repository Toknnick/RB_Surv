using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [Header("Стоймость воскрешения")]
    [SerializeField] private int ResurrecrtCost;
    [SerializeField] private int Multiply = 3;
    [SerializeField] private TextMeshProUGUI TotalSteelPlates;
    [SerializeField] private TextMeshProUGUI TotalGems;
    [SerializeField] private TextMeshProUGUI TotalGold;
    [SerializeField] private Button DeathButton;
    [SerializeField] private Button MultiplyButton;
    [SerializeField] private TextMeshProUGUI ResurrectionScreenTimeLeft;
    [SerializeField] private RectTransform ResurrectScreen;
    [SerializeField] private Button ResurrectionButton;
    [SerializeField] private TextMeshProUGUI ResurrectionCostText;
    [SerializeField] private int RessurectionTime;
    bool isRevived = false;
    private void Start()
    {
        PlayerData data = GameManager.instance.PlayerData;
        GameManager.instance.Player.OnDie += () =>
        {
            TotalSteelPlates.text = "Пластины: " + data.SteelPlatesObtained.ToString();
            TotalGems.text = "Алмазы: " + data.Gems.ToString();
            TotalGold.text = "Золото: " + data.Gold.ToString();
        };
        DeathButton.onClick.AddListener(() => { Loose(); });
        ResurrectionButton.onClick.AddListener(() => { TryResurrect(); });
        MultiplyButton.onClick.AddListener(() => { data.SteelPlatesObtained *= Multiply; data.Gems *= Multiply; data.Gold *= Multiply; Loose(); });
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        PlayerData data = GameManager.instance.PlayerData;

        if (!isRevived)
            ResurrectionReward();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Loose()
    {
        PlayerData data = GameManager.instance.PlayerData;
        //User.user.Cicuits += data.SteelPlatesObtained;
        //User.user.Gold += data.Gold;
        //User.user.Gems += data.Gems;
        SceneManager.LoadScene(0);
    }

    private void TryResurrect()
    {
        ResurrectionCostText.text = ResurrecrtCost.ToString();
        PlayerData data = GameManager.instance.PlayerData;

        //if (User.user.Gold >= ResurrecrtCost)
        //{
        //    User.user.Gold -= ResurrecrtCost;
        //Resurrect();
        //}
        if (data.Gold >= ResurrecrtCost)
        {
            data.Gold -= ResurrecrtCost;
            Resurrect();
        }
        else
        {
            ResurrectionButton.enabled = false;
        }
    }

    private void Resurrect()
    {
        Time.timeScale = 1;
        Entity player = GameManager.instance.PlayerTransform.GetComponent<Entity>();
        player.CurrentHealth = player.MaxHealth;
        isRevived = true;
        ResurrectScreen.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void ResurrectionReward()
    {
        StartCoroutine(RessurectionCountdown());
    }

    private IEnumerator RessurectionCountdown()
    {
        ResurrectScreen.gameObject.SetActive(true);
        int t = 0;
        while (t <= RessurectionTime)
        {
            ResurrectionScreenTimeLeft.text = (RessurectionTime - t).ToString();
            t++;
            yield return new WaitForSecondsRealtime(1);
        }
        ResurrectScreen.gameObject.SetActive(false);
    }
}
