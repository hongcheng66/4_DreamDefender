using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if(PlayerController.instance.isDead == false)
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
        PlayerController.instance.isDead = true;
        PlayerController.instance.transform.position = CoreController.instance.transform.position;

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
        PlayerController.instance.isDead = false;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;

        yield break;
    }
}
