using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class BuffChooseButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text nameLevelText;
    public Image buffIcon;

    private Buff assignedBuff;

    private GameObject target;
    private PlayerController player;

    private void Start()
    {
        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;
        player = target.GetComponent<PlayerController>();
    }

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
            player.AddBuff(assignedBuff);
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
