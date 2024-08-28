/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public GameObject image;

    // Update is called once per frame
    void Update()
    {
        checkClick();
    }

    public void checkClick()    //Press Any Key
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Clicked");
            image.SetActive(true);
            nextScene();
            
        }
    }

    public void nextScene()
    {
        if (Input.anyKeyDown)
        {

            SceneManager.LoadScene("inGame");

        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public GameObject image;
    private bool isImageShown = false;

    // Update is called once per frame
    void Update()
    {
        checkClick();
    }

    public void checkClick()    //Press Any Key
    {
        if (Input.anyKeyDown)
        {
            if (!isImageShown)
            {
                Debug.Log("Clicked");
                image.SetActive(true);
                isImageShown = true; // 이미지가 보여진 상태로 변경
            }
            else
            {
                nextScene();
            }
        }
    }

    public void nextScene()
    {
        SceneManager.LoadScene("inGame");
    }
}