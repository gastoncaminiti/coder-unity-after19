using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

     private  Stack inventoryOne;
     private  Queue inventoryTwo;
     Dictionary<string, GameObject> inventoryThree;

     [SerializeField] private int[] foodQuantity = { 0, 0, 0 };

    void Start()
    {
        inventoryOne = new Stack();
        inventoryTwo = new Queue();
        inventoryThree = new Dictionary<string, GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountFood(GameObject food)
    {
        FoodController f = food.GetComponent<FoodController>();
        switch (f.GetTypeFood())
        {
            case GameManager.typesFood.Chesee:
                foodQuantity[0]++;
                break;
            case GameManager.typesFood.Cookie:
                foodQuantity[1]++;
                break;
            case GameManager.typesFood.Egg:
                foodQuantity[2]++;
                break;
            default:
                Debug.Log("NO SE PUEDE CONTAR");
                break;
        }
    }

    public int[] GetFoodQuantity()
    {
        return foodQuantity;
    }

    public void AddInventoryOne(GameObject item)
    {
        inventoryOne.Push(item);
    }

    public GameObject GetInventoryOne()
    {
        return inventoryOne.Pop() as GameObject;
    }

    public void SeeInventoryOne()
    {
        Debug.Log(inventoryOne.ToString());
        foreach (var item in inventoryOne)
        {
            Debug.Log(item.ToString());
        }
    }

    public bool InventoryOneHas()
    {
        return inventoryOne.Count > 0;
    }
    //-------------------------- INVENTORY QUEQUE -------------------------//
    public void AddInventoryTwo(GameObject item)
    {
        inventoryTwo.Enqueue(item);
    }

    public GameObject GetInventoryTwo()
    {
        return inventoryTwo.Dequeue() as GameObject;
    }

    public void SeeInventoryTwo()
    {
        Debug.Log(inventoryTwo.ToString());
        foreach (var item in inventoryTwo)
        {
            Debug.Log(item.ToString());
        }
    }

    public bool InventoryTwoHas()
    {
        return inventoryTwo.Count> 0;
    }

    //-------------------------- INVENTORY DIC -------------------------//
    public void AddInventoryThree(string key,GameObject item)
    {
        inventoryThree.Add(key, item);
    }

    public GameObject GetInventoryThree(string key)
    {
        return inventoryThree[key] as GameObject;
    }

    public void SeeInventoryThree()
    {
        Debug.Log(inventoryThree.ToString());
        foreach (var item in inventoryThree)
        {
            Debug.Log(item.ToString());
        }
    }

    public bool InventoryThreeHas()
    {
        return inventoryThree.Count > 0;
    }
}
