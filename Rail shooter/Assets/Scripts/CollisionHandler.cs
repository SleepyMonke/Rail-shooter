using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float loadDelay = 1f;
  [SerializeField] ParticleSystem explosion;
  void OnCollisionEnter(Collision other) 
  {
   CrashSequence();
  }

  void CrashSequence()
  {
    explosion.Play();
    foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
           {
              child.enabled = false;
           }
   GetComponent<PlayerControls>().enabled = false;
   Invoke("ReloadLevel",loadDelay);
  }
  void ReloadLevel()
  {
   int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
   SceneManager.LoadScene(currentSceneIndex);
  }
}
