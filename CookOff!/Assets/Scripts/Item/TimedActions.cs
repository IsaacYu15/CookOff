using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedActions : MonoBehaviour
{
    public Slider slider;
    public float maxTime = 3f;
    private float currTime;

    private ItemPlace itemPlaceScript;

    public GameObject finishedItem;

    public Material steak;
    public Material chicken;
    public Material lettuce;

    private void Start()
    {
        //set all to max time (timers go down)
        currTime = maxTime;
        slider.maxValue = currTime;
        slider.value = slider.maxValue;

        itemPlaceScript = gameObject.GetComponent<ItemPlace>();

        slider.gameObject.SetActive(false);

    }

    void Update()
    {
        if (itemPlaceScript.itemPlaced != null) //when there is an item
        {

            slider.gameObject.SetActive(true); //activate timer and decrement time

            currTime -= Time.deltaTime;
            slider.value = currTime;

            if (currTime <= 0) //when the item is finished
            {
                //spawn cut item
                GameObject obj = Instantiate(finishedItem, itemPlaceScript.referencePosition.position, Quaternion.identity);

                //change the material depending on item type
                if (itemPlaceScript.itemPlaced.tag == "chicken")
                {
                    setCubedMaterial (chicken, obj, "chicken");
                    obj.name = chicken.name;

                } else if (itemPlaceScript.itemPlaced.tag == "steak")
                {
                    setCubedMaterial(steak, obj, "steak");
                    obj.name = steak.name;
                } else if (itemPlaceScript.itemPlaced.tag == "lettuce")
                {
                    setCubedMaterial(lettuce, obj, "lettuce");
                    obj.name = lettuce.name;

                    if (gameObject.name.Contains("grill"))
                    {
                        Destroy(obj);
                    }

                }

                //destroy old item
                Destroy(itemPlaceScript.itemPlaced);
                itemPlaceScript.itemPlaced = null;

                //reset counter and slider properties
                resetSlider();
            }

        } else
        {
            resetSlider();
        }
    }

    public void resetSlider ()
    {
        //reset counter and slider properties
        currTime = maxTime;

        slider.value = slider.maxValue;
        slider.gameObject.SetActive(false);
    }

    public void setCubedMaterial (Material material, GameObject obj, string tag)
    {
        obj.tag = tag; //transfer tag

        foreach (Transform child in obj.transform) //apply new material
        {
            child.GetComponent<MeshRenderer>().material = material;
            child.gameObject.tag = tag;
        }
    }


}
