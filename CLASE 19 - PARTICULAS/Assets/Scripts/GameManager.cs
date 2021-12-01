using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update

    //SCORE
    public static int score;
    //ENUMERACION PUBLICA
    public enum typesFood { Chesee , Cookie, Egg };

    private int scoreInstanciado;
    private PlayerController playerScript;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            score = 0;
            scoreInstanciado = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //playerScript.onDeath += GameOver;
        //PlayerController.onDeath += GameOver;
        PlayerEvents.onDeath += GameOver;

    }

    private void GameOver()
    {
        Debug.Log("EL JUEGO TERMINO");
        scoreInstanciado = 0;
    }


    public void addScore()
    {
        instance.scoreInstanciado += 1;  
    }

    public static int GetScore()
    {
        return instance.scoreInstanciado;
    }
}
