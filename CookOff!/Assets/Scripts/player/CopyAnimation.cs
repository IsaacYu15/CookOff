using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAnimation : MonoBehaviour
{
    public Transform targetLimb;
    private ConfigurableJoint joint;

    Quaternion targetInitialRotation;
    // Start is called before the first frame update
    void Start()
    {
        joint = this.GetComponent<ConfigurableJoint>();
        this.targetInitialRotation = this.targetLimb.transform.localRotation;
    }


    private void FixedUpdate()
    {
        joint.targetRotation = copyRotation();
    }

    private Quaternion copyRotation() //ragdolls attempt to copy the rotation of their animated counterpart
    {
        return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation;
    }


}
