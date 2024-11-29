using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FlyingSword : MonoBehaviour
{
    public float moveSpeed;
    public float searchRadius;
    public float duration;

    private Collider2D currentTarget;
    private float timeSinceLastTargetChange;

    private GameObject target;
    private PlayerController player;

    private void Start()
    {
        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;
        player = target.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (currentTarget == null || timeSinceLastTargetChange >= duration)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);
            List<Collider2D> enemies = new List<Collider2D>();

            foreach (var collider in colliders)
            {
                if (collider.tag == "Enemy" && !enemies.Contains(collider)) 
                {
                    enemies.Add(collider);
                }
            }

            if (enemies.Count > 0)
            {
                currentTarget = enemies[0];
                timeSinceLastTargetChange = 0f;
            }
        }

        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.Translate(direction * moveSpeed * Time.deltaTime);

            timeSinceLastTargetChange += Time.deltaTime;

        }

        if(player.GetComponent<PlayerController>().awakeStat == true && player.dreamComeTrue == false)
        {
            Destroy(gameObject);
        }

    }

}
