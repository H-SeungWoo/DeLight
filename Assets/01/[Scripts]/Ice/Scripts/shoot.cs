using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // �̻��� ������
    public GameObject missilePrefab;

    // �̻����� �߻�Ǵ� ������ �ӵ�
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
           

            // �̻��Ϸκ��� ������ٵ� 2D ������Ʈ ������
            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

            // �̻����� �������� �߻�
            rb.AddForce(transform.up * launchSpeed, ForceMode2D.Impulse);
        }
    }
}
