using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scoreScript : MonoBehaviour
{
    public Text scoreText;
    public int ScoreN = 0;
    public GameObject mainScript;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreN = mainScript.GetComponent<cameraScript>().scoreint;
        scoreText.text = ScoreN.ToString();
    }
}
