using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anwlro : MonoBehaviour
{
    public GameObject missilePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DispersionOfLight()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dispersion")
        {

            for (int i = 0; i <7; i++)
            {
                Transform spawnLocator = this.transform;
                float angle = Random.Range(-1f, 1f) * 3;
                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * spawnLocator.forward;
                GameObject bullet = Instantiate(missilePrefab, transform.position + new Vector3(4, angle, 0), transform.rotation);
                if (i == 0)
                    bullet.GetComponent<SpriteRenderer>().color = Color.red;
                else if(i == 1)
                    bullet.GetComponent<SpriteRenderer>().color = new Color(255/255f, 156/255f, 0, 255/255f);
                else if (i == 2)
                    bullet.GetComponent<SpriteRenderer>().color = Color.yellow;
                else if (i == 3)
                    bullet.GetComponent<SpriteRenderer>().color = Color.green;
                else if (i == 4)
                    bullet.GetComponent<SpriteRenderer>().color = Color.blue;
                else if (i == 5)
                    bullet.GetComponent<SpriteRenderer>().color = new Color(0/255, 0/255, 111/255f, 255/255);
                else if (i == 6)
                    bullet.GetComponent<SpriteRenderer>().color = new Color(128/255f, 0, 128/255f,255/255);
                dir = dir.normalized;
                // bullet.transform.position = this.transform.position;
                Debug.Log(dir);
                // 총알 쏘기
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // 미사일을 전방으로 발사
                rb.AddForce(transform.right * 10, ForceMode2D.Impulse);
            }
            this.gameObject.SetActive(false);
        }
    }

    
}
