using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newBlockSpawner : MonoBehaviour
{

    public GameObject NewBlock;
    public GameObject itemsManager;
    public Animator anim;
    GameObject[] prevBlock;
    // Start is called before the first frame update
    public void Add()
    {
         
        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.12f, 10));
        anim.Play("addButton");
        prevBlock = GameObject.FindGameObjectsWithTag("add");
        if (prevBlock == null && itemsManager.GetComponent<itemsManager>().adds>0)
        {
            GameObject blockInsta = (GameObject)Instantiate(NewBlock, centerPos, Quaternion.identity);
 
            itemsManager.GetComponent<itemsManager>().adds-=1;
        }else if (prevBlock != null && itemsManager.GetComponent<itemsManager>().adds>0){
DestroyAdds();

          
            GameObject blockInsta = (GameObject)Instantiate(NewBlock, centerPos, Quaternion.identity);
 
            itemsManager.GetComponent<itemsManager>().adds-=1;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyAdds(){
                    foreach (GameObject blockDe in prevBlock)
            {
                            Destroy(blockDe.GetComponent<blockDragHandler>().shadowIns);
            Destroy(blockDe.GetComponent<blockDragHandler>().numTop);
            Destroy(blockDe.GetComponent<blockDragHandler>().numBottom);
            Destroy(blockDe.GetComponent<blockDragHandler>().numLeft);
            Destroy(blockDe.GetComponent<blockDragHandler>().numRight);
            Destroy(blockDe.GetComponent<blockDragHandler>().pointLeft);
            Destroy(blockDe.GetComponent<blockDragHandler>().pointRight);
            Destroy(blockDe.GetComponent<blockDragHandler>().pointTop);
            Destroy(blockDe.GetComponent<blockDragHandler>().pointBottom);
            Destroy(blockDe);
            }
    }
}
