using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "wall")
        {
            if(collision.GetComponent<SpriteRenderer>().color == this.GetComponent<SpriteRenderer>().color)
            {
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
