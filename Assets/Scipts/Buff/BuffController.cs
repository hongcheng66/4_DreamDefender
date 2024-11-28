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

        for (int i = 0; i < PlayerController.instance.unassignedbuffs.Count; i++) //将所有满足等级条件的buff加入选择池子
        {
            if(EnemySpawner.instance.currentWave + 1 >= PlayerController.instance.unassignedbuffs[i].buffstats.level) //如果当前的回合数大于buff的等级 那么就可以获得这个buff
            {
                availableBuff.Add(PlayerController.instance.unassignedbuffs[i]);
            }
        }

        for (int i = 0;i < 4;i++) //4是槽位的数量
        { 
            int selected = Random.Range(0,availableBuff.Count);
            buffBechosen.Add(availableBuff[selected]);
            availableBuff.RemoveAt(selected);
        }

        for (int i = 0; i < buffBechosen.Count; i++) //将抽出的buff置入按钮中
        {
            ChooseBuff.instance.buffChooseButtons[i].DisplayBuff(buffBechosen[i]);
        }

        for (int i = 0; i < ChooseBuff.instance.buffChooseButtons.Length; i++) //根据数量亮起按钮
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
