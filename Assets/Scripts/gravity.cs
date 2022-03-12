using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
 
    public GameObject CameraScript;
    public GameObject spawner;
    public List<GameObject> blockList = new List<GameObject>();
    public GameObject[,] blockListMatrix = new GameObject[4, 4];
    int c = 0;
    // Start is called before the first frame update
    void Start()
    {
        blockListMatrix = spawner.GetComponent<Spawner>().blockListMatrix;
        blockList = spawner.GetComponent<Spawner>().blockList;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("blockListMatrix" + blockListMatrix[0, 1]);
        int counterY = 0;
        for (int j = 0; j < 4; j++)
        {
            for (int k = 0; k < 4; k++)
            {
                c = 0;
                counterY++;
 
                try
                {
                    if (blockListMatrix[k, j + 1] == null && blockListMatrix[k, j]!=null )
                    {
                        
                      

                        blockList[counterY + 3] = blockList[counterY];

                        blockList[counterY] = null;


                        blockListMatrix[k, j + 1] = blockListMatrix[k, j];
  
                        blockListMatrix[k,j] = null;
                   
                        CameraScript.GetComponent<cameraScript>().otherBlock = null;
                        try
                        {
                          
                                Transform toMoveblock = blockListMatrix[k, j + 1].transform;
                            Transform toMoveshadow = blockListMatrix[k, j + 1].GetComponent<Block>().shadowIns.transform;
                      
                      
                      
                            toMoveblock.gameObject.SendMessage("setRow", toMoveblock.gameObject.GetComponent<Block>().Row + 1);
               
                        }
                        catch (System.IndexOutOfRangeException e)
                        { Debug.Log("error : " + e + " j : " + j + " k : " + k); }

                    }
                }
                catch (System.IndexOutOfRangeException e)
                { Debug.Log("error : " + e + " j : " + j); }

            }
        }
    }
}
