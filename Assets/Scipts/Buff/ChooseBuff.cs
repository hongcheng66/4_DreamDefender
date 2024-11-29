using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuff : MonoBehaviour
{
    public static ChooseBuff instance;

    private void Awake()
    {
        instance = this;
    }

    public BuffChooseButton[] buffChooseButtons;

    public void Skip()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;

        if (EnemySpawner.instance.canGoNextWave == false)
        {
            ExperienceLevelController.instance.UpgradePanel();
            EnemySpawner.instance.canGoNextWave = true;
        }
    }
}
