using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public Transform refTransform;
    public float followSpeed;
    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = refTransform.position - transform.position; //vectors carry both magnitude and DIRECTION which we can use to help us with rotations
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, followSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
