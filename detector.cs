using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public GameObject targeto;
    private questManager questManager;

    public int ratCount = 0;
    public GameObject rat;
    public Transform pP;

    // Start is called before the first frame update
    void Start()
    {
        targeto = GameObject.Find("questManager");
        questManager = targeto.GetComponent<questManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(questManager.complete[0] == 1)
        {
            ratSummon();
        }
    }

    void ratSummon()
    {
        if(ratCount<6)

        {
            for(int i = ratCount; i<5; i++)
            {
                float playerX = pP.position.x;
                float playerY = pP.position.y;
                float randomX = UnityEngine.Random.Range(-25, 25);
                float randomY = UnityEngine.Random.Range(-25, 25);
                Vector2 a = new Vector2(playerX + randomX, playerY + randomY);
                Instantiate(rat, a, Quaternion.identity);
                ratCount++;
            }

        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "rat")
        { ratCount++; }

        
    }



}
