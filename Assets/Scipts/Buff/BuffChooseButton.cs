using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class BuffChooseButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text nameLevelText;
    public Image buffIcon;

    private Buff assignedBuff;

    public void DisplayBuff(Buff _buff)
    {
        if(_buff != null)
        {
            buffIcon.sprite = _buff.buffstats.icon;
            nameLevelText.text = _buff.buffstats.name;
        }
        assignedBuff = _buff;
    }

    public void SelectBuff()
    {
        if(assignedBuff != null)
        {
            PlayerController.instance.AddBuff(assignedBuff);
            UIController.instance.buffGetPanel.SetActive(false);
            UIController.instance.buffToolTip.HideToolTip();
            Time.timeScale = 1f;

            if(EnemySpawner.instance.canGoNextWave == false)
            {
                ExperienceLevelController.instance.UpgradePanel();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIController.instance.buffToolTip.ShowToolTip(assignedBuff);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIController.instance.buffToolTip.HideToolTip();
    }
}
