using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoss : Zombie
{
    //[SerializeField] private int shield = 1000;
    
    [SerializeField] private GameObject player;

    [SerializeField] protected GameObject originTwo;
    [SerializeField] protected GameObject originThree;


    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }

    public override void Move()
    {
        Vector3 playerDirection = GetPlayerDirection();
        rbZombie.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        rbZombie.AddForce(playerDirection * myData.Speed, ForceMode.Impulse);
    }

    public override void FindEnemy()
    {
        base.FindEnemy();
        BroadCastRaycast(originTwo.transform);
        BroadCastRaycast(originThree.transform);
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();
        DrawRay(originTwo.transform);
        DrawRay(originThree.transform);
    }
}
