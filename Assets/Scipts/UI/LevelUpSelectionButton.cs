using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class LevelUpSelectionButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    private Weapon assignedWeapon;

    private GameObject target;
    private PlayerController player;

    private void Start()
    {
        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;
        player = target.GetComponent<PlayerController>();
    }

    public void UpdateButtonDisplay(Weapon theWeapon)
    {

        if(theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = "Éý¼¶";
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.stats[theWeapon.weaponLevel].name;
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
                if(player.assignedWeapons.Count + player.fullyLeveledWeapons.Count < player.maxWeapons)
                {
                    player.AddWeapon(assignedWeapon);
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

                    for (int i = 0;i < player.assignedWeapons.Count;i++)
                    {
                        UI_ChangeWeapon.instance.levelUpButtons[i].UpdateButtonDisplay(player.assignedWeapons[i]);
                    }

                    for (int i = 0; i < UI_ChangeWeapon.instance.levelUpButtons.Length; i++)
                    {
                        if (i < player.assignedWeapons.Count)
                        {
                            UI_ChangeWeapon.instance.levelUpButtons[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            UI_ChangeWeapon.instance.levelUpButtons[i].gameObject.SetActive(false);
                        }
                    }
                    player.AddWeapon(assignedWeapon);
                }
                    
            }
        }
    }

    public void ChangeWeapon()
    {
        if(assignedWeapon != null)
        {
            player.DeleteWeapon(assignedWeapon);
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
