using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // 미사일 프리팹
    public GameObject missilePrefab;

    // 미사일이 발사되는 순간의 속도
    public float launchSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Shoot();
    }

    void Rotation()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0, 0, 5);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles -= new Vector3(0, 0, 5);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject missile = Instantiate(missilePrefab, transform.GetChild(0).position, transform.rotation);
           

            // 미사일로부터 리지드바디 2D 컴포넌트 가져옴
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

            // 미사일을 전방으로 발사
            rb.AddForce(transform.up * launchSpeed, ForceMode2D.Impulse);
        }
    }
}
