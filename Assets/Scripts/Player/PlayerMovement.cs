using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    CharacterController Controller;

    Rigidbody rb;
 
    public float Speed;
    public float NormalSpeed;
    public float SpeedBoost;
 
    public Transform Cam;
 
    //cooldown
    public Cooldown RightClickCD;
    public float RightCD_Length;
    [HideInInspector]
    public bool bCanRightClick = true;
    public Image RightClickImage;

    //cooldown
    public Cooldown LeftClickCD;
    public float LeftCD_Length;
    [HideInInspector]
    public bool bCanLeftClick = true;
    public Image LeftClickImage;

    //cooldown
    public Cooldown SpeedBoostCD;
    public float SpeedBoostCD_Length;
    [HideInInspector]
    public bool bCanSpeedBoost = true;

    //cooldown
    public Cooldown SuperVortexCD;
    public float SuperVortexCD_Length;
    [HideInInspector]
    public bool bCanSuperVortex = true;
    public GameObject m_SuperVortex;
 
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        NormalSpeed = Speed;
        SpeedBoost = Speed * 2;
        
        LeftClickCD = new Cooldown();
        SpeedBoostCD = new Cooldown();
        RightClickCD = new Cooldown();
        SuperVortexCD = new Cooldown();
    }
 
    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        // rb.AddForce(Cam.transform.right * Horizontal + Cam.transform.forward * Vertical - rb.velocity, ForceMode.Force);
 
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical; //
        Movement.y = 0f;

        Controller.Move(Movement);

        //Start cooldown on left click
        if (Input.GetMouseButtonDown(0) && bCanLeftClick)
        {
            //Start Ability
            StartSuperDash();

            //Update UI
            StartUICooldown(LeftClickImage);

            //Start cooldown
            StartAbilityCooldown(SpeedBoostCD, SpeedBoostCD_Length);
            bCanLeftClick = false;
            bCanSpeedBoost = false;
        }

        //start cooldown
        if (SpeedBoostCD.CDStarted)
        {
            //SpeedBoost is on cooldown
            SpeedBoostCD.UpdateCooldown(SpeedBoostCD);
        } else {
            //Off-Cooldown
            StopSuperDash();
            bCanSpeedBoost = true;

            //Start left click cooldown here
            StartAbilityCooldown(LeftClickCD, LeftCD_Length);
        }

        if (SpeedBoostCD.CDBool() && LeftClickCD.CDStarted) {
            //LeftClick is on cooldown
            LeftClickCD.UpdateCooldown(LeftClickCD);
            LeftClickImage.fillAmount -= 1.0f / LeftCD_Length * Time.deltaTime;
        }

        if (LeftClickCD.CDBool())
        {
            //Off-Cooldown
            bCanLeftClick = true;
        } 


/**************************************************/
        //Start cooldown on right click
        if (Input.GetMouseButtonDown(1) && bCanRightClick)
        {
            //Start Ability
            StartSuperVortex();

            //Update UI
            StartUICooldown(RightClickImage);

            //Start cooldown
            StartAbilityCooldown(SuperVortexCD, SuperVortexCD_Length);
            bCanRightClick = false;
            bCanSuperVortex = false;
        }

        //start cooldown
        if (SuperVortexCD.CDStarted)
        {
            //SpeedBoost is on cooldown
            SuperVortexCD.UpdateCooldown(SuperVortexCD);
        } else {
            //Off-Cooldown
            StopSuperVortex();
            bCanSuperVortex = true;

            //Start left click cooldown here
            StartAbilityCooldown(RightClickCD, RightCD_Length);
        }

        if (SuperVortexCD.CDBool() && RightClickCD.CDStarted) {
            //LeftClick is on cooldown
            RightClickCD.UpdateCooldown(RightClickCD);
            RightClickImage.fillAmount -= 1.0f / RightCD_Length * Time.deltaTime;
        }

        if (RightClickCD.CDBool())
        {
            //Off-Cooldown
            bCanRightClick = true;
        } 
/**************************************/

        // //Start cooldown on right click
        // if (Input.GetMouseButtonDown(1) && bCanRightClick)
        // {
        //     StartSuperVortex();
        //     RightClickImage.fillAmount = 1f;
        //     bCanRightClick = false;
        // }
        // if (RightClickCD.CDStarted)
        // {
        //     //RightClick is on cooldown
        //     RightClickCD.UpdateCooldown(RightClickCD);
        //     RightClickImage.fillAmount -= 1.0f / RightCD_Length * Time.deltaTime;
        // } else {
        //     //Off-Cooldown
        //     bCanRightClick = true;
        // }

        // //super vortex CD
        // if (SuperVortexCD.CDStarted && !RightClickCD.CDStarted)
        // {
        //     //SuperVortex is on cooldown
        //     SuperVortexCD.UpdateCooldown(SuperVortexCD);
        // } else {
        //     //Off-Cooldown
        //     StopSuperVortex();
        //     bCanSuperVortex = true;

        //     //Start right click cooldown here
        //     StartAbilityCooldown(RightClickCD, RightCD_Length);
        // }

    }

    public void StartSuperDash()
    {
        //update speed to boost speed
        Speed = SpeedBoost;
    }

    public void StartUICooldown(Image image)
    {
        //Update UI
        image.fillAmount = 1f;
    }

    public void StopSuperDash()
    {
        Speed = NormalSpeed;
    }

    public void StartSuperVortex()
    {
        //Start cooldown
        m_SuperVortex.SetActive(true);
        
    }

    public void StopSuperVortex()
    {
        m_SuperVortex.SetActive(false);
    }

    public void StartAbilityCooldown(Cooldown cd, float cdLength)
    {
        cd.CDStart(cdLength);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
