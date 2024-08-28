using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    public GameObject targetObject; // Detecter 스크립트가 있는 오브젝트
    private invenFinal final;
    public Dictionary<int, string> itemDictionary;
    public float delayTime;

    public bool touch;
    public bool isCollecting = false;
    public IEnumerator collecting;
    void Start()
    {
        targetObject = GameObject.Find("inventoryManager");
        final = targetObject.GetComponent<invenFinal>();

        touch = false;
        collecting = myCollect();
        delayTime = 3f;
    }
    void Update()
    {
        collectCheck();
        spaceCheck();
    }
    void collectCheck() // 닿아있을때 space를 누르면 수집시작
    {
        if (touch && !isCollecting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                collecting = myCollect();
                StartCoroutine(collecting);
                isCollecting = true;
            }
        }
    }
    IEnumerator myCollect() // 3초뒤 수집완료라고 뜸
    {
        Debug.Log(gameObject.tag + " 수집을 시작함");
        yield return new WaitForSecondsRealtime(delayTime);
        isCollecting = false;
        if (gameObject.tag == "stone")
        {
            Debug.Log("돌을 캠");
            final.count[28]++;
        }
        else if(gameObject.tag == "soil")
        {
            Debug.Log("흙을 캠");
            final.count[32]++;
        }
        else if (gameObject.tag == "tree") {
            Debug.Log("나무를 캠");
            final.count[27]++;
            if (Random.Range(1, 101) <= 50)
            {
                Debug.Log("방사능 사과를 수집함");
                final.count[2]++;
            }
        }
        else if (gameObject.tag == "iron")
        {
            Debug.Log("철을 캠");
            final.count[29]++;
        }
        else if (gameObject.tag == "titanum")
        {
            Debug.Log("티타늄을 캠");
            final.count[31]++;
        }
        else if(gameObject.tag == "cu")
        {
            Debug.Log("구리를 캠");
            final.count[30]++;
        }
        /*if (Random.Range(0, 100) < 10) //10%확률로 2단계 재료 수집
        {
            if (Random.Range(0, 100) < 50)
            {
                Debug.Log("철 수집완료");
                final.count[28]++;
            }
            else
            {
                Debug.Log("구리 수집완료");
                final.count[29]++;
            }
        }*/
    }
    void spaceCheck() // 닿아있을때 space를 때면 수집중단
    {
        if (touch && isCollecting)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(collecting);
                isCollecting = false;
                Debug.Log(gameObject.tag + "수집을 중단함");
            }
        }
    }
    void OnCollisionStay2D(Collision2D col) // 닿아있을때 touch = true
    {
        if (col.gameObject.tag == "Player")
        {
            touch = true;
        }
    }
    void OnCollisionExit2D(Collision2D col) // 떨어지면 touch = false // space를 누르고 있었다면 수집중단
    {
        if (col.gameObject.tag == "Player")
        {
            touch = false;
            if (isCollecting)
            {
                StopCoroutine(collecting);
                isCollecting = false;
                if (Input.GetKey(KeyCode.Space))
                {
                    Debug.Log(gameObject.tag + "수집을 중단함");
                }
            }
        }
    }


}
