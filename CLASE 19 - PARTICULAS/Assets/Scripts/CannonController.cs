using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject shootOrigen;
    [SerializeField] private float distanceRay = 10f;
    [SerializeField] private int shootCooldown = 2;

    [SerializeField] private float timerShoot = 0;
    [SerializeField] private GameObject bulletPrefab;

    private bool  canShoot = true;
    [SerializeField]  private bool  isActive = true;

    [SerializeField] private ParticleSystem shootParticle;

    /*
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    */

    public GameObject[] waypoints;

    void Start()
    {
        //StartCoroutine(RotateBehavior());
        //StartCoroutine(WaypointsBehavior());
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) { 
            if (canShoot)
            {
                RaycastCannon();
            }
            else
            {
               timerShoot += Time.deltaTime;
            }

     
            if(timerShoot > shootCooldown)
            {
                canShoot = true;
            }
        }
    }

    IEnumerator RotateBehavior()
    {
        /* yield return new WaitForSeconds(2f);
        Debug.Log("ROTAR CAÑON");
        transform.Rotate(0, 45f, 0);
        yield return new WaitForSeconds(2f);
        transform.Rotate(0, 45f, 0);
        */
        /*
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(3f);
            transform.Rotate(0, 90f, 0);
        }
        */
        while (isActive)
        {
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(3f);
                transform.Rotate(0, 90f, 0);
            }
        }

    }

    IEnumerator WaypointsBehavior()
    {
        /*
        transform.position = p1.transform.position;
        yield return new WaitForSeconds(4f);
        transform.position = p2.transform.position;
        */
        for (int i = 0; i < waypoints.Length; ++i)
        {
            while (transform.position !=  waypoints[i].transform.position)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, waypoints[i].transform.position, 10f * Time.deltaTime);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private void RaycastCannon()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(shootOrigen.transform.position, shootOrigen.transform.TransformDirection(Vector3.forward), out hit, distanceRay))
        {
            if(hit.transform.tag == "Player")
            {
                //Debug.Log("COLISION PLAYER");
                canShoot   = false;
                timerShoot = 0;
                GameObject b = Instantiate(bulletPrefab, shootOrigen.transform.position, bulletPrefab.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(shootOrigen.transform.TransformDirection(Vector3.forward) * 10f, ForceMode.Impulse);
                shootParticle.Play();
            }
        }

    }

    private void OnDrawGizmos()
    {

        if (canShoot && isActive) { 
            Gizmos.color = Color.blue;
            Vector3 direction = shootOrigen.transform.TransformDirection(Vector3.forward) * distanceRay;
            Gizmos.DrawRay(shootOrigen.transform.position, direction);
        }

    }

    public void SetActiveCannon(bool status)
    {
        isActive = status;
    }
}
