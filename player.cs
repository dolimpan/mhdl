using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public GameObject death;
    public GameObject lv2;
    public GameObject lv1;
    public GameObject targetObject; // Detecter 스크립트가 있는 오브젝트
    private invenFinal final;

    public GameObject targeto;
    private questManager questManager;
    //public GameObject tttargetObject;
    //private dotDamage dot;

    public GameObject cam1;
    public GameObject cam2;

    public GameObject roach;
    public GameObject totalMask;
    public Image hpbar;
    public Image mpbar;
    public Image maskbar;
    public bool maskGo = false;
    public GameObject sink;
    private CameraFollowWithShake cameraShake;
    public GameObject lv0;
    public bool isFerrying = false;
    public bool stun = false;

    public Transform playerTransform; // 플레이어의 Transform
    public Vector2 bottomLeft = new Vector2(-194, -81); // 좌표 범위의 왼쪽 아래
    public Vector2 topRight = new Vector2(-160, -60); // 좌표 범위의 오른쪽 위
    public bool isHome = false; // 플레이어가 지정된 범위 내에 있는지 여부

    public GameObject inven;
    public bool invenOpened = false;
    private detector detecterScript;
    public GameObject collection;
    public bool backward = false;
    public bool forward = false;
    public GameObject detectCircle;
    public float speed = 3f;
    public GameObject straightLight;
    public GameObject rightdiaguplight;
    public GameObject rightdiagdownlight;
    public GameObject uplight;
    public GameObject downlight;
    public gameManager gm;
    public bool right = false;
    public bool left = false;
    public bool up = false;
    public bool down = false;
    public bool rightUpDiag = false;
    private bool leftUpDiag = false;
    private bool rightDownDiag = false;
    private bool leftDownDiag = false;
    private bool headingLeft = false;
    public Animator anim;
    public bool walking = false;
    public float health;
    public float mental;
    public float mask;
    public float electricity = 4000;
    public GameObject cam;
    //public cameraShake cameraShake;
    public GameObject frontMask;
    public GameObject sideMask;
    public GameObject backMask;

    public IEnumerator mentalDecrease;
    public IEnumerator maskDecrease;
    public IEnumerator maskFalling;
    public bool isMentalDecrease;
    public bool isMaskDecrease;
    public bool isHealth;

    void statusCheck()
    {
        hpbar.fillAmount = health / 100;
        mpbar.fillAmount = mental / 100;
        maskbar.fillAmount = electricity / 4000;

        if(health <= 0)
        {
            death.gameObject.SetActive(true);
        }
        if (mental <= 0)
        {
            mental = 0;
            if (!isMentalDecrease)
            {
                StartCoroutine(mentalDecrease);
            }
        }
        else
        {
            if (isMentalDecrease)
            {
                StopCoroutine(mentalDecrease);
            }
        }
        if(mask <= 0)
        {
            mask = 0;
            if (!isMaskDecrease)
            {
                StartCoroutine(maskDecrease);
            }
        }
        else
        {
            if (isMaskDecrease)
            {
                StopCoroutine(maskDecrease);
            }
        }
    }

    

    IEnumerator myMentalDecrease()
    {
        isMentalDecrease = true;
        while (true)
        {
            health -= 1;
            cameraShake.TriggerShake(0.1f);
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log("정신력<0 이므로 체력 1 깍임");
            if(health <= 0)
            {
                break;
            }
        }
        yield break;
    }

    IEnumerator myMaskDecrease()
    {
        isMaskDecrease = true;
        while (true)
        {
            //health -= 1;
            //cameraShake.TriggerShake(0.1f);
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log("방독면<0 이므로 체력 1 깍임");
            if (health <= 0)
            {
                break;
            }
        }
        yield break;
    }

    void diagCheck()
    {
        if (right && up)
        {
            cam2.transform.eulerAngles = new Vector3 (0, 0, 135);
            rightdiaguplight.SetActive(true);
            straightLight.SetActive(false);
            rightdiagdownlight.SetActive(false);
            uplight.SetActive(false);
            downlight.SetActive(false);

            if (backward == true)
            {
                backMask.SetActive(true);
                frontMask.SetActive(false);
                sideMask.SetActive(false);
            }
            else
            {

                backMask.SetActive(false);
                frontMask.SetActive(false);
                sideMask.SetActive(true);
            }

        }
        else if (right && !up && !down)
        {
            cam2.transform.eulerAngles = new Vector3(0, 0, 90);
            rightdiaguplight.SetActive(false);
            straightLight.SetActive(true);
            rightdiagdownlight.SetActive(false);
            uplight.SetActive(false);
            downlight.SetActive(false);

            backMask.SetActive(false);
            frontMask.SetActive(false);
            sideMask.SetActive(true);

        }

        else if (right && down)
        {
            cam2.transform.eulerAngles = new Vector3(0, 0, 45);
            rightdiaguplight.SetActive(false);
            straightLight.SetActive(false);
            rightdiagdownlight.SetActive(true);
            uplight.SetActive(false);
            downlight.SetActive(false);

            if (forward == true)
            {
                backMask.SetActive(false);
                frontMask.SetActive(true);
                sideMask.SetActive(false);
            }
            else
            {

                backMask.SetActive(false);
                frontMask.SetActive(false);
                sideMask.SetActive(true);
            }

        }

        else if (left && up)
        {
            cam2.transform.eulerAngles = new Vector3(0, 0, 225);
            if (backward == true)
            {
                backMask.SetActive(true);
                frontMask.SetActive(false);
                sideMask.SetActive(false);
            }
            else
            {

                backMask.SetActive(false);
                frontMask.SetActive(false);
                sideMask.SetActive(true);
            }

            rightdiaguplight.SetActive(true);
            straightLight.SetActive(false);
            rightdiagdownlight.SetActive(false);
            uplight.SetActive(false);
            downlight.SetActive(false);



        }

        else if (left && !up && !down)
        {

            cam2.transform.eulerAngles = new Vector3(0, 0, 270);
            rightdiaguplight.SetActive(false);
            straightLight.SetActive(true);
            rightdiagdownlight.SetActive(false);
            uplight.SetActive(false);
            downlight.SetActive(false);

            backMask.SetActive(false);
            frontMask.SetActive(false);
            sideMask.SetActive(true);

        }
        else if (left && down)
        {
            cam2.transform.eulerAngles = new Vector3(0, 0, 315);
            if (forward == true)
            {
                backMask.SetActive(false);
                frontMask.SetActive(true);
                sideMask.SetActive(false);
            }
            else
            {

                backMask.SetActive(false);
                frontMask.SetActive(false);
                sideMask.SetActive(true);
            }

            rightdiaguplight.SetActive(false);
            straightLight.SetActive(false);
            rightdiagdownlight.SetActive(true);
            uplight.SetActive(false);
            downlight.SetActive(false);

        }
        else if (up && !left && !right)
        {

            cam2.transform.eulerAngles = new Vector3(0, 0, 180);
            backward = true;
            rightdiaguplight.SetActive(false);
            straightLight.SetActive(false);
            rightdiagdownlight.SetActive(false);
            uplight.SetActive(true);
            downlight.SetActive(false);

            backMask.SetActive(true);
            frontMask.SetActive(false);
            sideMask.SetActive(false);
        }

        else if (down && !left && !right)
        {

            cam2.transform.eulerAngles = new Vector3(0, 0, 0);
            forward = true;
            rightdiaguplight.SetActive(false);
            straightLight.SetActive(false);
            rightdiagdownlight.SetActive(false);
            uplight.SetActive(false);
            downlight.SetActive(true);


            backMask.SetActive(false);
            frontMask.SetActive(true);
            sideMask.SetActive(false);
        }



    }

    void leftCheck()
    {
        if (headingLeft == true)
        {
            transform.localScale = new Vector3(-0.3527f, 0.3527f, 0.3527f);
        }
        else
        {
            transform.localScale = new Vector3(0.3527f, 0.3527f, 0.3527f);
        }

    }

    void ferry()
    {
        
        if (Input.GetKey(KeyCode.RightShift) && (!right) && (!left) &&(!up) && (!down))
        {
            isFerrying = true;
            stun = true;    
            anim.SetBool("ferry", true);
        }


        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            isFerrying = false;
            stun = false;
            anim.SetBool("ferry", false);
        }
    }

    void move()
    {
        if (stun == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                left = true;
                headingLeft = true;
                walking = true;
            }
            if (Input.GetKey(KeyCode.D))
            {

                //straightLight.transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                right = true;
                headingLeft = false;
                walking = true;
            }
            if (Input.GetKey(KeyCode.W))
            {

                anim.SetBool("isFacingBackward", true);
                //Debug.Log(straightLight.transform.position);
                //straightLight.transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                up = true;
                walking = true;
            }
            if (Input.GetKey(KeyCode.S))
            {

                transform.Translate(Vector2.down * speed * Time.deltaTime);
                down = true;
                walking = true;
                anim.SetBool("isFacingForward", true);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                headingLeft = false;
                left = false;
                straightLight.transform.rotation = Quaternion.Euler(0, 0, 0);
                straightLight.transform.rotation = Quaternion.Euler(0, 0, 0);
                walking = false;


                backMask.SetActive(false);
                frontMask.SetActive(true);
                sideMask.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                right = false;
                straightLight.transform.rotation = Quaternion.Euler(0, 0, 0);
                walking = false;

                backMask.SetActive(false);
                frontMask.SetActive(true);
                sideMask.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                backward = false;
                up = false;
                straightLight.transform.rotation = Quaternion.Euler(0, 0, 0);
                anim.SetBool("isFacingBackward", false);
                walking = false;

                backMask.SetActive(false);
                frontMask.SetActive(true);
                sideMask.SetActive(false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                forward = false;
                down = false;
                straightLight.transform.rotation = Quaternion.Euler(0, 0, 0);
                walking = false;
                anim.SetBool("isFacingForward", false);

                backMask.SetActive(false);
                frontMask.SetActive(true);
                sideMask.SetActive(false);
            }


        }
    }

    
        void masks()
        {
            if(stun == false)
            {
                if (Input.GetKeyUp(KeyCode.K))
                {

                    totalMask.SetActive(false);
                    
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    totalMask.SetActive(true);
                    
                    
                }
            }
            

        }

        void lights()
        {
            if(stun == false)
            {
                if(electricity > 0)
                {
                    if (Input.GetKeyUp(KeyCode.L))
                    {

                        collection.SetActive(false);
                        cam1.SetActive(true);
                        cam2.SetActive(false);

                    }
                    if (Input.GetKeyDown(KeyCode.L))
                    {
                        collection.SetActive(true);
                        cam1.SetActive(false);
                        cam2.SetActive(true);
                    }
                }
                else
                {
                    collection.SetActive(false);
                    cam1.SetActive(true);
                    cam2.SetActive(false);
                }

                if (Input.GetKey(KeyCode.L))
                {
                    electricity -= 1;
                }    
            }
            

        }
        // Start is called before the first frame update
        void Start()
        {
        targeto = GameObject.Find("questManager");
        questManager = targeto.GetComponent<questManager>();


            electricity = 4000;
            cameraShake = Camera.main.GetComponent<CameraFollowWithShake>();

            targetObject = GameObject.Find("dectectCircle");
            detecterScript = targetObject.GetComponent<detector>();
            health = 100;
            mental = 100;
            mask = 100;
            gm = detectCircle.GetComponent<gameManager>();
            //cameraShake = cam.GetComponent<cameraShake>();
            mentalDecrease = myMentalDecrease();
            maskDecrease = myMaskDecrease();
            isMentalDecrease = false;
            isMaskDecrease = false;
            isHealth = true;
            targetObject = GameObject.Find("inventoryManager");
            final = targetObject.GetComponent<invenFinal>();



        //tttargetObject = GameObject.Find("char");
        //dot = tttargetObject.GetComponent<dotDamage>();
    }

    public void cameraShaking()
    { cameraShake.TriggerShake(0.5f); }


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "bat")
            {
                //dot.batHit = false;
                //Debug.Log("박쥐충돌, 건강 10차감");
                cameraShake.TriggerShake(0.5f);
                health -= 10;
                //cameraShake.TriggerShake();
                Destroy(col.gameObject);
                //gm.minus();
                detecterScript.ratCount -= 1;
            }

        }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("방사능 구역 안쪽에 있");
        if (other.gameObject.tag == "rad")
        {
            
        }
            
    }

    public void summon()
    {
        for(int i = 0; i<10; i++)
        {
            Instantiate(roach, new Vector2(transform.position.x, transform.position.y - 2), Quaternion.identity);
        }
        
    }

        private void OnCollisionStay2D(Collision2D col)
        {
        if (col.gameObject.tag == "sink" && Input.GetKey(KeyCode.Space))
            {

                sink.SetActive(true);
            }

        if (col.gameObject.tag == "lv0" && Input.GetKey(KeyCode.Space))
        {

            lv0.SetActive(true);
        }
        if (col.gameObject.tag == "lv1" && Input.GetKey(KeyCode.Space))
        {

            lv1.SetActive(true);
        }
        if (col.gameObject.tag == "lv2" && Input.GetKey(KeyCode.Space))
        {

            lv2.SetActive(true);
        }
        if(col.gameObject.tag == "river" && Input.GetKey(KeyCode.Space))
        {
            final.count[26] += 1;
        }

    }

        

        void walkingCheck()
        {
            if (walking == true)
            { anim.SetBool("isMoving", true); }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }

        void collectAnim()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("isCollecting", false);
                frontMask.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isCollecting", true);
                frontMask.SetActive(false);
            }

        }




    void invenCheck()
    {
        if (Input.GetKey(KeyCode.E))
        {
            inven.SetActive(true);

        }
        if((Input.GetKeyUp(KeyCode.E)))
        {
            inven.SetActive(false);
        }    
    }

    void CheckIfPlayerIsHome()
    {
        if (playerTransform != null)
        {
            Vector2 playerPosition = playerTransform.position;

            // 플레이어가 지정된 범위 내에 있는지 확인
            if (playerPosition.x >= bottomLeft.x && playerPosition.x <= topRight.x &&
                playerPosition.y >= bottomLeft.y && playerPosition.y <= topRight.y)
            {
                isHome = true;
                health = 100;
                electricity = 4000;
                cam1.SetActive(false);
                cam2.SetActive(false);
                totalMask.SetActive(false);
                detectCircle.SetActive(false);
                maskGo = false;
            }
            else
            {
                isHome = false;
                
                //totalMask.SetActive(true);
                detectCircle.SetActive(true);
                maskGo = true;
            }
            //Debug.Log("isHome: " + isHome);
        }
    }

    void changeIsHome()
    {
        if (!isHome)
        {
            if(maskFalling == null)
            {
                maskFalling = myMaskFalling();
                StartCoroutine(maskFalling);
            }
        }
        else
        {
            if(maskFalling != null)
            {
                StopCoroutine(maskFalling);
                maskFalling = null;
            }

        }
    }

    IEnumerator myMaskFalling()
    {
        while (true)
        {
            mask -= 2f;
            yield return new WaitForSecondsRealtime(1f);
        }
    }

        // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsHome();
        ferry();
        statusCheck();   
        walkingCheck();
        move();
        lights();
        diagCheck();
        leftCheck();
        walkingCheck();
        collectAnim();
        invenCheck();
        changeIsHome();
        masks();
    }

    void using0()
    {
        final.count[0] -= 1;
        health = Math.Min(health + 10, 100);
    }
    void using1()
    {
        final.count[1] -= 1;
        health = Math.Min(health + 5, 100);
    }
    void using2()
    {
        final.count[2] -= 1;
        health = Math.Min(health + 3, 100);
        mental = Math.Min(mental + 5, 100);
    }
    void using3()
    {
        final.count[3] -= 1;
        health = Math.Min(health + 10, 100);
        mental = Math.Max(mental - 5, 0);
    }
    void using4()
    {
        final.count[4] -= 1;
        health = Math.Min(health + 15, 100);
    }
    void using5()
    {
        final.count[5] -= 1;
        health = Math.Max(health - 10, 0);
        mental = Math.Min(mental + 15, 100);
    }
    void using6()
    {
        final.count[6] -= 1;
        health = Math.Min(health + 10, 100);
    }
    void using7()
    {
        final.count[7] -= 1;
        health = Math.Min(health + 70, 100);
    }
    void using8()
    {
        final.count[8] -= 1;
        health = Math.Min(health + 70, 100);
        mental = Math.Min(mental + 30, 100);
    }
    void using9()
    {
        final.count[9] -= 1;
        health = Math.Min(health + 100, 100);
        mental = Math.Min(mental + 100, 100);
    }
    void using17()
    {
        final.count[17] -= 1;
        health = Math.Min(health + 30, 100);
    }

}
