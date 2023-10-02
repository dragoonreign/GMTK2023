using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAllAction : MonoBehaviour
{
    public PlayerMovement m_PlayerMovement;
    public BallMovement m_BallMovement;

    // Start is called before the first frame update
    void Start()
    {
        DoRemoveInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsRemovePlayerInput)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnEnable() 
    {
        DoRemoveInputs();
    }

    private void OnDisable() 
    {
        DoGiveInputs();
    }

    void DoRemoveInputs()
    {
        m_BallMovement.enabled = false;
        if (!GameManager.instance) return;
        GameManager.instance.DoRemovePlayerInputs();
        GameManager.instance.DoDisableCharacterController();
    }

    void DoGiveInputs()
    {
        if (!m_BallMovement) return;
        m_BallMovement.enabled = true;
        if (!GameManager.instance) return;
        GameManager.instance.DoGivePlayerInputs();
        GameManager.instance.DoEnableCharacterController();
    }
}
