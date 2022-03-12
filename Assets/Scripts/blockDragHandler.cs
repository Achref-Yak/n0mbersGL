using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class blockDragHandler : MonoBehaviour
{
    public bool Drag = false;
    public Vector3 target;
    public GameObject place;
    public GameObject Spawner;
    public GameObject Block;
    float spaceiffset = 1.05f;
    public Image point;
    public Image pointTop;
    public Image pointLeft;
    public Image pointBottom;
    public Image pointRight;

    public Sprite pointRed;
    public Sprite pointYellow;
    public Sprite pointBlue;



    public GameObject cameraObject;
 
    public int[] coms = new int[4];
    public GameObject canvas;
    public Image imageTop;
    public Text numTop;
    public Text numBottom;
    public Text numLeft;
    public Text numRight;
    public int counter = 0;
    public int Blocknum;
    public GameObject shadow;

    public GameObject Cross;
    public GameObject shadowIns;
    public int[] numbers = new int[4];
    public Text num;
    int randomBlockNum;
    GameObject block;

        public int Column;
    public int Row;

        float offset;

    
    public Vector3 blockSize;
    float betweenOfset;

     private void setResponsive()
    {
        float width = Screen.width;
        float height = Screen.height;
        if (width == 1080 && height == 2340)
        {
            blockSize = new Vector3(0.077f, 0.077f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 9.0f;
        }

        if (width == 1080 && height == 2160)
        {
            blockSize = new Vector3(0.086f, 0.084f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 9.6f;
        }

        if (width == 1080 && height == 1920)
        {
            blockSize = new Vector3(0.099f, 0.097f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 22.6f;
        }

    }
    void Start()
    {
        setResponsive();
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        randomBlockNum = Random.Range(1, 6);
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        Blocknum = randomBlockNum;
        block = GameObject.FindGameObjectWithTag("Block");
        transform.localScale = block.GetComponent<Block>().blockSize;


        // this declares an integer array with 5 elements
        // and initializes all of them to their default value
        // which is zero
        coms = setComb(Blocknum);

        for (int i = 0; i < numbers.Length; i++)
        {
            int num = coms[i];


            numbers[i] = num;

        }




           canvas = GameObject.FindGameObjectWithTag("Canvas");
        pointTop = (Image)Instantiate(point, transform.position, transform.rotation);
        float offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x;
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        numTop = (Text)Instantiate(num, transform.position, transform.rotation);

        numTop.text = numbers[0].ToString();

        pointTop.transform.SetParent(canvas.transform);
        pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (numTop.text != "0")
            pointTop.GetComponent<Image>().color = new Color32(0, 88, 122, 255);

        numTop.transform.SetParent(canvas.transform);
        numTop.transform.position = namePose + new Vector3(0, offset, 0);
        /*pointTop = (Image)Instantiate(point, transform.position, transform.rotation);
        pointTop.transform.SetParent(canvas.transform);
        pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (numbers[0]==2)
        {
            pointTop.sprite = pointRed;
        }
        else if (numbers[0] == 4)
        {
            pointTop.sprite = pointBlue;
        }
        else if (numbers[0]==1)
        {
            pointTop.sprite = pointYellow;
        }
        else if (numbers[0] == 0)
        {
            pointTop.sprite = zero;
        }*/


        pointBottom = (Image)Instantiate(point, transform.position, transform.rotation);
        numBottom = (Text)Instantiate(num, transform.position, transform.rotation);

        numBottom.text = numbers[2].ToString();
        if (numBottom.text != "0")
            pointBottom.GetComponent<Image>().color = new Color32(0, 88, 122, 255);

        pointBottom.transform.SetParent(canvas.transform);
        pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);

        numBottom.transform.SetParent(canvas.transform);
        numBottom.transform.position = namePose + new Vector3(0, -offset, 0);

        /*pointBottom = (Image)Instantiate(point, transform.position, transform.rotation);
        pointBottom.transform.SetParent(canvas.transform);
        pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);
        if (numbers[2] == 2)
        {
            pointBottom.sprite = pointRed;
        }
        else if (numbers[2] == 1)
        {
            pointBottom.sprite = pointYellow;
        }
        else if (numbers[2] == 0)
        {
            pointBottom.sprite = zero;
        }
        else if (numbers[2] == 4)
        {
            pointBottom.sprite = pointBlue;
        }*/
        pointLeft = (Image)Instantiate(point, transform.position, transform.rotation);
        numLeft = (Text)Instantiate(num, transform.position, transform.rotation);
        numLeft.text = numbers[1].ToString();

        if (numLeft.text != "0")
            pointLeft.GetComponent<Image>().color = new Color32(0, 88, 122, 255);
        pointLeft.transform.SetParent(canvas.transform);
        pointLeft.transform.position = namePose + new Vector3(-offset, 0, 0);


        numLeft.transform.SetParent(canvas.transform);
        numLeft.transform.position = namePose + new Vector3(-offset, 0, 0);


        /*pointLeft = (Image)Instantiate(point, transform.position, transform.rotation);
        pointLeft.transform.SetParent(canvas.transform);
        pointLeft.transform.position = namePose + new Vector3(-offset, 0 , 0);
        if (numbers[1] == 2)
        {
            pointLeft.sprite = pointYellow;
        }
        else if (numbers[1] == 8)
        {
            pointLeft.sprite = pointRed;
        }
        else if (numbers[1] == 0)
        {
            pointLeft.sprite = zero;
        }
        else if (numbers[1] == 4)
        {
            pointLeft.sprite = pointBlue;
        }*/

        pointRight = (Image)Instantiate(point, transform.position, transform.rotation);
        numRight = (Text)Instantiate(num, transform.position, transform.rotation);
        numRight.text = numbers[3].ToString();
        pointRight.transform.SetParent(canvas.transform);
        pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numRight.transform.SetParent(canvas.transform);
        if (numRight.text != "0")
            pointRight.GetComponent<Image>().color = new Color32(0, 88, 122, 255);
        numRight.transform.position = namePose + new Vector3(offset, 0, 0);
        /*pointRight = (Image)Instantiate(point, transform.position, transform.rotation);
        pointRight.transform.SetParent(canvas.transform);
        pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
        if (numbers[3] == 2)
        {
            pointRight.sprite = pointYellow;
        }
        else if (numbers[3] == 8)
        {
            pointRight.sprite = pointRed;
        }
        else if (numbers[3] == 0)
        {
            pointRight.sprite = zero;
        }
        else if (numbers[3] == 4)
        {
            pointRight.sprite = pointBlue;
        }*/
        shadowIns = (GameObject)Instantiate(shadow, new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z), transform.rotation);
        setColor();


    }





    void Update()
    {
        updatePosition();
        if (Drag)
        {
            takePlace();
        }
        if (shadowIns != null)
            shadowIns.transform.position = transform.position + new Vector3(0, -0.04f, 0);
    }

        public void setColor()
    {
        if (numTop.text != "0")
        {
            numTop.color = new Color32(230, 230, 230, 255);
            pointTop.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
        }

        else
        {
            pointTop.GetComponent<Image>().color = new Color32(60, 60, 60, 255);
            numTop.color = new Color32(111, 111, 111, 255);
        }

        if (numLeft.text != "0")
        {
            pointLeft.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
            numLeft.color = new Color32(230, 230, 230, 255);
        }

        else
        {
            pointLeft.GetComponent<Image>().color = new Color32(60, 60, 60, 255);
            numLeft.color = new Color32(111, 111, 111, 255);
        }

        if (numBottom.text != "0")
        {
            numBottom.color = new Color32(230, 230, 230, 255);
            pointBottom.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
        }

        else
        {
            pointBottom.GetComponent<Image>().color = new Color32(60, 60, 60, 255);
            numBottom.color = new Color32(111, 111, 111, 255);
        }

        if (numRight.text != "0")
        {
            numRight.color = new Color32(230, 230, 230, 255);
            pointRight.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
        }

        else
        {
            numRight.color = new Color32(111, 111, 111, 255);
            pointRight.GetComponent<Image>().color = new Color32(60, 60, 60, 255);
        }
        for (int i = 0; i < 4; i++)
        {
            if (numbers[i] == 0)
                counter++;




            if (counter == 1)
            {
                // this.gameObject.GetComponent<Renderer>().material.color = new Color32(231, 211, 159, 255);
                shadowIns.GetComponent<Renderer>().material.color = new Color32(231, 211, 159, 130);
                /*numTop.color = new Color32(255, 255, 255, 255);
                numBottom.color = new Color32(255, 255, 255, 255);
                numLeft.color = new Color32(255, 255, 255, 255);
                numRight.color = new Color32(255, 255, 255, 255);*/
            }

            else if (counter == 2)
            {

                //this.gameObject.GetComponent<Renderer>().material.color = new Color32(234, 144, 133, 255);
                /*numTop.color = new Color32(255, 255, 255, 255);
                numBottom.color = new Color32(255, 255, 255, 255);
                numLeft.color = new Color32(255, 255, 255, 255);
                numRight.color = new Color32(255, 255, 255, 255);*/
            }

            else if (counter == 3)
            {
                //this.gameObject.GetComponent<Renderer>().material.color = new Color32(212, 93, 121, 255);
                /*numTop.color = new Color32(212, 93, 121, 255;
                numBottom.color = new Color32(255, 255, 255, 255);
                numLeft.color = new Color32(255, 255, 255, 255);
                numRight.color = new Color32(255, 255, 255, 255);*/
            }
            else if (counter == 4)
            {
                Destroy(numTop);
                Destroy(numLeft);
                Destroy(numBottom);
                Destroy(numRight);
                Destroy(pointTop);
                Destroy(pointBottom);
                Destroy(pointLeft);
                Destroy(pointRight);


                Destroy(shadowIns);

                createPlaces(Row,Column);


                Destroy(this.gameObject);
            }

        }

        counter = 0;



    }
     public void createPlaces(int Row, int Column)
    {
        bool x = true;
        GameObject[] places = GameObject.FindGameObjectsWithTag("Place");
        for (int i = 0; i < places.Length; i++)
        {
            if (places[i].GetComponent<Place>().column == Column && places[i].GetComponent<Place>().row == Row)
            {
                x = false;
                break;
            }

        }

        if(x){
      GameObject placeN = (GameObject)Instantiate(place, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.9f), transform.rotation);

        placeN.transform.localScale = blockSize;
        placeN.GetComponent<Place>().column = Column;
        placeN.GetComponent<Place>().row = Row;
        }
  
    }

    public void takePlace()
    {
 
        int col = place.GetComponent<Place>().column;
        int row = place.GetComponent<Place>().row;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 20);
             
 
     Destroy(Spawner.GetComponent<Spawner>().blockListMatrix[col, row]);
        if (transform.position == target)
        {
     
            GameObject blockInsta = (GameObject)Instantiate(Block, target, Quaternion.identity);

            blockInsta.GetComponent<Block>().coms = coms;
            blockInsta.GetComponent<Block>().numbers = numbers;
            blockInsta.SendMessage("setColumn", col);
            blockInsta.SendMessage("setRow", row);
            blockInsta.GetComponent<Block>().NewBlock = true;
            blockInsta.GetComponent<Block>().newTargetPosition = target;
            blockInsta.GetComponent<Block>().i1 = place.GetComponent<Place>().i1;
            blockInsta.GetComponent<Block>().i2 = place.GetComponent<Place>().i2;
            blockInsta.GetComponent<Block>().i3 = place.GetComponent<Place>().i3;
            blockInsta.GetComponent<Block>().i4 = place.GetComponent<Place>().i4;


            cameraObject.GetComponent<cameraScript>().blockList[getListIndexFromMatrix(col,row)] =blockInsta;
        
            Spawner.GetComponent<Spawner>().blockListMatrix[col, row] = blockInsta;
    
            
            Destroy(numTop);
            Destroy(numLeft);
            Destroy(numBottom);
            Destroy(numRight);
                Destroy(pointTop);
                Destroy(pointBottom);
                Destroy(pointLeft);
                Destroy(pointRight);
            Destroy(shadowIns);
                 
            Destroy(this.gameObject);

        }

    }

   private int getListIndexFromMatrix(int col, int row)
   {
                      int c=-1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                c++;
                if(j==col&&i==row)
                {
                     Debug.Log("oo : "+c+" col : "+col+"row : "+row);
                      return c;
                } 
               
            }
        }
        return 0;
   }
    void updatePosition()
    {


        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        if (pointBottom)
            pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);
        if (pointTop)
            pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (pointLeft)
            pointLeft.transform.position = namePose + new Vector3(-offset, 0, 0);
        if (pointRight)
            pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numTop.transform.position = namePose + new Vector3(0, offset, 0);
        numLeft.transform.position = namePose + new Vector3(-offset, 0, 0);
        numRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numBottom.transform.position = namePose + new Vector3(0, -offset, 0);


    }
    public int[] setComb(int num)
    {
        int[] numbersT = new int[4];



        for (int i = 0; i < 4; i++)
        {
            int[] nums = new int[3] { 2, 4, 8 };

            int numR = Random.Range(0, 3);
            if (numR == 0)
                numR = 2;
            else if (numR == 1)
                numR = 4;
            else if (numR == 2)
                numR = 8;
            Debug.Log("annd : " + numR);
            numbersT[i] = numR;

        }


        for (int i = 0; i < 4; i++)
        {
            int sum = sumFun(numbersT);
            if (sum > num)
                for (int j = i; j < 4; j++)
                {
                    if (numbersT[j] == 4)
                        numbersT[j] = 2;
                    else if (numbersT[j] == 2)
                        numbersT[j] = 0;
                    else if (numbersT[j] == 8)
                        numbersT[j] = 4;
                }

            else if (sum < num)
                for (int j = i; j < 4; j++)
                {
                    if (numbersT[j] == 0)
                        numbersT[j] = 2;
                    else if (numbersT[j] == 2)
                        numbersT[j] = 4;
                    else if (numbersT[j] == 4)
                        numbersT[j] = 8;



                }

            else if (sum == num)
            {
                break;
            }
        }






        return numbersT;

    }

    private int sumFun(int[] numbersT)
    {
        int sum = 0;
        for (int k = 0; k < 4; k++)
        {
            sum += numbersT[k];

        }
        return sum;
    }
 


    int setBlocknum(int x)
    {
        int[] numbersT = new int[16] { 0, 14, 8, 2 * 2, 3 * 2, 2 * 2, 4 * 2, 6 * 2, 3 * 2, 2 * 2, 3 * 2, 2 * 2, 4 * 2, 4, 8, 4 };

        return numbersT[x];
    }

}

