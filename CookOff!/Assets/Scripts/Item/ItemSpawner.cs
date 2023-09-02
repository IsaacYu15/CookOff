using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject prefab;
    GameObject item;

    public Transform referencePos;

    public void spawnItem ()//instantiate an item at a reference position when called
    {
        item = Instantiate(prefab, referencePos.position, Quaternion.identity);
        item.name = prefab.name;
    }

}
