using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }
  

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }
    void ProcessInput(){
        if(Input.GetKey(KeyCode.Space))
        {
           rigidBody.AddRelativeForce(Vector3.up);
           if(!m_MyAudioSource.isPlaying){
           m_MyAudioSource.Play(); 
           }
        }else{
            m_MyAudioSource.Stop();
        }
        if(Input.GetKey(KeyCode.A) )
        {
            transform.Rotate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    
    }
    
}
