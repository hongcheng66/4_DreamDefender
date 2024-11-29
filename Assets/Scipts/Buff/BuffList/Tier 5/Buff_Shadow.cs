using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Shadow : Buff
{
    private GameObject player;

    private GameObject shadow;
    private List<GameObject> shadows = new List<GameObject>();
    private int amount = 2;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().awakeStat == false && amount > 0)
        {
            StartCoroutine(Clone());
        }
        if(player.GetComponent<PlayerController>().awakeStat == true)
        {
            if(player.GetComponent<PlayerController>().dreamComeTrue == false)
            {
                for(int i = 0;i < shadows.Count;i++)
                {
                    Destroy(shadows[i]);
                }
                shadows.Clear();
                amount = 2;
            }
        }
    }

    private IEnumerator Clone()
    {
        amount--;
        yield return new WaitForSeconds(1f);

        shadow = Instantiate(player, transform.position, Quaternion.identity);
        shadow.tag = "Fake";
        shadow.GetComponent<PlayerController>().assignedbuffs.Clear();
        shadow.GetComponent<PlayerController>().unassignedbuffs.Clear();

        Transform[] childTransforms = shadow.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in childTransforms)
        {
            if (child.name == "Health Canvas")
            {
                Destroy(child.gameObject);
            }
        }
        shadows.Add(shadow);
    }
}
