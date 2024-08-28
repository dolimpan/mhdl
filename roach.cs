using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roach : MonoBehaviour
{
    public Animator anim;
    float randomX;
    float randomY;
    
    //public GameObject ch;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        randomX = UnityEngine.Random.Range(-500, 500);
        randomY = UnityEngine.Random.Range(-500, 500);
        Destroy(this, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(randomX, randomY), 30 * Time.deltaTime);
        
    }
}
