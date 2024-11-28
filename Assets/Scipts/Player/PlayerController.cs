using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public Animator anim;
    private SpriteRenderer spriteRenderer;

    private float originSpeed = 3f;
    public float moveSpeed = 3f;
    public float attackRange = 15f; //自动攻击范围
    public bool isDead = false;


    //public Weapon activeWeapon;
    [Header("WakeStat")]
    public bool awakeStat = true; //true代表现在是清醒梦 false代表梦游状态
    public GameObject core;
    private Transform targetTransform;
    private bool enemyFound;

    public float switchCooldown = 3f; //切换形态的冷却时间
    public float switchcooldownCounter;

    [Header("Weapons")]
    public List<Weapon> unassignedWeapons, assignedWeapons;
    public int maxWeapons = 3;
    [HideInInspector]
    public List<Weapon> fullyLeveledWeapons = new List<Weapon>();

    [Header("BUFF")]
    public List<Buff> unassignedbuffs, assignedbuffs;


    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (assignedWeapons.Count == 0)
        {
            //AddWeapon(Random.Range(0,unassignedWeapons.Count));   
            AddWeapon(2);
        }

        for (int i = 0; i < unassignedWeapons.Count; i++)
        {
            unassignedWeapons[i].weaponLevel = -1; //-1代表武器未解锁 用于商店展示
        }

        moveSpeed = originSpeed;
        maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);

    }

    // Update is called once per frame
    void Update()
    {
        switchcooldownCounter -= Time.deltaTime;

        if (isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.F) && switchcooldownCounter < 0)
            {
                switchcooldownCounter = switchCooldown;
                StateController.instance.SetCooldownOf(); //设置切换形态的CD

                UIController.instance.ChangeAwakeIcon();
                ChangeAwakeStat();
            }

            if (awakeStat == true)
            {
                Flip();
                PlayerMove();
            }
            else
            {
                PlayerAutoMode();
            }
        }
    }

    public void ChangeAwakeStat()
    {
        if (awakeStat == true)
        {
            awakeStat = false;
            moveSpeed *= 0.8f;
        }
        else
        {
            awakeStat = true;
            moveSpeed = moveSpeed / 0.8f;
        }
    }

    public void ChangeSpeed(float changeSpeed)
    {
        moveSpeed = originSpeed + changeSpeed;
    }

    private void Flip()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z - Camera.main.orthographicSize;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (mouseWorldPosition.x - transform.position.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }

    private void PlayerMove()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    private void PlayerAutoMode()
    {
        SearchEnemy();
    }

    private void SearchEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // 遍历所有找到的碰撞体

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                targetTransform = collider.transform;
                enemyFound = true;
                break;
            }
            enemyFound = false;
        }
        if (enemyFound)
        {
            PlayerSleepWake(targetTransform);
        }
        else
        {
            PlayerSleepWake(core.transform);
        }
    }

    private void PlayerSleepWake(Transform target)
    {
        Vector3 moveTowords = (target.position - transform.position).normalized;
        transform.position += moveTowords * moveSpeed * Time.deltaTime;

        if (moveTowords != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            unassignedWeapons[weaponNumber].weaponLevel = 0;
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {

        weaponToAdd.weaponLevel = 0;
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
    public void DeleteWeapon(int weaponNumber)
    {
        if (weaponNumber < assignedWeapons.Count)
        {
            assignedWeapons[weaponNumber].weaponLevel = -1;
            unassignedWeapons.Add(assignedWeapons[weaponNumber]);

            assignedWeapons[weaponNumber].gameObject.SetActive(false);
            assignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void DeleteWeapon(Weapon weaponToDelete)
    {
        weaponToDelete.weaponLevel = -1;
        weaponToDelete.gameObject.SetActive(false);

        unassignedWeapons.Add(weaponToDelete);
        assignedWeapons.Remove(weaponToDelete);
    }

    public void AddBuff(int buffNumber)
    {
        if (buffNumber < unassignedbuffs.Count)
        {
            assignedbuffs.Add(unassignedbuffs[buffNumber]);

            unassignedbuffs[buffNumber].gameObject.SetActive(true);
            unassignedbuffs.RemoveAt(buffNumber);
        }
    }

    public void AddBuff(Buff buffToAdd)
    {
        buffToAdd.gameObject.SetActive(true);

        assignedbuffs.Add(buffToAdd);
        unassignedbuffs.Remove(buffToAdd);
    }
}
