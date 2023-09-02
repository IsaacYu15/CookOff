using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public Transform referencePosition;
    public GameObject itemPlaced;
    public GameObject [] itemsCannotAccept;

    //if an item is listed in itemsCannotAccept, return false
    public bool validItem()
    {
        for(int i = 0; i < itemsCannotAccept.Length; i++ ) { 
            if (itemPlaced.name.Contains(itemsCannotAccept[i].name))
            {
                return false;
            }
        }

        return true;
    }
}
