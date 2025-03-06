using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightBook:MonoBehaviour
{
    public GameObject[] papersRight;
    public GameObject[] papersLeft;
    public Button[] buttons;

    public GameObject book;
    public int currentMap; //현재 진행중인 맵(0: earth, 1: ice, 2: sky, 3: water, 4: dark)

    int currentPage;
    Color basicColor;
    Color selectColor;

    private void Start() {
        selectColor = new Color(235/255f, 217/255f, 171/255f);
        basicColor = new Color(204/255f, 181/255f, 122/255f);

        currentMap = mapManager.currentMap;
        currentPage = 0;
        ChangeColor(currentPage, true);

        CheckMap(currentMap);
    }

    public void CheckMap(int mapNum)
    {
        if(mapNum == 4) return;
        if(mapNum == 3)
        {
            mapNum++;
        }
        for(int i = mapNum; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

    }

    public void markClick()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        int num = int.Parse(clickObject.name.Substring(clickObject.name.Length-1, 1));
        ChangePaper(num);
        SoundManager.instance.SFXPlay("BookNext", SoundManager.instance.book_next, 1f);
    }

    public void ChangePaper(int n)
    {
        ChangeColor(currentPage, false);
        for(int i = 0; i < n; i++)
        {
            papersLeft[i].SetActive(true);
            papersRight[i].SetActive(false);
        }
        for(int i = n; i < 5; i++)
        {
            papersLeft[i].SetActive(false);
            papersRight[i].SetActive(true);
        }
        papersRight[n-1].SetActive(true);
        currentPage = n-1;
        ChangeColor(currentPage, true);
    }

    private void ChangeColor(int n, bool isSelected) {
        ColorBlock colorBlock = buttons[n].colors;
        if(isSelected)
        {
            colorBlock.normalColor = selectColor;
        }
        else
        {
            colorBlock.normalColor = basicColor;
        }
        buttons[n].colors = colorBlock;
    }

    public void CloseBook()
    {
        book.SetActive(false);
    }

}
