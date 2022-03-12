using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    static levelManager instance;
    public int level = 0;
    public int hints = 1;
    public string theme = "light";
    public List<int> wins = new List<int>();
    // Start is called before the first frame update
    void Awake()
    {
        if(instance!= null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

   
}
