using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexCollider : MonoBehaviour
{
    public float m_Thrust = 1;
    public float m_Lift = 1;
    public float m_Force = 1;

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Ball")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((transform.root.position - other.transform.position) * m_Force, ForceMode.Force);
        }
    }
}
