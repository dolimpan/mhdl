using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catManager : MonoBehaviour
{
    public GameObject targeto;
    private questManager questManager;

    public GameObject cat;
    public int catCount;
    // Start is called before the first frame update
    void Start()
    {
        targeto = GameObject.Find("questManager");
        questManager = targeto.GetComponent<questManager>();
        catCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(questManager.complete[1] == 1)
        {
            gogo();
        }
        
    }

    void gogo()
    {
        while(!(catCount == 10))
            {
                float randomX = UnityEngine.Random.Range(-110, 15);
                float randomY = UnityEngine.Random.Range(-70, 25);
                Instantiate(cat, new Vector2(randomX, randomY), Quaternion.identity);
                catCount++;
                Debug.Log("1");
            }
    }
}

