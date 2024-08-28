using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nggs : MonoBehaviour
{
    public GameObject targeto;
    private questManager questManager;

    public GameObject shell;
    public bool onAttack;
    public GameObject rect;
    public GameObject target;
    public player pT;
    public Rigidbody2D pR;
    public float rightTime;
    // Start is called before the first frame update
    void Start()
    {
        targeto = GameObject.Find("questManager");
        questManager = targeto.GetComponent<questManager>();

        target = GameObject.Find("char");
        pT = target.GetComponent<player>();
        pR = target.GetComponent<Rigidbody2D>();
        Invoke("turn", 7f);




    }

    public void turn()
    {
        {
            onAttack = true;
        }
        
    }

    void turnOff()
    {
        onAttack = false;
        Invoke("turn", 7f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pT.health -= 20;
            pT.cameraShaking();
        }
        //pR.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse)
    }


    void attack()
    {
        rect.SetActive(false);
        Vector2 targetPosition = new Vector2((transform.position.x) - 20, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 50 * Time.deltaTime);
    }

    void follow()
    {
        if(onAttack == false)
        {
            Vector2 targetPosition = new Vector2((pT.transform.position.x) + 10, pT.transform.position.y + 4);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 50 * Time.deltaTime);
        }
        else
        {
            rect.SetActive(true);
            Invoke("attack", 1f);
            Invoke("turnOff", 1.5f);
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(pT.isHome == false)
        {
            follow();
        }

    }
}
