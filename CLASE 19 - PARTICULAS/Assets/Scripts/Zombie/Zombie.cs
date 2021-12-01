using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //[SerializeField] protected float speedEnemy = 50f;
    //[SerializeField] private     float distanceRay = 10f;
    [SerializeField] protected GameObject originOne;


    [SerializeField] protected ZombieData myData; 

    protected Rigidbody rbZombie;
    protected Animator animZombie;

    private bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(myData.ZombieName);
        rbZombie = GetComponent<Rigidbody>();
        animZombie = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAttack)
        {
            FindEnemy();
            Move();
        }
    }

    public virtual void Move()
    {
        Vector3 direction = Vector3.left;
        rbZombie.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rbZombie.AddForce(direction * myData.Speed, ForceMode.Impulse);
    }

    public virtual void FindEnemy()
    {
        BroadCastRaycast(originOne.transform);
    }

    protected void BroadCastRaycast(Transform origen)
    {
        RaycastHit hit;
        if (Physics.Raycast(origen.position, origen.TransformDirection(Vector3.forward), out hit, myData.DistanceRay))
        {
            if (hit.transform.CompareTag("Player"))
            {
                isAttack = true;
                rbZombie.velocity = Vector3.zero;
                animZombie.SetBool("isAttack", isAttack);
            }
        }
    }

    protected void DrawRay(Transform origen)
    {
        Gizmos.color = Color.blue;
        Vector3 direction = origen.TransformDirection(Vector3.forward) * myData.DistanceRay;
        Gizmos.DrawRay(origen.position, direction);
    }

    public virtual void DrawRaycast()
    {
        DrawRay(originOne.transform);
    }

    void OnDrawGizmos()
    {
        if (!isAttack)
        {
            DrawRaycast();
        }
    }
}
