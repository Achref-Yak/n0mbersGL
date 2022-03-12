using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rateUs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void maybelater()
    {
        Destroy(this.gameObject);
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.TwoRocks.N0mbers");
        Destroy(this.gameObject);
    }
}
