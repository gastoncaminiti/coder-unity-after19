using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textCheese;
    [SerializeField] private Text textCookie;
    [SerializeField] private Text textEgg;
    [SerializeField] private TextMeshProUGUI textLives;

    [SerializeField]  private InventoryManager mgInventory;
    [SerializeField]  private GameObject panelFood;

    // Start is called before the first frame update

    private void Awake()
    {
        PlayerController.onLivesChange += OnLivesChangeHandler;
        //PlayerCharacterController.onLivesChange += OnLivesChangeHandler;
    }

    void Start()
    {
         //GameObject.Find("Player").GetComponent<PlayerController>().onDeath += OnDeadHandler;
        PlayerEvents.onDeath += OnDeadHandler;
        //PlayerController.onLivesChange += OnLivesChangeHandler;
     }

    public void OnDeadHandler()
    {
        textLives.text = "GAME OVER";
    }

    public void OnLivesChangeHandler(int lives)
    {
        textLives.text = "LIVES "+lives;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFoodUI();
    }

     void UpdateFoodUI()
    {
        int[] foodCount = mgInventory.GetFoodQuantity();
        textCheese.text = "x"+foodCount[0];
        textCookie.text = "x"+foodCount[1];
        textEgg.text = "x"+foodCount[2];
    }

    public void TooglePanel()
    {
        panelFood.SetActive(!panelFood.activeSelf);
    }
}
