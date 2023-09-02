using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishContainer : MonoBehaviour
{
    public List<GameObject> itemsOnPlate = new List<GameObject>();
    public GameObject[] acceptableItems;

    //every time an item touches the plate, we the item to "stick" to the plate
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.layer == LayerMask.NameToLayer("item") && validItem()) 
        {
            obj.transform.parent = transform;
            Destroy(obj.GetComponent<Rigidbody>());
            Destroy(obj.GetComponent<Collider>());

            itemsOnPlate.Add(obj);
            
        }
    }

    //check to see if the item is a valid item that can be added to the plate
    public bool validItem()
    {
        for (int i = 0; i < acceptableItems.Length; i++)
        {
            if (acceptableItems[i].name.Contains(acceptableItems[i].name))
            {
                return true;
            }
        }

        return false;
    }
}
