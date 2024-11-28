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
    public string name; //��������
    public float speed; //�����ٶ�
    public float damage; //�˺�
    public float range; // ��Χ
    public float timeBetweenAttacks; //�������
    public float amount; //����
    public float duration; //����ʱ��

    public string upgradeText; //�������� 
 }
