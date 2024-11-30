using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("WakeStat")]
    public GameObject awakeImage;
    public GameObject unawakeImage;

    [Header("EXP Level")]
    public Slider explvlSlider;
    public TMP_Text explvlText;

    public LevelUpSelectionButton[] levelUpButtons;

    [Header("Weapon Info")]
    public UI_WeaponToolTip weaponToolTip;

    public GameObject changeWeaponPanel;
    public GameObject levelUpPanel;

    [Header("Waves Info")]

    public GameObject waveReport;
    public TMP_Text waveReportText;

    public GameObject treasureAnimation;

    [Header("Buff Info")]
    public BuffToolTip buffToolTip;
    public GameObject buffGetPanel;


    [Header("Coin")]
    public TMP_Text coinText;

    public PlayerUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public GameObject levelSuccessScreen;
    public TMP_Text endTimeText;

    public string mainMenuName;

    [Header("Pause")]
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void ChangeAwakeIcon()
    {
        if (awakeImage.activeSelf == true)
        {
            awakeImage.SetActive(false);
            unawakeImage.SetActive(true);

        }
        else if (unawakeImage.activeSelf == true)
        {
            awakeImage.SetActive(true);
            unawakeImage.SetActive(false);
        }
    }

    public void UpdateExperience(int currentExp,int levelExp,int currentlvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        explvlText.text = "Lv : " + currentlvl;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        coinText.text = ":" + CoinController.instance.currentCoins;
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        if (seconds < 10f && minutes == 0f)
            timeText.color = Color.red;
        else
            timeText.color = Color.white;

        timeText.text = minutes + ":" + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if(pauseScreen.activeSelf == true)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseScreen.SetActive(true);

            if (levelUpPanel.activeSelf == false)
            {
                Time.timeScale = 0f;
            }
        }

    }
}
