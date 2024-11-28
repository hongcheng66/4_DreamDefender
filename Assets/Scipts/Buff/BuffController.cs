using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public static BuffController instance;

    public List<Buff> buffBechosen;

    private void Awake()
    {
        instance = this;
    }

    public void UpgradeBuffPanel()
    {
        UIController.instance.buffGetPanel.SetActive(true);

        Time.timeScale = 0f;

        buffBechosen.Clear();

        List<Buff> availableBuff = new List<Buff>();

        for (int i = 0; i < PlayerController.instance.unassignedbuffs.Count; i++) //����������ȼ�������buff����ѡ�����
        {
            if(EnemySpawner.instance.currentWave + 1 >= PlayerController.instance.unassignedbuffs[i].buffstats.level) //�����ǰ�Ļغ�������buff�ĵȼ� ��ô�Ϳ��Ի�����buff
            {
                availableBuff.Add(PlayerController.instance.unassignedbuffs[i]);
            }
        }

        for (int i = 0;i < 4;i++) //4�ǲ�λ������
        { 
            int selected = Random.Range(0,availableBuff.Count);
            buffBechosen.Add(availableBuff[selected]);
            availableBuff.RemoveAt(selected);
        }

        for (int i = 0; i < buffBechosen.Count; i++) //�������buff���밴ť��
        {
            ChooseBuff.instance.buffChooseButtons[i].DisplayBuff(buffBechosen[i]);
        }

        for (int i = 0; i < ChooseBuff.instance.buffChooseButtons.Length; i++) //������������ť
        {
            if (i < buffBechosen.Count)
            {
                ChooseBuff.instance.buffChooseButtons[i].gameObject.SetActive(true);
            }
            else
            {
                ChooseBuff.instance.buffChooseButtons[i].gameObject.SetActive(false);
            }
        }



    }
}
