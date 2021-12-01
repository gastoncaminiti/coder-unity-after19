using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float forcePower = 4f;

    private Rigidbody rbItem;


    void Start()
    {
        rbItem = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rbItem.AddForce(Vector3.up * forcePower, ForceMode.Impulse);
        }

        if (collision.contacts[0].otherCollider.gameObject.CompareTag("PlayerHand"))
        {
            GameManager.score += 1;
            GameManager.instance.addScore();
            Debug.Log(GameManager.GetScore());
            Debug.Log(GameManager.score);
            Destroy(gameObject);
        }
    }
}
