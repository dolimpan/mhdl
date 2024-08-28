using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class recipeManager : MonoBehaviour
{
    public GameObject targetObject; // Detecter ?????????? ???? ????????
    private invenFinal final;
    public GameObject image;

    void Start()
    {
        targetObject = GameObject.Find("inventoryManager");
        final = targetObject.GetComponent<invenFinal>();
    }

    public void makeMud()//???? ???? ????
    {
        if (final.count[32] >= 1 && final.count[6] >= 1) 
        {
            final.count[24] += 1;
            final.count[32] -= 1;
            final.count[6] -= 1;


            // create mud   mud index = 24
        }
        else
        {
            Debug.Log("cannot create mud");
        }
    }
    public void makeTitanium()//?????? ???? ????
    {
        if (final.count[29] >= 1 && final.count[32] >= 1)
        {
            final.count[31] += 1;
            final.count[29] -= 1;
            final.count[32] -= 1;

            // create titanium index 31
        }
        else
        {
            Debug.Log("cannot create titanium");
                                       
        }
    }
    public void makeGn() //Gn ???? ????
    {
        if (final.count[31] >= 1 && final.count[28] >= 1 && final.count[26] >= 1 && final.count[24] >= 1 && final.count[29] >= 1)
        {
            final.count[33] += 1;
            final.count[31] -= 1;
            final.count[28] -= 1;
            final.count[26] -= 1;
            final.count[24] -= 1;
            final.count[29] -= 1;
            Debug.Log("The end");
            victory();
        }
        else
        {
            Debug.Log("cannot create Gn");
        }
    }
    public void victory()
    {
        image.SetActive(true);
    }
}
