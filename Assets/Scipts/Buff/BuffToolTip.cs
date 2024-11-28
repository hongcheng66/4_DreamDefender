using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffToolTip : UI_Tooltip
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private float defaultNameFontSize;

    public void ShowToolTip(Buff buff)
    {
        if (buff != null)
        {
            weaponName.text = buff.buffstats.name + " Lv:"  + buff.buffstats.level;
            weaponText.text = "ΩÈ…‹:" + buff.buffstats.describeText;

        }

        AdjustPosition();
        AdjustFontSize(weaponName);

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        weaponName.fontSize = defaultNameFontSize;
        gameObject.SetActive(false);
    }
}
