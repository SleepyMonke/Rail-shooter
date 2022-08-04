using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
   [SerializeField]GameObject enemyExplosion;
   [SerializeField] GameObject damageExplosion;
   GameObject explosionParent;
   [SerializeField] int enemyHP = 3;
   [SerializeField] int enemyValue = 10;
   ScoreKepper sk;
   void Start()
   {
     sk = FindObjectOfType<ScoreKepper>(); 
     explosionParent = GameObject.FindWithTag("SpawnAtRuntime");
     enemyHP *=15;
     AddRigidbody();
   }
   void OnParticleCollision(GameObject other) 
   {
     DamageEnemy();
     
     if(enemyHP <1)
     {
      DestroyEnemy();
     }
   }

  void AddRigidbody()
  {
     Rigidbody rb= gameObject.AddComponent<Rigidbody>();
     rb.useGravity = false;
  }
   void DamageEnemy()
   {
     enemyHP--;
     GameObject vfx= Instantiate(damageExplosion,transform.position,Quaternion.identity);
     vfx.transform.parent = explosionParent.transform;
   }
   void DestroyEnemy()
   {
    GameObject vfx= Instantiate(enemyExplosion,transform.position,Quaternion.identity);
    vfx.transform.parent = explosionParent.transform;
    sk.IncreaseScore(enemyValue);
    Destroy(gameObject);
   }
}
