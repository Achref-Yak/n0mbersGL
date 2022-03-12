using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemsManager : MonoBehaviour
{
    public int adds = 3;
    public int hints = 1;
    public int backs = 3;
    public Text addsText;
    public Text hintText; 
    public Text backText;
    public GameObject cameraObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void resetCorrect()
    {
        List<int> disableds = new List<int>();
        disableds.Clear();
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
       
        foreach (GameObject block in cameraObj.GetComponent<cameraScript>().blockList)
        {
            if (block.GetComponent<Block>().enabled)
                Debug.Log(cameraObj.GetComponent<cameraScript>().blockList.IndexOf(block) + " : enabled");
            else
            {
                disableds.Add(cameraObj.GetComponent<cameraScript>().blockList.IndexOf(block));
                Debug.Log(cameraObj.GetComponent<cameraScript>().blockList.IndexOf(block) + " : disabled");
            }

        }
        int index = -1;
        foreach (GameObject block in cameraObj.GetComponent<cameraScript>().blockList)
            try
            {



                Dictionary<int, List<int>> Pairs = new Dictionary<int, List<int>>();
                Pairs = block.GetComponent<Block>().Pairs;
                


                index = cameraObj.GetComponent<cameraScript>().blockList.IndexOf(block);
               
                if (checkAdjInDisableds(Pairs[index], disableds) && !cameraObj.GetComponent<cameraScript>().CorruptedLists.Contains(index))
                {
                    block.GetComponent<Block>().finishedBlock = true;
                    Debug.Log(index + " adjs all disabled");
                }
                 
                else
                    Reset(Pairs[index]);



            }
            catch (System.Exception e)
            {
               Debug.Log("allll"+ index);
            }

        cameraObj.GetComponent<cameraScript>().CorruptedLists.Clear();
    }

    public void Reset(List<int> vals)
    {
        foreach (int val in vals)
        {
            //cameraObj.GetComponent<cameraScript>().blockList[val].GetComponent<BoxCollider2D>().enabled = true;
            try
            {
                cameraObj.GetComponent<cameraScript>().blockList[val].GetComponent<Block>().enabled = true;
                cameraObj.GetComponent<cameraScript>().blockList[val].GetComponent<Block>().reactivateBlock();
                cameraObj.GetComponent<cameraScript>().blockList[val].GetComponent<Block>().resetInits();


            }
            catch(System.Exception e)
            {
                Debug.LogError("ababab");
            }
    
        }
        //    cameraObj.GetComponent<cameraScript>().blockList[val].GetComponent<Block>().numbers = cameraObj.GetComponent<cameraScript>().blockList[val].GetComponent<Block>().inits;



    }
    private bool checkAdjInDisableds(List<int> vals, List<int> disableds)
    {

        foreach (int val in vals)
        {
            //  Debug.Log("ch"+val+"="+)
            if (!disableds.Contains(val))
                // block sets are not modified, we need to reset them.
                
                return false;
        }

        // block sets are all deleted correctly, we can't reset this set.
        return true;

    }

    // Update is called once per frame
    void Update()
    {
        backText.text = backs.ToString();
        if (backs < 0)
            backs = 0;
 
        if (hints < 0)
            hints = 0;
        hintText.text = (hints).ToString();

    }
}
