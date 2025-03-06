using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightstop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("ddddddd");
            ClearSky.SimplePlayerController.islightstop = true;

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("ddddddd");
            ClearSky.SimplePlayerController.islightstop = true;

        }

    }
}
