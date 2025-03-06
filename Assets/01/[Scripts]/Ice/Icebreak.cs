using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icebreak : MonoBehaviour

{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            this.gameObject.SetActive(false);
            if(this.GetComponent<IcePlayer>())
                IcePlayer.instance.isspace = false;
            SoundManager.instance.SFXPlay("ice", SoundManager.instance.ice_sound,1f);
        }
    }
}
