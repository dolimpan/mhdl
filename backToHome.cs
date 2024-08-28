using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backToHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void checkClick()
    {
        SceneManager.LoadScene("gameStart");
    }

    // Update is called once per frame
    void Update()
    {
        //checkClick();
    }
}
