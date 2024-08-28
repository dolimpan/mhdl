using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questManager : MonoBehaviour
{
    public GameObject monster;
    public GameObject[] images; // ???? ???? Image ?????????? ?????? ?? ???????? ??????????.
    public int currentIndex = 0; // ???? ???????? -1?? ???????? ???? ?????? ?????? ?? ?? ???? ???????? ?????????? ??
    public GameObject alarmImage;

    public GameObject targetObject; // Detecter ?????????? ???? ????????
    private invenFinal final;


    public GameObject targetObject3; // Detecter ?????????? ???? ????????
    private nggs final3;

    public List<int> complete = new List<int> { 0, 0, 0, 0 };



    void Start()
    {
        currentIndex = 0;

        targetObject = GameObject.Find("inventoryManager");
        final = targetObject.GetComponent<invenFinal>();

        targetObject3 = GameObject.Find("monster");
        //final3 = targetObject3.GetComponent<nggs>();
        // ???? ???????? ????????
        foreach (var image in images)
        {
            image.SetActive(false);
        }
    }

    void Update()
    {
        missionOne();
        missionTwo();
        missionThree();
        missionFour();
        // ESC ???? ?????? ?? ???? ???????? ????
        //questAlarm();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentIndex >= 0 && images[currentIndex].activeSelf)
            {
                images[currentIndex].SetActive(false);
            }
        }
    }

    public void OnButtonClick()
    {
        alarmImage.SetActive(false);

        if (currentIndex == 0)
        {
            images[0].SetActive(true);
        }

        if (currentIndex == 1)
        {

            images[0].SetActive(false);
            images[1].SetActive(true);
        }

        if (currentIndex == 2)
        {

            images[1].SetActive(false);
            images[2].SetActive(true);
        }

        if (currentIndex == 3)
        {

            images[2].SetActive(false);
            images[3].SetActive(true);
        }
    }
    public void missionOne()
    {
        if (final.count[27] >= 1 && final.count[28]>=1)
        {

            complete[0] = 1;
            alarmImage.SetActive(true);
            currentIndex = 1;
        }

    }
    public void missionTwo()
    {
        if (complete[0] == 1)
        {
            if (final.count[29] >= 2 && final.count[0] >= 1)
            {
                complete[1] = 1;
                alarmImage.SetActive(true);
                currentIndex = 2;
            }
        }
    }

    public void missionThree() {

        if (complete[1] == 1)
        {
            if (final.count[26] >= 2)
            {
                complete[2] = 1;
                alarmImage.SetActive(true);
                currentIndex = 3;
                Instantiate(monster, new Vector2(0, 0), Quaternion.identity);
            }
        }
    }

    public void missionFour() { 
        if (complete[2] == 1)
        {
            if (final.count[26] >= 3 && final.count[28] >= 1 && final.count[29]>= 2)
            {
                complete[3] = 1;
                alarmImage.SetActive(true);
            }
        }
    }


    /*public void questAlarm()
    {
        if (//?????? ????)0
        {
            alarmImage.SetActive(true);
        }
    }
    */
}
