using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject pushParticles;
    public Animator animator;
    public List<Collider> colliders = new List<Collider>();

    public PlayerMovement move;
    public bool fire;
    private string fireButton;

    public float pushforce = 10f;

    public GameObject itemHeld;
    public bool holdingItem;

    public Transform handPosition;

    //order of priority item > player > ingredientsBox > cuttingBox
    public void Start()
    {
        fireButton = "Fire" + move.playerNumber; //set fire button according to player number
    }

    public void LateUpdate()
    {
        if (Input.GetButtonDown(fireButton))
        {

            //allow one thing at a time priortizing picking up items
            bool selectedAction = false;
            int i = 0;

            while (i < colliders.Count &! selectedAction) //get everything we are touching
            {
                if (colliders[i] == null) //remove dead colliders
                {
                    colliders.Remove(colliders[i]);
                }
                else
                {
                    GameObject obj = colliders[i].transform.gameObject;

                    if (holdingItem && obj.layer == LayerMask.NameToLayer("counter")) //touching a table top
                    {
                        ItemPlace itemPlaceScript = obj.GetComponent<ItemPlace>();
                        itemPlaceScript.itemPlaced = itemHeld;

                        if (itemPlaceScript.validItem()) //we can place the item on this counter
                        {
                            //remove it from our hands, place on table
                            itemHeld.transform.position = itemPlaceScript.referencePosition.position;
                            itemHeld.transform.parent = itemPlaceScript.referencePosition;
                            itemPlaceScript.itemPlaced.transform.rotation = Quaternion.identity;

                            itemHeld.GetComponent<Collider>().enabled = true;
                            itemHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                            colliders.Remove(itemHeld.GetComponent<Collider>());
                            itemHeld = null;
                            holdingItem = false;

                        } else
                        {
                            //drop the item
                            itemPlaceScript.itemPlaced = null;
                            deactivateItem();
                        }


                        selectedAction = true; //prevents us from selecting another action later on

                    }


                    i++;
                }

            }

            if (holdingItem) //drop the item
            {
                deactivateItem();
                selectedAction = true;

            }

            i = 0;

            while (i < colliders.Count & !selectedAction) //we want to push a player
            {
                GameObject obj = colliders[i].transform.gameObject;
                GameObject parent = obj.transform.root.gameObject;

                if (obj.layer == LayerMask.NameToLayer("player") && parent.name != gameObject.transform.root.gameObject.name) //make sure we are not pushing ourselves
                {
                    Transform hips = parent.GetComponent<PlayerComponents>().hips;
                    //spawn a push particle effect
                    GameObject pushEffect = Instantiate(pushParticles, hips.position, Quaternion.identity);
                    Destroy(pushEffect, 1);

                    hips.GetComponent<Rigidbody>().AddForce(transform.forward * pushforce, ForceMode.Impulse);
                    selectedAction = true;
                    animator.SetTrigger("push"); //trigger animation

                }

                i++;
            }

            i = 0;

            while (i < colliders.Count & !selectedAction)
            {
                GameObject obj = colliders[i].transform.gameObject;


                if (obj.layer == LayerMask.NameToLayer("item")) //taking an item off of the counter
                {
                    for (int j = 0; j < colliders.Count; j++)
                    {
                        if (colliders[j].transform.gameObject.layer == LayerMask.NameToLayer("counter"))
                        {
                            colliders[j].transform.gameObject.GetComponent<ItemPlace>().itemPlaced = null;
                        }
                    }

                    obj.GetComponent<Collider>().enabled = false;

                    //parent the object to our hand so it follows the player
                    obj.transform.position = handPosition.position;
                    obj.transform.parent = handPosition;
                    obj.transform.rotation = Quaternion.identity;

                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //freeze all so it stays in our hand

                    itemHeld = obj;
                    holdingItem = true;

                    selectedAction = true;

                }

                i++;
            }

            i = 0;

            while (i < colliders.Count & !selectedAction) //if we are interacting with ingredient box to spawn an item
            {
                GameObject obj = colliders[i].transform.gameObject;

                if (obj.layer == LayerMask.NameToLayer("ingredients"))
                {
                    obj.GetComponent<ItemSpawner>().spawnItem();

                    selectedAction = true;
                }

                i++;
            }


        }
    }

    private void OnTriggerEnter(Collider other) //get all items we collide with
    {
        if (!colliders.Contains(other) && other.gameObject.name != transform.parent.gameObject.name) {
            colliders.Add(other); 
        }
    }

    private void OnTriggerExit(Collider other) //update collider list
    {
        colliders.Remove(other);
    }

    public void deactivateItem () //deactivate an item (belongs to no parent, item is sitting on the ground)
    {

        colliders.Remove(itemHeld.GetComponent<Collider>());

        itemHeld.transform.parent = null;

        itemHeld.GetComponent<Collider>().enabled = true;
        itemHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        itemHeld = null;
        holdingItem = false;

    }



}
