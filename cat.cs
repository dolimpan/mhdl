using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    public GameObject To;
    private dotDamage dD;

    public GameObject thisobj;
    private CameraFollowWithShake cameraShake;

    public GameObject targetObject1; // Detecter ?????????? ???? ????????
    private player playerScript;


    public GameObject targetObject2; // Detecter ?????????? ???? ????????
    private catManager catScript;

    public Rigidbody2D rb;
    public Transform pT;
    public Animator anim;
    public AudioSource ac;
    // Start is called before the first frame update

    public void SoundStart()
    {
        ac.Play();
    }
    void Start()
    {
        //To = GameObject.Find("dotManager");
        //dD = To.GetComponent<dotDamage>();

        cameraShake = Camera.main.GetComponent<CameraFollowWithShake>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        targetObject1 = GameObject.Find("char");
        playerScript = targetObject1.GetComponent<player>();
        pT = playerScript.transform;

        targetObject2 = GameObject.Find("catManager");
        catScript = targetObject2.GetComponent<catManager>();
    }

    void whatToDo()
    {
        Vector2 pP = pT.position;
        float playerX = pP.x;
        float playerY = pP.y;

        Vector2 currentPosition = transform.position;

        if (Vector2.Distance(currentPosition, pP) < 15f)
        {
            //Debug.Log("사정거리내 진입");
            anim.SetBool("standBy", true);
           // ac.Play();
            Invoke("rush", 3f);
        }
    }

    void rush()
    {
        anim.SetBool("letsGo", true);
        //Debug.Log("출발");
        Vector2 target = pT.position;
        Vector2 currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(currentPosition, target, 70 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (playerScript.isFerrying == true)
            {
                Debug.Log("페리공");
                rb.AddForce(new Vector2(0, 150), ForceMode2D.Impulse);
                catScript.catCount--;
               Destroy(thisobj, 0.5f);
            }

            else
            {
                //dD.catHit = true;
                cameraShake.TriggerShake(0.5f);
                playerScript.health -= 30;
                catScript.catCount--;
                Destroy(thisobj);
            }

            
        }

        if(collision.tag == "house")
        {
            catScript.catCount--;
            Destroy(thisobj);
            Debug.Log("집에 생성됐습니다.");
        }

    }


    // Update is called once per frame
    void Update()
    {
        whatToDo();   
    }

}
