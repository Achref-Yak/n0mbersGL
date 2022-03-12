using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Score;
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Score.GetComponent<scoreScript>().ScoreN>9)
        {
            float x = this.GetComponent<RectTransform>().anchoredPosition.x-10;
            float y = this.GetComponent<RectTransform>().anchoredPosition.y;
            this.GetComponent<RectTransform>().anchoredPosition = new Vector3(60,y,0);
        }
            
    }
}
