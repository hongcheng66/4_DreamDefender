using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreSanController : MonoBehaviour
{
    public static CoreSanController instance;

    public float currentSan;
    public float maxSan = 100f;

    public float additionRecover = 0f;

    private bool isRecover = false;
    private bool isDecrease = false;

    public Slider SanSlider;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        currentSan = maxSan;

        SanSlider.maxValue = maxSan;
        SanSlider.value = currentSan;

    }

    // Update is called once per frame
    void Update()
    {
        SanSlider.value = currentSan;

        if (currentSan <= 0)
        {

            Instantiate(deathEffect, transform.position, transform.rotation);

            //SFXManager.instance.PlaySFX(3);//玩家死亡音效

            LevelManager.instance.EndLevel();
        }


        /*
        if (PlayerController.instance.awakeStat == true && !isRecover)
        {
            StartCoroutine(RecoverSan());
        }

        if (PlayerController.instance.awakeStat == false && !isDecrease)
        {
            StartCoroutine(LoseSan());
        }
        */

    }

    public void AddSan(float amount) //用于外部天赋恢复san值
    {
        currentSan += amount;

        if (currentSan > maxSan)
            currentSan = maxSan;

        SanSlider.value = currentSan;
    }

    private IEnumerator RecoverSan()
    {
        isRecover = true;

        while (currentSan < maxSan)
        {
            currentSan++;
            currentSan += additionRecover;

            yield return new WaitForSeconds(1f);
        }

        isRecover = false;

        yield return null;
    }

    private IEnumerator LoseSan()
    {
        isDecrease = true;
        while (currentSan > 0 && PlayerController.instance.awakeStat == false)
        {
            currentSan = currentSan - maxSan * 0.05f;

            yield return new WaitForSeconds(1f);
        }

        isDecrease = false;

        yield return null;
    }
}