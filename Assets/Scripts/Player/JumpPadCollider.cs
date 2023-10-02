using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadCollider : MonoBehaviour, IPlayAudio
{
    public float m_Thrust = 1;
    public float m_Lift = 1;
    public float m_Force = 1;
    // public AudioClip audioClip;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Ball")
        {
            other.GetComponent<Rigidbody>().AddForce((transform.forward * m_Thrust + transform.up * m_Lift) * m_Force, ForceMode.Impulse);
            PlayAudio();
        }

        if (other.tag == "Player")
        {
            other.transform.root.gameObject.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<Rigidbody>().AddForce((transform.forward * m_Thrust + transform.up * m_Lift) * m_Force, ForceMode.Impulse);
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        PlayJumpPadAudio();
    }

    public void PlayJumpPadAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
