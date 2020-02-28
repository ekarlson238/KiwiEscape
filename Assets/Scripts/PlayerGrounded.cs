using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement pm;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "NotJumpable")
        {
            pm.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "NotJumpable")
        {
            pm.grounded = false;
        }
    }
}
