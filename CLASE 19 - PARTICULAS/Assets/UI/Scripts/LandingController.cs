using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LandingController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_InputField inputUserName;
    [SerializeField] private TextMeshProUGUI textTitle;

    void Start()
    {
        inputUserName.onEndEdit.AddListener(OnEndInputUsername);
        textTitle.text = "PROYECTO BETA";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndInputUsername(string newString)
    {
        Debug.Log("DINAMICO END" + newString);
        ProfileManager.instance.SetPlayerName(newString);
        Debug.Log("NEW PLAYER NAME " + ProfileManager.instance.GetPlayerName());
    }

    public void OnChangeToggleName(bool newStatus)
    {
        Debug.Log("DINAMICO END" + newStatus);
        ProfileManager.instance.SetVisibleName(newStatus);
        Debug.Log("NEW PLAYER NAME " + ProfileManager.instance.GetVisibleName());
    }

    public void OnChangeSlider(float newValue)
    {
        Debug.Log(newValue);
    }

    public void OnClickPlay()
    {
        Debug.Log("A JUGAR!");
        textTitle.text = "CARGANDO...";
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(SceneManager.GetActiveScene().name);
        //SceneManager.LoadSceneAsync("Level1");
        SceneManager.LoadScene("Level1");
    }
}
