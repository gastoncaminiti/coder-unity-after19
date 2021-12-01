using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int lifePlayer = 3;
    [SerializeField] private float cameraAxisX = -90f;
    [SerializeField] private float speedPlayer = 3f;
    [SerializeField] private float speedTurn= 7f;

    [SerializeField] private Animator animPlayer;
    [SerializeField] private AudioClip punchSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private GameObject mPlayer;

    [SerializeField] private TextMeshPro usernameText;

    private AudioSource audioPlayer;
    private Rigidbody rbPlayer;
    private InventoryManager mgInventory;


    //EVENTS
    //public static event Action onDeath;

    public static event Action<int> onLivesChange;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        rbPlayer = GetComponent<Rigidbody>();
        mgInventory = GetComponent<InventoryManager>();
        animPlayer.SetBool("isRun", false);
        //usernameText.text = ProfileManager.instance.GetPlayerName();
        SetPlayerRotation();
        Debug.Log(onLivesChange.GetInvocationList().Length);
        onLivesChange?.Invoke(lifePlayer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioPlayer.PlayOneShot(punchSound, 1f);
            animPlayer.SetBool("isPunch", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animPlayer.SetBool("isPunch", false);
        }


        if (Input.GetKeyUp(KeyCode.G) && mgInventory.InventoryOneHas())
        {
            UseItem();
        }

        if (Input.GetKeyUp(KeyCode.H) && mgInventory.InventoryTwoHas())
        {
            UseItem();
        }

        if (Input.GetKeyUp(KeyCode.J) && mgInventory.InventoryThreeHas())
        {
            UseItem();
        }
    }

    private void FixedUpdate()
    {
        Move();
        RotatePlayer();
    }

    private void Move()
    {
        float ejeHorizontal = Input.GetAxis("Horizontal");
        float ejeVertical   = Input.GetAxis("Vertical");

        if (ejeHorizontal != 0 || ejeVertical != 0) {
            animPlayer.SetBool("isRun", true);
            rbPlayer.AddRelativeForce(Vector3.forward * speedPlayer * ejeVertical, ForceMode.Force);
            rbPlayer.AddRelativeForce(Vector3.right  * speedPlayer * ejeHorizontal, ForceMode.Force);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(walkSound, 0.5f);
            }
        }
        else
        {
            animPlayer.SetBool("isRun", false);
            ResetVelocities();
        }
    }
    private void RotatePlayer()
    {
        if(Input.GetAxisRaw("Mouse X") != 0) { 
            cameraAxisX += Input.GetAxisRaw("Mouse X");
            SetPlayerRotation();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lifePlayer--;
            Destroy(collision.gameObject);
            //onDeath?.Invoke();
            onLivesChange?.Invoke(lifePlayer);
            if (lifePlayer == 0)
            {
                //onDeath();
                //onDeath?.Invoke();
                PlayerEvents.OnDeath();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Generator"))
        {
            other.gameObject.GetComponent<GeneratorController>().setNewColor(Color.black);
        }

        if (other.gameObject.CompareTag("Food"))
        {
            GameObject food = other.gameObject;
            food.SetActive(false);
            //mgInventory.AddInventoryOne(food);
            //mgInventory.SeeInventoryOne();
            //mgInventory.AddInventoryTwo(food);
            //mgInventory.SeeInventoryTwo();
            mgInventory.AddInventoryThree(food.name, food);
            mgInventory.SeeInventoryThree();
            mgInventory.CountFood(food);
        }

        if (other.gameObject.CompareTag("Life"))
        {
            lifePlayer++;
            onLivesChange?.Invoke(lifePlayer);
            Destroy(other.gameObject);
        }


    }

    private void UseItem()
    {
        //GameObject food = mgInventory.GetInventoryOne();
        //GameObject food = mgInventory.GetInventoryTwo();
        GameObject food = mgInventory.GetInventoryThree("egg");
        food.SetActive(true);
        food.transform.position = transform.position + new Vector3(1f,.1f,.1f);
    }

    private void SetPlayerRotation()
    {
        Quaternion angulo = Quaternion.Euler(0, cameraAxisX * speedTurn, 0);
        transform.rotation = angulo;
        mPlayer.transform.rotation = transform.rotation;
    }

    private void ResetVelocities()
    {
        rbPlayer.velocity = Vector3.zero;
        rbPlayer.angularVelocity = Vector3.zero;
    }
}
