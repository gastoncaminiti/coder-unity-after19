using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ZombieData", menuName = "Zombie Data")]
public class ZombieData : ScriptableObject
{
    [SerializeField]
    private string zombieName;
    [SerializeField]
    private int hp;
    [SerializeField] 
    private int shield;
    [SerializeField]
    private float speed;
    [SerializeField] 
    private float distanceRay;
    [SerializeField]
    private float damageHit;
    //GETTER
    public string ZombieName { 
        get
        {
            return zombieName;
        } 
    }

    public int HP
    {
        get
        {
            return hp;
        }
    }

    public int Shield
    {
        get
        {
            return shield;
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public float DistanceRay
    {
        get
        {
            return distanceRay;
        }
    }
}
