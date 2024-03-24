using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Comp Variables
    [SerializeField] float LoadLevelDelay = 1f;
    [SerializeField] AudioClip Crash;
    [SerializeField] AudioClip Success;
    [SerializeField] ParticleSystem CrashParticles;
    [SerializeField] ParticleSystem SuccessParticles;

    // Cache Var for sounds to play
    AudioSource Sounds;

    // state for the object 
    bool IsTransitioning = false;
    bool CollisionDisable = false;

    void Start()
    {
        Sounds = GetComponent<AudioSource>();
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            CollisionDisable = !CollisionDisable; // Disable the debugger
        }
    }

    // Collision Handler
    void OnCollisionEnter(Collision other) 
    {
        if (IsTransitioning || CollisionDisable) {return;};

        switch(other.gameObject.tag)
        {
            case "Friendly" :
                Debug.Log("Friendly");
                break;

            case "Finish" : 
                Debug.Log("Finish"); 
                StartSuccessSequence();
                break;

            // Fuel not added as a feature
            case "Fuel" :
                Debug.Log("Fuel");
                break;

            default :
                Debug.Log("Destroyed spacecraft");
                StartCrashSequence();
                break;

        }   
    }

    // Reload Level function
    void ReloadLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }
    // Next Level function
    void LoadNextLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;
        
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0; // restart Game
        }
        SceneManager.LoadScene(NextSceneIndex);
    }
    // Load next Level success
    void StartSuccessSequence()
    {
        IsTransitioning = true;
        // Stop repeats of sounds
        Sounds.Stop();
        Sounds.PlayOneShot(Success);
        // Particles
        SuccessParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LoadLevelDelay);
    }
    // Crash sequence function for execution
    void StartCrashSequence()
    {
        IsTransitioning = true;
        // Stop repeats of sounds
        Sounds.Stop();
        Sounds.PlayOneShot(Crash);
        // Particles
        CrashParticles.Play();
        // Disable player Control  fds
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LoadLevelDelay); 
    }
}
