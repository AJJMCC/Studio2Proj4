using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFlash : MonoBehaviour {
    public static CanvasFlash Instance;

    public GameObject Obj;
    private Image PanImg;


    private Color color = Color.white;

    public bool Flash = false;
    public bool SlowFlash = false;

    // Use this for initialization
    void Start()
    {
        color.a = 0;
        Instance = this;
        PanImg = Obj.GetComponent<Image>();

    }
    void Update()
    {
        if (!Flash)
        {
            if (PanImg.color.a > 0)
            {
                color.a -= 2.5f * Time.deltaTime;


                PanImg.color = color;
              
            }
          

        }

      if (Flash)
        {
            if (PanImg.color.a < 255)
            {
                color.a += 10.5f * Time.deltaTime;
                PanImg.color = color;
            }
        }


      if (SlowFlash)
        {
            color.a += 3f * Time.deltaTime;
            PanImg.color = color;
        }

    }



    
}
