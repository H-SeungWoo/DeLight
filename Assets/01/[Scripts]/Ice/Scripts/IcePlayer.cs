using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcePlayer : MonoBehaviour
{
    public static IcePlayer instance;
    Rigidbody2D rb;
    public bool isspace = false;
    public bool isplaying = false;
    public GameObject Slight;
    public int speed = 20;
    public GameObject Gameover;
    public GameObject ClearCan;
    public GameObject PauseCanvas;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
         rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isplaying)
        {
            Rotation(Slight);
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Slight")
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            isspace = false;
            isplaying = false;
            this.transform.parent = collision.transform;
            this.transform.SetSiblingIndex(0);
            //StraightOfLight = collision.gameObject.transform.parent.gameObject;
            this.transform.position = new Vector3(collision.transform.position.x , collision.transform.position.y+1.2f, this.transform.position.z);
            rb.velocity = Vector2.zero;
            SoundManager.instance.SFXPlay("Ride", SoundManager.instance.ride_sound,0.5f);


        }
        else if (collision.tag == "bulb")
        {
            if (GameObject.FindGameObjectWithTag("dark"))
            {
                GameObject dark = GameObject.FindGameObjectWithTag("dark");
                dark.SetActive(false);

                SoundManager.instance.SFXPlay("touchblub", SoundManager.instance.touchbulb_sound,0.8f);

            }
        }

        else if (collision.tag == "dark")
        {
            Gameover.SetActive(true);
            this.gameObject.SetActive(false);
            SoundManager.instance.SFXPlay("Die", SoundManager.instance.die_sound,1f);
        }
        else if (collision.tag == "clear")
        {
            if (SceneManager.GetActiveScene().name == "Ice")
            {
                mapManager.currentMap = 2;
            }
            ClearCan.SetActive(true);
            this.gameObject.SetActive(false);
            SoundManager.instance.SFXPlay("Clear", SoundManager.instance.clear_sound, 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Slight" && !isspace)
        {
            //this.transform.position = new Vector3(other.transform.position.x , other.transform.position.y+1, this.transform.position.z);
            isplaying = false;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Slight")
        {
            this.transform.parent = collision.transform.parent.parent.parent;
            collision.transform.parent.eulerAngles = new Vector3(0, 0, 0);
        }

        if (collision.tag == "wall")
        {
            Gameover.SetActive(true);
            this.gameObject.SetActive(false);
            SoundManager.instance.SFXPlay("Die", SoundManager.instance.die_sound,1f);
        }
    }

    void Rotation(GameObject StraightOfLight)
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0, 0, 5);
            StraightOfLight.transform.eulerAngles += new Vector3(0, 0, 5);

            SoundManager.instance.SFXPlay("Rotate", SoundManager.instance.rotate_sound,0.4f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles -= new Vector3(0, 0, 5);
            StraightOfLight.transform.eulerAngles -= new Vector3(0, 0, 5);

            SoundManager.instance.SFXPlay("Rotate", SoundManager.instance.rotate_sound,0.4f);
        }*/
        if (StraightOfLight.transform.rotation.z <= 0.7)
        {
            Debug.Log(this.transform.rotation.z);
            if (Input.GetKey(KeyCode.A))
            {
                //transform.eulerAngles += new Vector3(0, 0, 0.5f);
                StraightOfLight.transform.eulerAngles += new Vector3(0, 0, 0.5f);
                if (SoundManager.instance.rotate_sound_timer > 40)
                {
                    SoundManager.instance.SFXPlay("Rotate", SoundManager.instance.rotate_sound, 0.3f);
                    SoundManager.instance.rotate_sound_timer = 0;

                }
            }
        }
        if (StraightOfLight.transform.rotation.z >= -0.7)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.eulerAngles -= new Vector3(0, 0, 0.5f);
                StraightOfLight.transform.eulerAngles -= new Vector3(0, 0, 0.5f);
                if (SoundManager.instance.rotate_sound_timer > 40)
                {
                    SoundManager.instance.SFXPlay("Rotate", SoundManager.instance.rotate_sound, 0.3f);
                    SoundManager.instance.rotate_sound_timer = 0;

                }
            }
        }
        SoundManager.instance.rotate_sound_timer++;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isspace = true;
            isplaying = true;
            // 미사일을 전방으로 발사
            rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
            SoundManager.instance.SFXPlay("Shoot", SoundManager.instance.shoot_sound,0.5f);

        }
    }

    public void Pause()
    {

        PauseCanvas.SetActive(true);
            if (Time.timeScale == 1.0F)
                Time.timeScale = 0.02F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;


    }
}
