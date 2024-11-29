using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChangeWeapon : MonoBehaviour
{
    public static UI_ChangeWeapon instance;

    private void Awake()
    {
        instance = this;
    }

    public LevelUpSelectionButton[] levelUpButtons;

    public void Skip()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
