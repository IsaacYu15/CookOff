using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDishItems : MonoBehaviour
{
    public ItemPlace itemPlace;

    public gameManager gameManager;
    public DishContainer dishContainer;
    public Material cookedChicken;
    public Material cookedSteak;
    public Material choppedLettuce;
    public Material taco;

    public int cubedChickens;
    public int cubedSteaks;
    public int cubedLettuce;
    public int tacos;
    public int wrongItems;
    public float timer;

    private void Start()
    {
        itemPlace = gameObject.GetComponent<ItemPlace>();        
    }

    public void Update()
    {
        //if no object is on top, increment the timer
        if (itemPlace.itemPlaced == null)
        {
            timer = gameManager.currTime;
        }
    }

    public void getDish ()
    {
        if (itemPlace.itemPlaced != null) //if an object is on top
        {
            if (itemPlace.itemPlaced.GetComponent<DishContainer>() != null) //and the object is a dish
            {
                //go through all the dish items, counting up the cubedChicikens, wrong items, tacos, etc for final comparison in scores
                dishContainer = itemPlace.itemPlaced.GetComponent<DishContainer>();
                
                cubedChickens = 0;
                cubedSteaks = 0;
                cubedLettuce = 0;
                tacos = 0;
                wrongItems = 0;

                foreach (GameObject item in dishContainer.itemsOnPlate)
                {
                    if (item.name == cookedChicken.name)
                    {
                        cubedChickens++;
                    }
                    else if (item.name == cookedSteak.name)
                    {
                        cubedSteaks++;
                    }
                    else if (item.name == choppedLettuce.name)
                    {
                        cubedLettuce++;
                    }
                    else if (item.name == taco.name)
                    {
                        tacos++;
                    } else {
                        wrongItems++;
                    }
                }


            }

        } 


    }

    public void resetTable () //reset all values
    {
        cubedChickens = 0;
        cubedSteaks = 0;
        cubedLettuce = 0;
        tacos = 0;
        timer = 0;
    }
}
