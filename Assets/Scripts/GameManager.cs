using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject BeatUI;
    public GameObject WaitForPlayerUI;
    public PlayableDirector director;
    public PlayerMovement m_PlayerMovement;
    public CharacterController m_CharacterController;
    public Cooldown m_Cooldown;
    public int currScene;
    Scene scene;
    [HideInInspector]
    public bool IsRemovePlayerInput = true;
    [HideInInspector]
    public bool OnWaitingForPlayerInput = true;
    [HideInInspector]
    public bool OnLevelBeaten = false;

    public Transform playerSpawnerTransform;
    public Transform ballSpawnerTransform;
    public GameObject playerGameObject;
    public GameObject ballGameObject;

    private void Awake() {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Cooldown = new Cooldown();
        scene = SceneManager.GetActiveScene();
        currScene = scene.buildIndex;
    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    // Update is called once per frame
    void Update()
    {
        DoUpdate();

        //check if level is beaten
        if (OnLevelBeaten && !OnWaitingForPlayerInput)
        {
            DoLoadNextLevel();
        }
    }

    public void DoUpdate()
    {
        OnPlayerInput();
    }
    
    public void OnPlayerInput()
    {
        //in cutscene remove player input
        if (IsRemovePlayerInput) return;
        DoStopTime();

        //waiting for player input
        if (Input.anyKey && WaitForPlayerUI.activeInHierarchy)
        {
            OnWaitingForPlayerInput = false;
        }

        //waiting for player input
        if (Input.GetKeyDown("r"))
        {
            ResetLevel();
        }

        if (Input.GetKey("escape"))
        {
            DoQuitApplication();
        }

        if (OnWaitingForPlayerInput) return;
        DoStartTime();

        //player is now active
        if (!WaitForPlayerUI.activeInHierarchy) return;
        DoDisableWaitForPlayerUI();
    }

    public void DoEnableLevelBeatUI()
    {
        BeatUI.SetActive(true);
    }

    public void DoEnableWaitForPlayerUI()
    {
        OnWaitingForPlayerInput = true;
        WaitForPlayerUI.SetActive(true);
    }

    public void DoDisableWaitForPlayerUI()
    {
        WaitForPlayerUI.SetActive(false);
    }

    public void DoStopTime()
    {
        Time.timeScale = 0f;
    }

    public void DoStartTime()
    {
        Time.timeScale = 1.0f;
    }

    public void DoLevelStart()
    {
        // DoStartTime();
        DoEnableCharacterController();
    }

    public void DoLevelBeaten()
    {
        // DoStopTime();
        // DoRemovePlayerInputs();
        DoEnableLevelBeatUI();
        DoEnableWaitForPlayerUI();
        DoDisableCharacterController();
        OnLevelBeaten = true;
    }

    public void DoLoadNextLevel()
    {
        SceneManager.LoadScene(currScene + 1);
    }

    public void DoLoadThisLevel()
    {
        SceneManager.LoadScene(currScene);
    }

    public void ResetLevel() 
    {
        Rigidbody m_PRB = playerGameObject.GetComponent<Rigidbody>();
        Rigidbody m_BRB = ballGameObject.GetComponent<Rigidbody>();
        if (m_PRB.velocity.magnitude > 0)
        {
            m_PRB.velocity = Vector3.zero;
        }
        playerGameObject.GetComponent<CharacterController>().enabled = false;
        playerGameObject.transform.position = playerSpawnerTransform.position;
        playerGameObject.GetComponent<CharacterController>().enabled = true;
        if (m_BRB.velocity.magnitude > 0)
        {
            m_BRB.velocity = Vector3.zero;
        }
        ballGameObject.transform.position = ballSpawnerTransform.position;
        ballGameObject.transform.rotation = ballSpawnerTransform.rotation;
        ballGameObject.GetComponent<BallMovement>().DoStartBallMovement();
    }

    public void DoEnableCharacterController()
    {
        m_CharacterController.enabled = true;
    }

    public void DoDisableCharacterController()
    {
        m_CharacterController.enabled = false;
    }

    public void DoRemovePlayerInputs()
    {
        m_PlayerMovement.enabled = false;
    }

    public void DoGivePlayerInputs()
    {
        m_PlayerMovement.enabled = true;
    }

    public void DoQuitApplication() 
    {
        Application.Quit();
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            instance.IsRemovePlayerInput = false;
            DoEnableWaitForPlayerUI();
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
