using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashCan : MonoBehaviour
{

    public GameObject targetObject; // Detecter 스크립트가 있는 오브젝트
    private invenFinal final;

    bool touch; 
    bool isCollecting = false;
    public IEnumerator collecting; 
    private Dictionary<int, int> itemDict;
    private int totalProbability;

    void Start()
    {

        targetObject  = GameObject.Find("inventoryManager");
        final = targetObject.GetComponent<invenFinal>();

        touch = false;
        collecting = myCollect();
        itemDict = new Dictionary<int, int>()
        {
            {29, 20}, {1, 15}, {0, 20}, {3, 10}, {5, 6}, {6, 10}, {13, 4}, {14, 3},
            {15, 2}, {16, 2}, {17, 4}, {18, 2}, {19, 3}, {20, 2}, {21, 1}, {22, 1 }
        };
        totalProbability = 0;
        foreach (var probability in itemDict.Values)
        {
            totalProbability += probability;
        }
    }
    void collectItem() // 쓰레기통에서 랜덤으로 아이템 하나를 뽑음
    {
        int randValue = Random.Range(0, totalProbability);
        int cumulativeProbability = 0;

        foreach (var item in itemDict)
        {
            cumulativeProbability += item.Value;
            if (randValue < cumulativeProbability)
            {
                Debug.Log(final.name[item.Key] + " 수집완료");
                final.count[item.Key] ++;
                break;
            }
        }
    }
    void bugBite() // 20%확률로 벌레에 물림
    {
        if (Random.Range(1, 101) < 20)
        {
            Debug.Log("20% 확률로 벌레물림");
        }
    }
    void Update()
    {
        collectCheck();
        spaceCheck();
    }
    void collectCheck() // 닿아있을때 space를 누르면 뒤지기 시작
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
    IEnumerator myCollect() // 5초뒤 뒤지기완료라고 뜨고 attempts만큼 아이템을 얻음
    {
        Debug.Log("뒤지기 시작함");
        yield return new WaitForSecondsRealtime(5.0f);
        isCollecting = false;
        int attempts = Random.Range(1, 101) / 25;
        for (int i = 0; i < attempts; i++)
        {
            collectItem();
        }
        bugBite();
        Destroy(gameObject);
    }
    void spaceCheck() // 닿아있을때 space를 때면 뒤지기중단
    {
        if (touch && isCollecting)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(collecting);
                isCollecting = false;
                Debug.Log("뒤지기 중단");
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
    void OnCollisionExit2D(Collision2D col) // 떨어지면 touch = false // space를 누르고 있었다면 뒤지기중단
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
                    Debug.Log("뒤지기 중단");
                }
            }
        }
    }
}
