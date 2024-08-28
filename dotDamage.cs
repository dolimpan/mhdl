using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dotDamage : MonoBehaviour
{
    public int guage = 0;
    public bool catHit = false;
    public GameObject wholeSet;
    public bool batHit = false;
    public GameObject targetObject; 
    private player ps;
    //ps.health
    public int currentClick = -1;
    public List<string> damageName;
    public List<int> hpDamagePerSec;
    public List<float> speedDamagePerSec;
    public List<string> dotDamLore;
    public TextMeshProUGUI effectNameT;
    public TextMeshProUGUI effectLoreT;
    public IEnumerator triggerEvents;
    public bool isTriggerEvents;

    private Vector2 lastPosition;
    //private float stationaryTime = 0.0f;

    IEnumerator myTriggerEvents()
    {
        isTriggerEvents = true;
        for (int i = 0; i < 10; i++)
        {
            ps.health -= hpDamagePerSec[currentClick];

            Debug.Log("아야!아야!");
            yield return new WaitForSecondsRealtime(1.0f); // 1초 대기
        }
        ps.speed = 3.0f; // 상태 이상 효과 종료 후 속도를 원래대로 복원
        wholeSet.SetActive(false);
        isTriggerEvents = false;
    }

    void mySort()
    {
        damageName = new List<string> { "저체온증", "방사능 중독", "감기", "건강함", "포자", "할큄", "정신이상" };
        hpDamagePerSec = new List<int> { 2, 3, 0, 0, 1, 2, 1 };
        speedDamagePerSec = new List<float> { 1.0f, 0.9f, 0.9f, 1.2f, 1.0f, 0.8f, 1.0f };
        dotDamLore = new List<string> { "저체 설명", "방사 설명", "감기 설명", "건강함 설명", "포자 설명", "할큄 설명", "정신이상 설명" };
    }


    public void dotDamageOuch()
    {
        if (isTriggerEvents)
        {
            StopCoroutine(triggerEvents);
            isTriggerEvents = false;
        }
        effectNameT.text = damageName[currentClick];
        effectLoreT.text = dotDamLore[currentClick];
        ps.speed *= speedDamagePerSec[currentClick];
        if(ps.speed >= 3.6f)
        {
            ps.speed = 3.6f;
        }

        if(ps.speed <= 2.7f)
        {
            ps.speed = 2.7f;
        }
        Debug.Log("느려진다아");
        triggerEvents = myTriggerEvents();
        StartCoroutine(triggerEvents);
    }
    public void SetCurrentClick(int value)
    {
        currentClick = value;
        dotDamageOuch();
    }

    public void zero() { SetCurrentClick(0); }
    public void one() { SetCurrentClick(1); }
    public void two() { SetCurrentClick(2); }
    public void three() { SetCurrentClick(3); }
    public void four() { SetCurrentClick(4); }
    public void five() { SetCurrentClick(5); }
    public void six() { SetCurrentClick(6); }

    public void whatOuch()
    {
        if (guage >= 1000f)
        {
            SetCurrentClick(0); // 저체온증
        }
        else if (ps.mask == 0)
        {
            SetCurrentClick(1); // 방사능 중독
        }
        else if (ps.health <= 50)
        {
            SetCurrentClick(2); // 감기
        }
        else if (ps.health == 100)
        {
            SetCurrentClick(3); // 건강함
        }
        else if (batHit == true)   // 돌연변이 박쥐한테 공격당했을 때 50% 확률 )
        {
            int a = Random.Range(0, 2);
            if(a == 0)
            {
                SetCurrentClick(4);
            }
            batHit = false;
            // 포자중독
            
        }
        else if (catHit == true)
        {

            int a = Random.Range(0, 3);
            if (a > 0)
            {
                SetCurrentClick(5);
            }
            catHit = false; // 할큄
        }
        else if (ps.mental <= 40)
        {
            SetCurrentClick(6); // 정신이상
        }
        /*else if (// 쓰레기통 뒤질 때 20% 확률 
        {
            SetCurrentClick(7); // 벌레물림
        }*/
    }



    // Start is called before the first frame update
    void Start()
    {
        mySort();
        isTriggerEvents = false;

        targetObject = GameObject.Find("char");
        ps = targetObject.GetComponent<player>();
    }

    void standCheck()
    {
        if(!(Input.anyKey))
        {
            guage += 1;
        }
        if(Input.anyKey)
        {
            guage = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        standCheck();
        whatOuch();
    }
}