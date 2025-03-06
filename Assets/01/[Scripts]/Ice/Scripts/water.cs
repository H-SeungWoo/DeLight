using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public GameObject missilePrefab;

    public float launchSpeed = 10.0f;

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

        if (collision.tag == "water")
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 10000);
            if(hit)
            {
                Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 0.3f);
                Vector3 normalVector = hit.point.normalized;
               // normalVector = normalVector.normalized;
                Debug.Log("collisionEnter" + transform.position);
                Vector3 incomingVector = this.transform.forward;
                incomingVector = incomingVector.normalized;
                Debug.Log("incomingVector" + incomingVector);
                Debug.Log("normalVector" + normalVector);
                // 충돌한 면의 법선 벡터를 구해낸다.
                //Vector3 normalVector = collision.contacts[0].normal;
                Vector3 newdir = Refract(1.0f, 1.33f, normalVector, incomingVector);
                Debug.Log("newdir: "+ newdir);
                //GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
                Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
                rb.AddForce(newdir * launchSpeed *10f);

                // 미사일로부터 리지드바디 2D 컴포넌트 가져옴
                //Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();

                // 미사일을 전방으로 발사
                // rb.AddForce(newdir * launchSpeed, ForceMode2D.Impulse);
            }
        }
    }
   



    public static Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident)
    {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        return (RI1 / RI2 * Vector3.Cross(surfNorm, Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident) * (RI1 / RI2 * RI1 / RI2), Vector3.Cross(surfNorm, incident)))).normalized;
    }
}
