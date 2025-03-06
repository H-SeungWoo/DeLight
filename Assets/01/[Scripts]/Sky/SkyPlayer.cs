using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyPlayer : MonoBehaviour
{
    public GameObject Player;
    Color[] wallcolor = new Color[4];
    public GameObject positionR;
    public GameObject positionG;
    public GameObject positionB;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] wall = GameObject.FindGameObjectsWithTag("cellophane");
        for (int i = 0; i < wall.Length; i++)
        {
            wallcolor[i] = wall[i].GetComponent<Image>().color;
        }
        positionR = GameObject.Find("Canvas").transform.Find("positionR").gameObject;
        positionG = GameObject.Find("Canvas").transform.Find("positionG").gameObject;
        positionB = GameObject.Find("Canvas").transform.Find("positionB").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dispersion")
        {
            SoundManager.instance.SFXPlay("Wall", SoundManager.instance.wall_sound, 1f);
            collision.gameObject.transform.parent.gameObject.SetActive(false);
            
            GameObject RedPlayer = Instantiate(Player, positionR.transform.position + new Vector3(0,0,-1f), transform.rotation);
            GameObject BluePlayer = Instantiate(Player, positionB.transform.position + new Vector3(0, 0, -1f), transform.rotation);
            GameObject GreenPlayer = Instantiate(Player, positionG.transform.position + new Vector3(0, 0, -1f), transform.rotation);
            RedPlayer.GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
            BluePlayer.GetComponent<SpriteRenderer>().color = new Color(0,0 , 1, 1);
            GreenPlayer.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
           // RedPlayer.transform.parent = GameObject.Find("Canvas").transform;
           // BluePlayer.transform.parent = GameObject.Find("Canvas").transform;
           // GreenPlayer.transform.parent = GameObject.Find("Canvas").transform;

            //RedPlayer.transform.SetParent(this.transform.parent, false);
            //BluePlayer.transform.SetParent(this.transform.parent, false);
            //GreenPlayer.transform.SetParent(this.transform.parent, false);

            this.gameObject.SetActive(false);
        }

        if (collision.tag == "button")
        {
            GameObject[] wall = GameObject.FindGameObjectsWithTag("cellophane");
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<Image>().color = collision.GetComponent<Image>().color;
            }
        }

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
                SoundManager.instance.SFXPlay("Switch", SoundManager.instance.switch_sound, 1f);
            }
        }
    }
}
