using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : MonoBehaviour
{
    EnemyController enemyControllerListner;

    public GameObject splitSlime;

    public int splitNumber = 2;
    // Start is called before the first frame update
    void Start()
    {
        enemyControllerListner = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyControllerListner.isalive == false)
        {
            for(int i = 0;i < splitNumber;i++)
            {
                Instantiate(splitSlime, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
