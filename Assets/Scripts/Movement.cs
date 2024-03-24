using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Component Caches
    Rigidbody Rb;
    AudioSource As;
    // Variables for Scripts
    [SerializeField] float MainThrust = 1000f;
    [SerializeField] float RotationThrust = 1f;
    [SerializeField] AudioClip RocketThrust;
    [SerializeField] ParticleSystem MainEngineParticles;
    [SerializeField] ParticleSystem LeftThrusterParticles;
    [SerializeField] ParticleSystem RightThrusterParticles;
    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        As = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Process Input 
    void ProcessThrust()
    {
        // Can be used with c style string name = "Space"
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        Rb.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
        if (!As.isPlaying)
        {
            As.PlayOneShot(RocketThrust);
        }
        if (!MainEngineParticles.isPlaying)
        {
            MainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        As.Stop();
        MainEngineParticles.Stop();
    }

    // Rotation control
    void ProcessRotation()
    {
        // rigid body const add
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotation();
        }
    }

    void StopRotation()
    {
        LeftThrusterParticles.Stop();
        RightThrusterParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(RotationThrust);
        if (!LeftThrusterParticles.isPlaying)
        {
            LeftThrusterParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-RotationThrust);
        if (!RightThrusterParticles.isPlaying)
        {
            RightThrusterParticles.Play();
        }
    }

    void ApplyRotation(float RotationThisFrame)
    {
        Rb.freezeRotation = true; // disable the rotation conflict between controls and engine
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.deltaTime);
        Rb.freezeRotation = false; // disable the rotation conflict between controls and engine
    }
}
