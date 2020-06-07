using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float shipThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip clearLevel;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem clearLevelParticles;
    
    
    AudioSource m_MyAudioSource;
    Rigidbody rigidBody;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (state != State.Alive) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StarSucessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        m_MyAudioSource.Stop();
        m_MyAudioSource.PlayOneShot(death);
        deathParticles.Play();
        state = State.Dying;
        Invoke("ReloadFirstScene", 1f);
    }

    private void StarSucessSequence()
    {     
        clearLevelParticles.Play();
        m_MyAudioSource.Stop();
        m_MyAudioSource.PlayOneShot(clearLevel);
        
        state = State.Transcending;
        Invoke("LoadNextScene", 1.5f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    private void ReloadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {
            m_MyAudioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    private void ApplyThrust()
    {

        rigidBody.AddRelativeForce(Vector3.up * shipThrust);
        if (!m_MyAudioSource.isPlaying)
        {
            m_MyAudioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
    void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual rotation     
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //resume world rotation
    }


}


