using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonsController : MonoBehaviour
{
    // Start is called before the first frame update
    //ARRAY DE CAÑONES
    [SerializeField] GameObject[] cannons;
    [SerializeField] GameObject selected;

    void Start()
    {
        /*
        cannons[0].SetActive(false);
        cannons[1].SetActive(false);
        cannons[2].SetActive(false);
        */
        DiseableCannons();
        ActivateOne();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DiseableCannons()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            // cannons[i].SetActive(false);
            //CannonController componente = cannons[i].GetComponent<CannonController>();
            //componente.SetActiveCannon(false);
            GetCannonComponent(cannons[i]).SetActiveCannon(false);
        }
    }

    private void ActivateOne()
    {
        int cannonIndex = Random.Range(0, cannons.Length);
        //cannons[cannonIndex].SetActive(true);
        GetCannonComponent(cannons[cannonIndex]).SetActiveCannon(true);
        setSelected(cannons[cannonIndex].transform);
    }

    private void setSelected(Transform target)
    {
        selected.transform.position = new Vector3(target.position.x, target.position.y + 1.8f, target.position.z);
        selected.SetActive(true);
    }

    private CannonController GetCannonComponent(GameObject cannnon)
    {
        return cannnon.GetComponent<CannonController>();
    }
}
