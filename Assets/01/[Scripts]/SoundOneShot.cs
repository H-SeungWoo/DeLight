using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOneShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonSoungPlay(AudioClip clip)
    {
        GameObject go = new GameObject("Button Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.PlayOneShot(clip, 0.5f);

        Destroy(go, clip.length);
    }
}
