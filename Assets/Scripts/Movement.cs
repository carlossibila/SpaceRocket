using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    [SerializeField] float thrustStrength = 0f;
    [SerializeField] float rotationStrength = 0f;
    [SerializeField] InputAction Thrust;
    [SerializeField] InputAction Rotation;
    [SerializeField] AudioClip SFXMainEngine;
    [SerializeField] ParticleSystem VFXMainEngine;
    [SerializeField] ParticleSystem VFXRightEngine;
    [SerializeField] ParticleSystem VFXLeftEngine;



    Rigidbody rigidBody;
    AudioSource audioSource;


    void OnEnable()
    {
        Thrust.Enable();
        Rotation.Enable();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Thrust.IsPressed())
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(SFXMainEngine);
            }
            if (!VFXMainEngine.isPlaying)
            {
                VFXMainEngine.Play();
            }
        }
        else
        {
            audioSource.Stop();
            VFXMainEngine.Stop();
        }
    }
    private void ProcessRotation()
    {
        float RotationInput = Rotation.ReadValue<float>();
        if (RotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if (!VFXRightEngine.isPlaying)
            {
                 VFXLeftEngine.Stop();
                VFXRightEngine.Play();
            }
        }
        else if (RotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!VFXLeftEngine.isPlaying)
            {
                VFXRightEngine.Stop();
                VFXLeftEngine.Play();
            }
        }
        else
        {
            VFXRightEngine.Stop();
            VFXLeftEngine.Stop();
        }
    }

    private void ApplyRotation(float rotationValue)
    {
        rigidBody.freezeRotation = false;
        transform.Rotate(Vector3.forward * rotationValue * Time.fixedDeltaTime);
        rigidBody.freezeRotation = true;
    }
}
