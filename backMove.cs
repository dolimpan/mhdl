using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMove : MonoBehaviour
{
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
                collision.transform.position = new Vector3(-190, -77, 0);

            }


        }
    }
}
