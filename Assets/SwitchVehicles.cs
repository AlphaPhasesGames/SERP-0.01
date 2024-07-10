using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchVehicles : MonoBehaviour
{
    public bool hasScrolled;

  //  public GameObject carModel;
  //  public GameObject toiletModel;
  //  public GameObject skateModel;

    public GameObject currentModel;
    public int arrayPos;
    public int maxLengthArray;
    public int minLengthArray = 1;

    public GameObject[] modelArray;

    // Start is called before the first frame update
    void Start()
    {
        arrayPos = 0; // on start set array pos to 0
        currentModel = modelArray[arrayPos]; // the current object we have selected is the building brick assigned by the arrayPos
        maxLengthArray = modelArray.Length; // max length of array is the length of the buildingBricks array
    }

    // Update is called once per frame
    void Update()
    {

      



        if (!hasScrolled)
        {
            for (int i = 0; i < 3; i++)
            {
                modelArray[i].SetActive(i == arrayPos);
                Debug.Log("Do We SCroll Forever");
                hasScrolled = true;
            }
        }
       

        if (Input.GetKeyDown(KeyCode.H))
        {
            arrayPos++;
            hasScrolled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasScrolled = false;
            arrayPos = 0;
        }

        if(arrayPos == 3)
        {
            hasScrolled = false;
            arrayPos = 0;
        }



        /*
                if(arrayPos == 1)
                {
                    carModel.gameObject.SetActive(true);
                    Debug.Log("Car Active");
                }

                if(arrayPos == 2)
                {
                    toiletModel.gameObject.SetActive(true);
                    Debug.Log("toilet Active");
                }

                if (arrayPos == 3)
                {
                    skateModel.gameObject.SetActive(true);
                    Debug.Log("skateboard Active");
                }
        */
    }
}
