using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacterController : MonoBehaviour
{
    //DESING DATA [CANDIDATO A OBJETO SERIALIZADO]
    [SerializeField] private int lifePlayer = 3;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float Gravity = -9.81f;

    //RUNTIME DATA
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Transform cam;
    [SerializeField] private float mouseSensitivity = 2f;

    //PRIVATE COMPONENTS REFERENCE
    [SerializeField] private Animator animPlayer;
    private CharacterController cc;


    //EVENTS
    public static event Action onDeath;
    public static event Action<int> onLivesChange;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        animPlayer.SetBool("isRun", false);
        onLivesChange?.Invoke(lifePlayer);
    }
    void Update()
    {
        //MOVER
        Move();
        //ROTAR
        Rotate();
        //SALTAR
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            animPlayer.SetBool("isRun", false);
            velocity.y = Mathf.Sqrt(-5f * Gravity);
        }
        //GOLPEAR
        if (Input.GetKeyDown(KeyCode.F))
        {
            animPlayer.SetBool("isPunch", true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animPlayer.SetBool("isPunch", false);
        }
        //APLICAR GRAVEDAD
        velocity.y += Gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }


    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            animPlayer.SetBool("isRun", true);
            cc.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animPlayer.SetBool("isRun", false);
        }
        /* FIX CHILD POSITION
        animPlayer.gameObject.transform.position = transform.position;
        animPlayer.gameObject.transform.rotation = transform.rotation;
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lifePlayer--;
            Destroy(collision.gameObject);
            onLivesChange?.Invoke(lifePlayer);
            if (lifePlayer == 0)
            {
                onDeath?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Life"))
        {
            lifePlayer++;
            onLivesChange?.Invoke(lifePlayer);
            Destroy(other.gameObject);
        }
    }

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ESTOY EN EL PISO");
        }
    }
    */

}
