using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> blockList = new List<GameObject>();
    public GameObject[,] blockListMatrix = new GameObject[4, 4];

    public GameObject Block;
    public float spaceiffset = 1.15f;
    public float spaceyOffset =0.0f;
    private void setResponsive()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(0, (Screen.height/1.7f), 0));
        float width = Screen.width;
        float height = Screen.height;
        if (width == 1080 && height == 2340)
        {
            spaceyOffset = 1.13f;
            spaceiffset = 1.13f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.114f, 0, 0);
        }

        if (width == 1080 && height == 2160)
        {
            spaceyOffset = 1.15f;
            spaceiffset = 1.205f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.192f, 0, 0);
        }

        if (width == 1080 && height == 2280)
        {
            spaceyOffset = 1.150f;
            spaceiffset = 1.185f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.102f, 0, 0);
        }


        if (width == 720 && height == 1440)
        {
            spaceyOffset = 1.205f;
            spaceiffset = 1.205f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.192f, 0, 0);
        }


        if (width == 720 && height == 1560)
        {
            spaceyOffset = 1.105f;
            spaceiffset = 1.155f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.09f, 0, 0);
        }
        if (width == 720 && height == 1600)
        {
            spaceyOffset = 1.105f;
            spaceiffset = 1.155f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.00f, 0, 0);
        }

        if (width == 1080 && height == 1920)
        {
            spaceyOffset = 1.17f;
            spaceiffset = 1.40f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.195f, 0, 0);
        }
        if (width == 1440 && height == 2560)
        {
            spaceyOffset = 1.28f;
            spaceiffset = 1.40f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.205f, 0, 0);
        }
        if (width == 1080 && height == 2246)
        {
            spaceyOffset = 1.17f;
            spaceiffset = 1.20f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.105f, 0, 0);
        }
        if (width == 1440 && height == 3040)
        {
            spaceyOffset = 0.2f;
            spaceiffset = 1.15f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.137f, -2, 0);
        }
        if (width == 1440 && height == 2960)
        {
            spaceyOffset = 1.13f;
            spaceiffset = 1.15f;
            this.transform.position = point + new Vector3((1.0f / 2) + 0.207f, 0, 0);
        }
     


    }
    // Start is called before the first frame update
    void Start()
    {
        setResponsive();

        int k = -1;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                k++;
                GameObject blockInsta = (GameObject)Instantiate(Block, new Vector3(transform.position.x + Block.GetComponent<BoxCollider2D>().bounds.size.x + i * spaceiffset, transform.position.y - j * spaceyOffset, 0), Quaternion.identity);
                blockInsta.SendMessage("setBlocknum",  k);
                blockInsta.SendMessage("setColumn", i);
                blockInsta.SendMessage("setRow", j);
                blockInsta.GetComponent<BoxCollider2D>().size = new Vector2(blockInsta.GetComponent<BoxCollider2D>().size.x, blockInsta.GetComponent<BoxCollider2D>().size.y+ spaceyOffset);
                blockList.Add(blockInsta);
                

                blockListMatrix[i, j] = blockInsta;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {




    }


    private void replaceBlock()
    {

    }
    public GameObject GetBlock(int i, int j)
    {

        return blockListMatrix[i, j];
    }


    public void setGroups()
    {
        int num = Random.Range(1, 3);

        List<List<GameObject>> blocks = new List<List<GameObject>>();
        List<GameObject> block1 = new List<GameObject>();
        List<GameObject> block2 = new List<GameObject>();
        List<GameObject> block3 = new List<GameObject>();
        List<GameObject> block4 = new List<GameObject>();
        List<GameObject> block5 = new List<GameObject>();
        List<GameObject> block6 = new List<GameObject>();


        block1.Add(blockListMatrix[0, 0]);
        block1.Add(blockListMatrix[1, 0]);
        block1.Add(blockListMatrix[1, 1]);
        block1.Add(blockListMatrix[0, 1]);



        /*block2.Add(blockListMatrix[2, 0]);
        block2.Add(blockListMatrix[2, 1]);

         block3.Add(blockListMatrix[0, 2]);
         block3.Add(blockListMatrix[0, 3]);

         block4.Add(blockListMatrix[1, 2]);
         block4.Add(blockListMatrix[2, 2]);
         block4.Add(blockListMatrix[2, 3]);
         block4.Add(blockListMatrix[1, 3]);*/

    }
}
