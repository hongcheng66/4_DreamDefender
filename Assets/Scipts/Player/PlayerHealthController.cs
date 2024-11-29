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
    public float healthrate; //��Ѫ����

    private float origin_reviveTime = 3f;
    public float reviveTime = 3f; //����ʱ��
    private float restReviveTime;
    public TMP_Text restTimeText;

    public Slider healthSlider;

    public GameObject deathEffect;

    private GameObject target;
    private PlayerController player;

    public bool isMedical = false;

    public int deadCount = 0; //������������

    public bool isBurningBlood = false; //ȼѪ����buff

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
        currentHealth +=  healthrate * Time.deltaTime; //ÿ���Ѫ����

        if (isBurningBlood)
            currentHealth -= maxHealth * 0.05f * Time.deltaTime;

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
