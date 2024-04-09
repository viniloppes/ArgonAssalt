using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
     [SerializeField] private float toutFinaliza = 1f;
    [SerializeField] ParticleSystem crashVFX;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{this.name}--Collides with--{collision.gameObject.name}");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.name}--Triggers with--{other.gameObject.name}");
        StartCrashSequence();
    }
    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke(nameof(ReloadLevel), toutFinaliza);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        GetComponent<PlayerController>().enabled = false;
    }
}
