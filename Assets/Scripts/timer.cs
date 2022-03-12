using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class timer : MonoBehaviour
{
    float s, m;
    public Text counterText;
    public GameObject Main;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }

 
    // Update is called once per frame
    void Update()
    {
        if(Main.GetComponent<cameraScript>().GameOverB==false)
        {
        m = (int)(Time.timeSinceLevelLoad / 60f);
        s = (int)(Time.timeSinceLevelLoad % 60f);
        counterText.text = m.ToString("00") + ":"+ s.ToString("00");
        }
        else
        {
            counterText.GetComponent<Text>().color = Color.white;
        }

    }
}
