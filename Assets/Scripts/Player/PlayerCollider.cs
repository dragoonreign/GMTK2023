using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public float m_Thrust = 1;
    public float m_Lift = 1;
    public float m_Force = 1;

    private void OnTriggerEnter(Collider other) {
        if (transform.gameObject.tag != "Player") return;
        if (other.tag == "Ground")
        {
            transform.root.gameObject.GetComponent<CharacterController>().enabled = true;
        }
        // if (other.tag == "Interactable")
        // {
        //     transform.root.gameObject.GetComponent<CharacterController>().enabled = false;
        //     transform.root.gameObject.GetComponent<Rigidbody>().AddForce((transform.forward * m_Thrust + transform.up * m_Lift) * m_Force, ForceMode.Impulse);
        // }
    }
}
