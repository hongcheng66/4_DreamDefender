using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void StorePanel()
    {
        UIController.instance.treasureAnimation.SetActive(false);

        if(EnemySpawner.instance.currentWave + 2 > EnemySpawner.instance.waves.Count)
            LevelManager.instance.PassGame();
        else
            BuffController.instance.UpgradeBuffPanel();
    }
}
