using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
    public int dead = 0;
    GameObject charObject;
    public GameObject targetObject; // Detecter 스크립트가 있는 오브젝트
    private detector detecterScript;
    public GameObject thisObj;
    public Transform pT;
    public float moveSpeed = 5f;
    public int started = 0;
    public Vector3 target;
    void whattodo()
    {
        Vector2 pP = pT.position;
        float playerX = pP.x;
        float playerY = pP.y;

        float monsterX = transform.position.x;
        float monsterY = transform.position.y;


        double tX = Math.Pow(playerX - monsterX, 2);
        double tY = Math.Pow(playerY - monsterY, 2);

        if (Math.Sqrt(tX + tY) <= 25)
        {
            moveSpeed = 1.82f;
            follow();
        }
        else
        {
            moveSpeed = 2f;
            idle();
        }
    }


    void follow()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = pT.position;
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        //Debug.Log("follow");
    }

    Vector3 TargetSet()
    {
        Vector2 pP = pT.position;
        float playerX = pP.x;
        float playerY = pP.y;

        float randomX = UnityEngine.Random.Range(-5, 5);
        float randomY = UnityEngine.Random.Range(-5, 5);
        Vector3 target = new Vector3(playerX + randomX, playerY + randomY);
        return target;
    }

    void idle()
    {
        if (started == 0)
        {
            target = TargetSet();
            started = 1;
            Debug.Log(target);
        }

        Vector3 currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(currentPosition, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(currentPosition, target) < 0.1f)
        {
            started = 0;
        }
        //Debug.Log("idle");
    }

    void Start()
    {
        charObject = GameObject.Find("char");
        pT = charObject.GetComponent<Transform>(); // 캐릭터 위치
        targetObject = GameObject.Find("dectectCircle");
        detecterScript = targetObject.GetComponent<detector>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "light")
        {
            Debug.Log("w");
            Destroy(thisObj);
            detecterScript.ratCount -= 1;
            dead++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        whattodo();
    }
}
