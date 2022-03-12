using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuThemeControl : MonoBehaviour
{

    public GameObject cameraOb;
    string theme;
    // Start is called before the first frame update
    void Start()
    {
        theme = cameraOb.GetComponent<cameraScript>().theme;

        if (theme == "dark")
            this.GetComponent<CanvasRenderer>().SetColor(new Color32(200, 200, 200, 255));
        else
            this.GetComponent<CanvasRenderer>().SetColor(new Color32(150, 150, 150, 255));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
