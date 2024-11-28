using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanController: MonoBehaviour
{
    private float currentSan;
    private float maxSan;

    public Slider SanSlider;
    public GameObject deathEffect;

    private void Start()
    {
        currentSan = CoreSanController.instance.currentSan;
        maxSan = CoreSanController.instance.maxSan;

        SanSlider.maxValue = maxSan;
        SanSlider.value = currentSan;
    }

    void Update()
    {
        currentSan = CoreSanController.instance.currentSan;
        maxSan = CoreSanController.instance.maxSan;

        SanSlider.value = currentSan;

        if (currentSan <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            PlayerController.instance.ChangeAwakeStat();
        }
    }
}
