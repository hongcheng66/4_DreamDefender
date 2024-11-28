using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_WeaponToolTip : UI_Tooltip
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponSpeed;
    [SerializeField] private TextMeshProUGUI weaponDamage;
    [SerializeField] private TextMeshProUGUI weaponRange;
    [SerializeField] private TextMeshProUGUI weaponAttackTime;
    [SerializeField] private TextMeshProUGUI weaponAmount;
    [SerializeField] private TextMeshProUGUI weaponDuration;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private float defaultNameFontSize;

    public void ShowToolTip(Weapon weapon)
    {
        if(weapon.weaponLevel == -1)
        {
            weaponName.text = weapon.stats[0].name;
            weaponSpeed.text = "�����ٶ�:" + weapon.stats[0].speed ;
            weaponDamage.text = "�˺�:" + weapon.stats[0].damage;
            weaponRange.text = "��Χ:" + weapon.stats[0].range;
            weaponAttackTime.text = "�������:" + weapon.stats[0].timeBetweenAttacks;
            weaponAmount.text = "����:" + weapon.stats[0].amount;
            weaponDuration.text = "����ʱ��:" + weapon.stats[0].duration;
            weaponText.text = "����:" + weapon.stats[0].upgradeText;
            
        }
        else
        {
            weaponName.text = weapon.stats[weapon.weaponLevel].name;
            weaponSpeed.text = "�����ٶ�:" + weapon.stats[weapon.weaponLevel].speed + "->" + weapon.stats[weapon.weaponLevel + 1].speed;
            weaponDamage.text = "�˺�:" + weapon.stats[weapon.weaponLevel].damage + "->" + weapon.stats[weapon.weaponLevel + 1].damage;
            weaponRange.text = "��Χ:" + weapon.stats[weapon.weaponLevel].range + "->" + weapon.stats[weapon.weaponLevel + 1].range;
            weaponAttackTime.text = "�������:" + weapon.stats[weapon.weaponLevel].timeBetweenAttacks + "->" + weapon.stats[weapon.weaponLevel + 1].timeBetweenAttacks;
            weaponAmount.text = "����:" + weapon.stats[weapon.weaponLevel].amount + "->" + weapon.stats[weapon.weaponLevel + 1].amount;
            weaponDuration.text = "����ʱ��:" + weapon.stats[weapon.weaponLevel].duration + "->" + weapon.stats[weapon.weaponLevel + 1].duration;
            weaponText.text = "����:" + weapon.stats[weapon.weaponLevel + 1].upgradeText;
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
