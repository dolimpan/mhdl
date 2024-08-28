using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gen1 : MonoBehaviour
{
    public Button upButton0;
    public Button upButton1;

    public Button upButton2;
    public Button upButton3;

    public Button button4;
    public Button button5;
    public GameObject targetObject; // Detecter 스크립트가 있는 오브젝트
    public GameObject targetObject2;
    private player ps;
    private invenFinal iv;
    public int currentLevel = 0;
    public GameObject lv0;
    public GameObject lv1;
    public GameObject lv2;
    public bool okay = false;
    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find("char");
        ps = targetObject.GetComponent<player>();

        targetObject2 = GameObject.Find("inventoryManager");
        iv = targetObject2.GetComponent<invenFinal>();
    }

    public void energy()
    {
        if (currentLevel == 0)
        {
            if(ps.mental>= 10)
            {
                ps.mental -= 10;
                ps.mask += 10;
            }
            
        }
        if (currentLevel == 1)
        {
            if (ps.mental >= 10)
            {
                ps.mental -= 10;
                ps.mask += 15;
            }
        }
        if (currentLevel == 2)
        {
            if (ps.mental >= 10)
            {
                ps.mental -= 10;
                ps.mask += 30;
            }
        }

    }

    public void upgrade()
    {
        if(currentLevel == 0)
        {
            Debug.Log(iv.count[26]);
            if((iv.count[26] >= 5) && (iv.count[23] >= 5) && (iv.count[6] >= 1))
            {
                okay = true;
            }
            iv.count[26] -= 5;
            iv.count[23] -= 5;
            iv.count[6] -= 1;
            currentLevel = 1;
            lv0.SetActive(false);
            lv1.SetActive(true);
            okay = false;
        }
        if(currentLevel == 1)
        {
            if((iv.count[29] >=5)&&(iv.count[28] >= 5) &&(iv.count[24]>=5))
            {
                okay = true;
                iv.count[29] -= 5;
                iv.count[28] -= 5;
                iv.count[24] -= 5;
                currentLevel = 2;
                lv1.SetActive(false);
                lv2.SetActive(true);
                okay = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(ps.mental <10)
        {
            upButton0.interactable = false;
            upButton2.interactable = false;
            button4.interactable = false;
        }
        else
        {
            upButton0.interactable = true;
            upButton2.interactable = true;
            button4.interactable = true;
        }
        if(okay == false)
        {
            upButton1.interactable = false;
            upButton3.interactable = false;
            button5.interactable = false;
        }
        else
        {
            upButton1.interactable = true;
            upButton3.interactable = true;
            button5.interactable = true;
        }
    }
}
