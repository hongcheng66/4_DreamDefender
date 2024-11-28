using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    public Sprite icon;

    [HideInInspector]
    public bool statsUpdated;

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;

            statsUpdated = true;

            if(weaponLevel >= stats.Count - 1)
            {
                PlayerController.instance.fullyLeveledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public string name; //武器名字
    public float speed; //攻击速度
    public float damage; //伤害
    public float range; // 范围
    public float timeBetweenAttacks; //攻击间隔
    public float amount; //数量
    public float duration; //持续时间

    public string upgradeText; //升级描述 
 }
