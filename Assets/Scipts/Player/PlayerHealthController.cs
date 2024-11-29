using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float currentHealth;
    public float maxHealth = 100f;
    public float healthrate; //回血速率

    private float origin_reviveTime = 3f;
    public float reviveTime = 3f; //复活时间
    private float restReviveTime;
    public TMP_Text restTimeText;

    public Slider healthSlider;

    public GameObject deathEffect;

    private GameObject target;
    private PlayerController player;

    public bool isMedical = false;

    public int deadCount = 0; //死亡次数计数

    public bool isBurningBlood = false; //燃血急行buff

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        reviveTime = origin_reviveTime;

        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;
        player = target.GetComponent<PlayerController>();

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

    }

    void Update()
    {
        currentHealth +=  healthrate * Time.deltaTime; //每秒回血速率

        if (isBurningBlood)
            currentHealth -= maxHealth * 0.05f * Time.deltaTime;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;


        healthSlider.value = currentHealth;

    }

    public void ChangeHealthRate(float changeRate) //用于外部函数调整回血速率
    {
        healthrate += changeRate;
    }

    public void ChangeMaxHealth(float changeHealth) //用于外部函数调整血量最大值
    {
        maxHealth += changeHealth;
        currentHealth += changeHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void AddHealth(float amount) //用于外部函数调用恢复生命值
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        if(player.isDead == false)
        {
            currentHealth -= damageToTake;

            if(currentHealth <= 0 )
            {
                if (isMedical == false)
                    reviveTime += deadCount * 2f;
                else
                    reviveTime = origin_reviveTime;

                deadCount++;
                StartCoroutine(Revive());
            }

            healthSlider.value = currentHealth;
        }
    }

    private IEnumerator Revive()
    {
        player.isDead = true;
        player.transform.position = CoreController.instance.transform.position;

        restReviveTime = reviveTime;
        restTimeText.text = restReviveTime.ToString();
        restTimeText.gameObject.SetActive(true);

        while (restReviveTime > 0)
        {
            restTimeText.text = restReviveTime.ToString();
            restReviveTime--;
            yield return new WaitForSeconds(1f);
        }
        restTimeText.gameObject.SetActive(false);
        player.isDead = false;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;

        yield break;
    }
}
