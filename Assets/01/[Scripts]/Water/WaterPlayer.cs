using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterPlayer : MonoBehaviour
{
    public float launchSpeed = 12f;
    public float movementSpeed = 0.2f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb;
    public bool isWater;
    public GameObject GameoverCanvas;
    public GameObject ClearCan;
    public GameObject PauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.right * launchSpeed *7f);
        SoundManager.instance.sound_timer_3 = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWater)
        {
            float movex = 0f;
            float movey = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                movex += 0.01f;
                movey += 0.04f;
                if (SoundManager.instance.run_sound_timer > 30)
                {
                    SoundManager.instance.SFXPlay("run", SoundManager.instance.run_sound, 0.3f);
                    SoundManager.instance.run_sound_timer = 0;

                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                movex += 0.01f;
                movey -= 0.04f;
                if (SoundManager.instance.run_sound_timer > 30)
                {
                    SoundManager.instance.SFXPlay("run", SoundManager.instance.run_sound, 0.3f);
                    SoundManager.instance.run_sound_timer = 0;

                }
            }
            SoundManager.instance.run_sound_timer++;
            SoundManager.instance.sound_timer_3++;
            transform.Translate(new Vector3(movex, movey, 0f) * movementSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            GameoverCanvas.SetActive(true);
            this.gameObject.SetActive(false);
            SoundManager.instance.SFXPlay("Die", SoundManager.instance.die_sound, 1f);
        }
        else if (collision.gameObject.tag == "clear")
        {
            if (SceneManager.GetActiveScene().name == "Water")
            {
                mapManager.currentMap = 4;
            }
            SoundManager.instance.SFXPlay("Clear", SoundManager.instance.clear_sound, 1f);
            ClearCan.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "water")
        {
            isWater = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 10000);
            if (hit)
            {
                Vector3 normalVector = hit.point.normalized;
                Vector3 incomingVector = this.transform.forward;
                incomingVector = incomingVector.normalized;
                // Vector3 newdir = Refract(1.0f, 1.33f, normalVector, incomingVector); water == 1.33 but very small change .... so 
                Vector3 newdir = Refract(1.0f, 2.0f, normalVector, incomingVector);
                rb.velocity = Vector2.zero;
                rb.AddForce(newdir * launchSpeed * 7f);

            }
            if (SoundManager.instance.sound_timer_3 > 50)
            {
                SoundManager.instance.SFXPlay("Water", SoundManager.instance.water_sound, 1f);
                SoundManager.instance.sound_timer_3 = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "water")
        {
            isWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "water")
        {
            isWater = false;
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
        rb.velocity = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector3.right * launchSpeed * 3.5f);
    }


    public static Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident)
    {
        surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
        incident.Normalize();

        return (RI1 / RI2 * Vector3.Cross(surfNorm, Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident) * (RI1 / RI2 * RI1 / RI2), Vector3.Cross(surfNorm, incident)))).normalized;
    }

    public void Pause()
    {
        
            PauseCanvas.SetActive(true);
            if (Time.timeScale == 1.0F)
                Time.timeScale = 0.02F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        
    }
}
