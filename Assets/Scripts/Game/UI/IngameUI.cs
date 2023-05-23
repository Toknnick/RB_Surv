using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public static IngameUI instance;
    public Joystick joystick;

    [SerializeField] private List<PanelOfAmplifier> amplifiers;
    [SerializeField] private List<PanelOfModificator> modificators;
    [Space(1)]
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI LvL;
    [Space(1)]
    [SerializeField] private RectTransform deathScreen;
    [SerializeField] private RectTransform levelUpScreen;
    [Space(1)]
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button specialButton;

    [SerializeField] private RectTransform hpBar;
    [SerializeField] private Slider LevelBar;
    [SerializeField] private Slider WaveBar;


    private EntityPlayer player;
    private PlayerAI playerAI;
    private SpawnManager spawnManager;
    private PlayerData playerData;

    public void TryToQuitFromLvl()
    {
        confirmationPanel.SetActive(true);
        exitButton.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void StayAtLvl()
    {
        confirmationPanel.SetActive(false);
        exitButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void QuitFromLvl()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void CloseLevelUpPanel()
    {
        Time.timeScale = 1;
        levelUpScreen.gameObject.SetActive(false);
    }

    private void UseSpecialAbility()
    {
        specialButton.gameObject.SetActive(false);
        playerAI.SpecialAbility.UseSpecial();
    }

    private void UpgrageAbilityOnUI(Ability ability, bool isAmplifier, int numberOfAbilityInList)
    {
        if (isAmplifier)
            amplifiers[numberOfAbilityInList].TakeAbillity(ability);
        else
            modificators[numberOfAbilityInList].TakeAbillity(ability);
    }

    private void SetAbility(Ability ability, bool isAmplifier, int numberOfAbilityInList)
    {
        if (isAmplifier)
            amplifiers[numberOfAbilityInList].TakeAbillity(ability);
        else
            modificators[numberOfAbilityInList].TakeAbillity(ability);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            instance = this;
        }
    }

    private void Start()
    {
        specialButton.gameObject.SetActive(false);
        confirmationPanel.SetActive(false);
        exitButton.gameObject.SetActive(true);

        playerData = GameManager.instance.PlayerData;
        player = GameManager.instance.Player;
        playerAI = GameManager.instance.PlayerAI;
        spawnManager = GameManager.instance.SpawnManager;

        playerData.OnSpecialChargeIsOn += () =>
        {
            specialButton.gameObject.SetActive(true);
        };
        playerAI.OnLearnedAbility += SetAbility;
        playerAI.OnUpgradedAbility += UpgrageAbilityOnUI;

        playerData.OnGoldChanged += () => { gold.text = playerData.Gold.ToString(); };
        playerData.OnLevelChanged += () => { LvL.text = playerData.Level.ToString(); LevelBar.value = 0; };
        spawnManager.OnWaveChanged += () => { wave.text = (spawnManager.WaveIndex + 1).ToString(); WaveBar.value = 0; };

        spawnManager.OnAllWave += ShowGameOverPanel;
        playerData.OnXPChanged += ProgressLevelBar;
        player.OnDie += ShowDeathScreen;
        spawnManager.OnChangedWaveStatus += ProgressWaveBar;
        specialButton.onClick.AddListener(() => { UseSpecialAbility(); });

        player.OnHealthChanged += () =>
        {
            Vector3 a = hpBar.localScale;
            a.x = player.CurrentHealth / player.MaxHealth;
            hpBar.localScale = a;
        };
        playerData.OnLevelChanged += () =>
        {
            levelUpScreen.gameObject.SetActive(true);
        };
    }

    private void OnDestroy()
    {
        playerAI.OnLearnedAbility -= SetAbility;
        playerAI.OnUpgradedAbility -= UpgrageAbilityOnUI;

        playerData.OnGoldChanged -= () => { gold.text = playerData.Gold.ToString(); };
        playerData.OnLevelChanged -= () => { LvL.text = playerData.Level.ToString(); };
        spawnManager.OnWaveChanged -= () => { wave.text = (spawnManager.WaveIndex + 1).ToString(); };
        playerData.OnSpecialChargeIsOn -= () =>
        {
            specialButton.gameObject.SetActive(false);
        };

        spawnManager.OnAllWave -= ShowGameOverPanel;
        playerData.OnXPChanged -= ProgressLevelBar;
        player.OnDie -= ShowDeathScreen;
        spawnManager.OnChangedWaveStatus -= ProgressWaveBar;

        player.OnHealthChanged -= () =>
        {
            Vector3 a = hpBar.localScale;
            a.x = player.CurrentHealth / player.MaxHealth;
            hpBar.localScale = a;
        };
        playerData.OnLevelChanged -= () =>
        {
            levelUpScreen.gameObject.SetActive(true);
        };
    }

    private void ProgressWaveBar(float maxEnemies, float nowKilled)
    {
        if (nowKilled / maxEnemies == 0)
            WaveBar.gameObject.SetActive(false);
        else
            WaveBar.gameObject.SetActive(true);

        WaveBar.value = nowKilled / maxEnemies;
    }

    private void ProgressLevelBar(float maxXPForLevel, float nowXP)
    {
        if (nowXP / maxXPForLevel == 0)
            LevelBar.gameObject.SetActive(false);
        else
            LevelBar.gameObject.SetActive(true);

        LevelBar.value = nowXP / maxXPForLevel;
    }

    private void ShowDeathScreen()
    {
        //User.user.Save();
        deathScreen.gameObject.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
