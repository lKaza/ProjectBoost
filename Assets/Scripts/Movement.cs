using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

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

    [SerializeField] float levelLoadDelay;
    
    
    AudioSource m_MyAudioSource;
    Rigidbody rigidBody;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    bool myCollider = true;
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
        if(Debug.isDebugBuild){
            DebugNextLevel();
            DebugToggleCollisions();
        }
    }

    private void DebugToggleCollisions()
    {
         if(Input.GetKeyDown(KeyCode.C))
        {
            myCollider = false;
        }
    }

    private void DebugNextLevel()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            StarSucessSequence();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (state != State.Alive || myCollider == false) { return; }

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
        Invoke("ReloadScene", levelLoadDelay);
        
    }

    private void StarSucessSequence()
    {     
        clearLevelParticles.Play();
        m_MyAudioSource.Stop();
        m_MyAudioSource.PlayOneShot(clearLevel);        
        state = State.Transcending;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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
        
        rigidBody.AddRelativeForce(Vector3.up * shipThrust * Time.deltaTime);
        Vector3 temp = new Vector3(transform.position.x,transform.position.y,0f);
        transform.position = temp;
        if (!m_MyAudioSource.isPlaying)
        {
            m_MyAudioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
    void Rotate()
    {
        rigidBody.angularVelocity = Vector3.zero;
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        
    }


}


