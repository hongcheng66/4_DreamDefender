using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {

        if(theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = "Éý¼¶";
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.stats[theWeapon.weaponLevel].name + "Lv" + theWeapon.weaponLevel;
        }
        else
        {
            upgradeDescText.text = "»ñµÃ";
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.stats[0].name;
        }
        assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            { 
                assignedWeapon.LevelUp();
                UIController.instance.levelUpPanel.SetActive(false);
                UIController.instance.weaponToolTip.HideToolTip();
                Time.timeScale = 1f;

                if (EnemySpawner.instance.canGoNextWave == false)
                    EnemySpawner.instance.canGoNextWave = true;
            }
            else
            {
                if(PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLeveledWeapons.Count < PlayerController.instance.maxWeapons)
                {
                    PlayerController.instance.AddWeapon(assignedWeapon);
                    UIController.instance.levelUpPanel.SetActive(false);
                    UIController.instance.weaponToolTip.HideToolTip();
                    Time.timeScale = 1f;

                    if (EnemySpawner.instance.canGoNextWave == false)
                        EnemySpawner.instance.canGoNextWave = true;
                }
                else
                {

                    UIController.instance.levelUpPanel.SetActive(false);
                    UIController.instance.changeWeaponPanel.SetActive(true);

                    for (int i = 0;i < PlayerController.instance.assignedWeapons.Count;i++)
                    {
                        UI_ChangeWeapon.instance.levelUpButtons[i].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[i]);
                    }

                    for (int i = 0; i < UI_ChangeWeapon.instance.levelUpButtons.Length; i++)
                    {
                        if (i < PlayerController.instance.assignedWeapons.Count)
                        {
                            UI_ChangeWeapon.instance.levelUpButtons[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            UI_ChangeWeapon.instance.levelUpButtons[i].gameObject.SetActive(false);
                        }
                    }
                    PlayerController.instance.AddWeapon(assignedWeapon);
                }
                    
            }
        }
    }

    public void ChangeWeapon()
    {
        if(assignedWeapon != null)
        {
            PlayerController.instance.DeleteWeapon(assignedWeapon);
        }
        UIController.instance.changeWeaponPanel.SetActive(false);
        UIController.instance.weaponToolTip.HideToolTip();
        Time.timeScale = 1f;

        if (EnemySpawner.instance.canGoNextWave == false)
            EnemySpawner.instance.canGoNextWave = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIController.instance.weaponToolTip.ShowToolTip(assignedWeapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIController.instance.weaponToolTip.HideToolTip();
    }
}
