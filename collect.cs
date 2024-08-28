using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    public GameObject targetObject; // Detecter ��ũ��Ʈ�� �ִ� ������Ʈ
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
    void collectCheck() // ��������� space�� ������ ��������
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
    IEnumerator myCollect() // 3�ʵ� �����Ϸ��� ��
    {
        Debug.Log(gameObject.tag + " ������ ������");
        yield return new WaitForSecondsRealtime(delayTime);
        isCollecting = false;
        if (gameObject.tag == "stone")
        {
            Debug.Log("���� ķ");
            final.count[28]++;
        }
        else if(gameObject.tag == "soil")
        {
            Debug.Log("���� ķ");
            final.count[32]++;
        }
        else if (gameObject.tag == "tree") {
            Debug.Log("������ ķ");
            final.count[27]++;
            if (Random.Range(1, 101) <= 50)
            {
                Debug.Log("���� ����� ������");
                final.count[2]++;
            }
        }
        else if (gameObject.tag == "iron")
        {
            Debug.Log("ö�� ķ");
            final.count[29]++;
        }
        else if (gameObject.tag == "titanum")
        {
            Debug.Log("ƼŸ���� ķ");
            final.count[31]++;
        }
        else if(gameObject.tag == "cu")
        {
            Debug.Log("������ ķ");
            final.count[30]++;
        }
        /*if (Random.Range(0, 100) < 10) //10%Ȯ���� 2�ܰ� ��� ����
        {
            if (Random.Range(0, 100) < 50)
            {
                Debug.Log("ö �����Ϸ�");
                final.count[28]++;
            }
            else
            {
                Debug.Log("���� �����Ϸ�");
                final.count[29]++;
            }
        }*/
    }
    void spaceCheck() // ��������� space�� ���� �����ߴ�
    {
        if (touch && isCollecting)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(collecting);
                isCollecting = false;
                Debug.Log(gameObject.tag + "������ �ߴ���");
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
    void OnCollisionExit2D(Collision2D col) // �������� touch = false // space�� ������ �־��ٸ� �����ߴ�
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
                    Debug.Log(gameObject.tag + "������ �ߴ���");
                }
            }
        }
    }


}
