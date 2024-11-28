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
}
