using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class mapManager : MonoBehaviour
{
    public static mapManager instance;
    public Sprite[] mapSprites; //clear map sprite
    public static int currentMap; //progressing map
    public Button[] mapButtons; //map buttons
    public GameObject bulboff;
    public GameObject bulbon;

    private void Awake()
    {
        if(mapManager.instance == null)
        {
            mapManager.instance = this;
        }
    }

    void Start()
    {
        CreateMap();
        if(currentMap >= 1)
        {
            bulboff.gameObject.SetActive(false);
            bulbon.gameObject.SetActive(true);
        }
    }



    //map clear check function
    void CreateMap()
    {
        for(int i = 0; i < currentMap; i++)
        {
            //change the map sprite to clear map sprite
            mapButtons[i].image.sprite = mapSprites[i];
            //이미 클리어한 맵 막아두기(스토리상 정령을 구하는 거면 막아둬야 할 거 같아서)
            mapButtons[i].interactable = false;
            ColorBlock colorBlock = mapButtons[i].colors;
            colorBlock.disabledColor = Color.white;
            mapButtons[i].colors = colorBlock;
        }
        if(currentMap < 5)
            mapButtons[currentMap].interactable = true;
    }

    

    public void ClickMapBtn()
    {
        GameObject clickMap = EventSystem.current.currentSelectedGameObject;

        //move to clickMap
        switch(clickMap.name)
        {
            case ("Earth"):
                SceneManager.LoadScene("Earth");
                break;
            case ("Ice"):
                SceneManager.LoadScene("Ice");
                break;
            case ("Sky"):
                SceneManager.LoadScene("Sky");
                break;
            case ("Water"):
                SceneManager.LoadScene("Water");
                break;
            case ("Dark"):
                SceneManager.LoadScene("Dark");
                break;
        }
    }

    public void startScene()
    {
        SceneManager.LoadScene("Start");
    }

    
}



