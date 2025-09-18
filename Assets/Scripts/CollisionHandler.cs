using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float SequenceDelay = 0f;
    [SerializeField] AudioClip SFXCrash;
    [SerializeField] AudioClip SFXLevelComplete;
    [SerializeField] ParticleSystem VFXCrash;
    [SerializeField] ParticleSystem VFXLevelComplete;


    AudioSource audioSource;

    bool isControllable = true;
    bool isCollisionOn = true;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollisionOn) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("FriendlyTag");
                break;
            case "Fuel":
                Debug.Log("FuelTag");
                break;
            case "Finish":
                Debug.Log("FinishTag");
                StartLevelCompleteSequence();
                break;
            case "Respawn":
                Debug.Log("RespawnTag");
                break;
            default:
                Debug.Log("crash");
                StartCrashSequence();
                break;
        }
    }

    // these sequence parts could be handle wihthin one manager

    private void StartLevelCompleteSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(SFXLevelComplete);
        VFXLevelComplete.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", SequenceDelay);
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(SFXCrash);
        VFXCrash.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", SequenceDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollisionOn = !isCollisionOn;
        }
    }

}

