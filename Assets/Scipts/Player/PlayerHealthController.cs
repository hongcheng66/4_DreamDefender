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
    public float maxHealth = 100;
    public float healthrate; //��Ѫ����

    public float reviveTime = 3f;
    public float restReviveTime;
    public TMP_Text restTimeText;

    public Slider healthSlider;

    public GameObject deathEffect;

    private GameObject target;
    private PlayerController player;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

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
        currentHealth +=  healthrate * Time.deltaTime; //ÿ���Ѫ����
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;


        healthSlider.value = currentHealth;


    }

    public void ChangeHealthRate(float changeRate) //�����ⲿ����������Ѫ����
    {
        healthrate += changeRate;
    }

    public void ChangeMaxHealth(float changeHealth) //�����ⲿ��������Ѫ�����ֵ
    {
        maxHealth += changeHealth;
        currentHealth += changeHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void AddHealth(float amount) //�����ⲿ�������ûָ�����ֵ
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
