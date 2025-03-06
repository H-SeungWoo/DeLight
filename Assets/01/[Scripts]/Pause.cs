using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {

        PauseCanvas.SetActive(true);
        if (Time.timeScale == 1.0F)
            Time.timeScale = 0.02F;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

    }
}
