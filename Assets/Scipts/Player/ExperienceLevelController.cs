using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public int addition = 0;

    public ExpPickup pickup;

    public List<int> expLevels;
    public int currentLevel = 1,levelCount = 100;

    public List<Weapon> weaponToUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        while(expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet + addition;

        if(currentExperience >= expLevels[currentLevel])
        {
            currentExperience -= expLevels[currentLevel];

            currentLevel++;

            if (currentLevel >= expLevels.Count)
            {
                currentLevel = expLevels.Count - 1;
            }

            UpgradePanel();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);

        SFXManager.instance.PlaySFXPitched(2); //ʰȡ������Ч
    }

    public void SpawnExp(Vector3 position,int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    public void UpgradePanel()
    {
        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        weaponToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons); //���Խ��������������ʹ�õ�����

        if(availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count); //�ӿ�ʹ�õ������������ѡһ�Ѽ�������������
            weaponToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }
            
        if(PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLeveledWeapons.Count < 4) //������е�������δ�ﵽչʾ��λ����
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);   //�ڿ�ʹ�õ������м��뻹δ����������
        }

        for(int i = weaponToUpgrade.Count;i < 4;i++)//��δ���������������б�
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count); //�ٴӿ�ѡ��������м��� 3-�Խ���������
                weaponToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }




        for (int i = 0; i < weaponToUpgrade.Count;i++)
        {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponToUpgrade[i]);
        }

        for(int i = 0;i < UIController.instance.levelUpButtons.Length;i++)
        {
            if(i < weaponToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }
   


        PlayerStatController.instance.UpdateDisplay();
    }
}
