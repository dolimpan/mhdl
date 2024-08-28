using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashCan : MonoBehaviour
{

    public GameObject targetObject; // Detecter ��ũ��Ʈ�� �ִ� ������Ʈ
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
    void collectItem() // �������뿡�� �������� ������ �ϳ��� ����
    {
        int randValue = Random.Range(0, totalProbability);
        int cumulativeProbability = 0;

        foreach (var item in itemDict)
        {
            cumulativeProbability += item.Value;
            if (randValue < cumulativeProbability)
            {
                Debug.Log(final.name[item.Key] + " �����Ϸ�");
                final.count[item.Key] ++;
                break;
            }
        }
    }
    void bugBite() // 20%Ȯ���� ������ ����
    {
        if (Random.Range(1, 101) < 20)
        {
            Debug.Log("20% Ȯ���� ��������");
        }
    }
    void Update()
    {
        collectCheck();
        spaceCheck();
    }
    void collectCheck() // ��������� space�� ������ ������ ����
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
    IEnumerator myCollect() // 5�ʵ� ������Ϸ��� �߰� attempts��ŭ �������� ����
    {
        Debug.Log("������ ������");
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
    void spaceCheck() // ��������� space�� ���� �������ߴ�
    {
        if (touch && isCollecting)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(collecting);
                isCollecting = false;
                Debug.Log("������ �ߴ�");
            }
        }
    }
    void OnCollisionStay2D(Collision2D col) // ��������� touch = true
    {
        if (col.gameObject.tag == "Player")
        {
            touch = true;
        }
    }
    void OnCollisionExit2D(Collision2D col) // �������� touch = false // space�� ������ �־��ٸ� �������ߴ�
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
                    Debug.Log("������ �ߴ�");
                }
            }
        }
    }
}
