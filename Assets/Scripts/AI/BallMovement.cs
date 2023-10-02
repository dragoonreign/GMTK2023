using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // public Vector3 speed;
    Rigidbody m_Rigidbody;
    public float m_Thrust = 1;
    public float m_Lift = 1;
    public float m_Force = 1;

    // Start is called before the first frame update
    void Start()
    {
        DoStartBallMovement();
    }

    public void DoStartBallMovement()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.AddForce((transform.forward * m_Thrust + transform.up * m_Lift) * m_Force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball")
        {
            other.GetComponent<Rigidbody>().AddForce((transform.forward * m_Thrust + transform.up * m_Lift) * m_Force, ForceMode.Impulse);
        }
    }

    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.name == "TestBall")
    //     {
    //         m_Rigidbody.AddForce((transform.forward * m_Thrust + transform.up * m_Lift) * m_Force, ForceMode.Impulse);
    //     }
    // }

}
