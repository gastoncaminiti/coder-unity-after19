using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speedEnemy  = 50f;
    [SerializeField] private float attackRange = 1f;

    private GameObject player;
    private Rigidbody rbEnemy;
    private Animator  animEnemy;

    private bool isAttack = false;

    void Start()
    {
        player = GameObject.Find("Player");
        rbEnemy = GetComponent<Rigidbody>();
        animEnemy = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        animEnemy.SetBool("isAttack", isAttack);
    }

    private void FixedUpdate()
    {
        Vector3 playerDirection = GetPlayerDirection();
        if(playerDirection.magnitude > attackRange)
        {
            isAttack = false;
            rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
            rbEnemy.AddForce(playerDirection.normalized * speedEnemy, ForceMode.Impulse);
        }
        else
        {
            isAttack = true;
        }
        
    }

    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }

}
