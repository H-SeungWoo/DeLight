using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSkyPlayer : MonoBehaviour
{
    GameObject clickPlayer;
    GameObject[] colorplayer;
    Color[] wallcolor = new Color[4];
    List<GameObject> colPlayer = new List<GameObject>();
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] wall = GameObject.FindGameObjectsWithTag("cellophane");
        for (int i = 0; i < wall.Length; i++)
        {
            wallcolor[i] = wall[i].GetComponent<Image>().color;
        }
        playercheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
            colPlayer = new List<GameObject>();
        if (Input.GetAxisRaw("Horizontal") > 0)
            colPlayer = new List<GameObject>();
        if (Input.GetMouseButton(0))
            playerbutton();


    }

    public void playerbutton()
    {
        // 마우스로 클릭해서 인식 후 대화
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if(hit.transform.gameObject.tag == "Player")
        {
            for (int i = 0; i < colorplayer.Length; i++)
            {
                colorplayer[i].GetComponent<ClearSky.SimplePlayerController>().alive = false;
            }
            hit.transform.gameObject.GetComponent<ClearSky.SimplePlayerController>().alive = true;
        }
        else
        { }

        



        /*for (int i = 0; i < colorplayer.Length; i++)
        {
            colorplayer[i].GetComponent<ClearSky.SimplePlayerController>().alive = false;
        }
        clickPlayer = EventSystem.current.currentSelectedGameObject;
        clickPlayer.GetComponent<ClearSky.SimplePlayerController>().alive = true;
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cellophane")
        {
            if (collision.GetComponent<Image>().color == this.GetComponent<SpriteRenderer>().color)
            {
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
        if (collision.tag == "button")
        {
            GameObject[] wall = GameObject.FindGameObjectsWithTag("cellophane");
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<Image>().color = collision.GetComponent<Image>().color;
                SoundManager.instance.SFXPlay("Switch", SoundManager.instance.switch_sound, 1f);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "cellophane")
        {
            if (collision.GetComponent<Image>().color == this.GetComponent<SpriteRenderer>().color)
            {
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }

    public void playercheck()
    {
        colorplayer = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < colorplayer.Length; i++)
        {
            colorplayer[i].GetComponent<ClearSky.SimplePlayerController>().alive = false;
        }
    }

    public void colorcheck()
    {
            if (colorplayer[0].GetComponent<SpriteRenderer>().color == colorplayer[1].GetComponent<SpriteRenderer>().color )
            {
                Debug.Log("g");
                colorplayer[0].gameObject.SetActive(false);
            }
            else if (colorplayer[0].GetComponent<SpriteRenderer>().color == colorplayer[2].GetComponent<SpriteRenderer>().color)
            {
                Debug.Log("gd");
                colorplayer[0].gameObject.SetActive(false);
            }
            else if (colorplayer[1].GetComponent<SpriteRenderer>().color == colorplayer[2].GetComponent<SpriteRenderer>().color)
            {
                Debug.Log("gda");
                colorplayer[1].gameObject.SetActive(false);
            }

        playercheck();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "cellophane")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (collision.tag == "button")
        {
            GameObject[] wall = GameObject.FindGameObjectsWithTag("cellophane");
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<Image>().color = wallcolor[i];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.transform.tag == "Player")
            if (!colPlayer.Contains(collision.transform.gameObject))
                colPlayer.Add(collision.transform.gameObject);
        
        if(colPlayer.Count >= 2)
        {
            GameObject skyplayer = GameObject.Find("Canvas").transform.Find("Player").gameObject;
            skyplayer.SetActive(true);
            skyplayer.transform.position = this.transform.position;
            for (int i = 0; i < 3; i++)
            {
                colorplayer[i].gameObject.SetActive(false); 
            }
        }*/
        if (collision.transform.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color + collision.transform.GetComponent<SpriteRenderer>().color;
            if (!this.GetComponent<ClearSky.SimplePlayerController>().alive)
                this.gameObject.SetActive(false);
        }
    }
}
