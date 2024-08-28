using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public GameObject cam1;
    public GameObject ch;
    public int myPosition = 0; //0 is home, 1 is outisde
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("hello");
                ch.transform.position = new Vector3(-60,-40, 0);
                cam1.SetActive(true);
            }


        }
    }
}
