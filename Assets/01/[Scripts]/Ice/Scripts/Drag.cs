using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drag : MonoBehaviour,IDragHandler
{
     float distance = 0.002110893f;
     Vector3 begin;


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Debug.Log(eventData.position);
        var x = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = new Vector3(eventData.position.x, 35, 1);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        
    }

    public void OnMouseEnter()
    {
        begin = new Vector3(Input.mousePosition.x, 0,0);
    }


}
