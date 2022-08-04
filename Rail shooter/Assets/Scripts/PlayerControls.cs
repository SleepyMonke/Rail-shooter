using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Movment Variables")]
    [SerializeField]InputAction movment;
    [SerializeField]float moveSpeed = 30f;
    [SerializeField]float xRange= 13.95f;
    [SerializeField]float yRangemax = -70;
    [SerializeField]float yRangemin = -130;

    [Header("Rotation Variables")]
    [SerializeField]float pitchPosition = -0.6f;
    [SerializeField]float pitchControl = -3f;
    [SerializeField]float yawPosition = 0.6f;
    [SerializeField]float rollControl = -20f;
     
     [Header("Laser Variables")]
    [SerializeField]GameObject[] lasers;
    [SerializeField]InputAction fire;
    float horizontalThrow,verticalThrow;

    void OnEnable() 
    {
      movment.Enable();
      fire.Enable();  
    }

    void OnDisable() 
    {
      movment.Disable(); 
      fire.Disable(); 
    }

    void Update()
    {
       ShipTranslation();
       ShipRotation();
       LaserFiring();

    }
    void ShipTranslation()
    {
      horizontalThrow = movment.ReadValue<Vector2>().x; 
      verticalThrow = movment.ReadValue<Vector2>().y;

        float xOffset = horizontalThrow*Time.deltaTime* moveSpeed;
        float yOffset = verticalThrow*Time.deltaTime* moveSpeed;

       float newXpos = transform.localPosition.x + xOffset;
       float clampedXpos = Mathf.Clamp(newXpos,-xRange,xRange);
       float newYpos = transform.localPosition.y + yOffset;
       float clampedYpos = Mathf.Clamp(newYpos,yRangemin,yRangemax);

       transform.localPosition = new Vector3 (clampedXpos, clampedYpos, transform.localPosition.z);
    }
    void ShipRotation()
    {
      float pitch = (transform.localPosition.y+124) *pitchPosition + verticalThrow*pitchControl; // first 2 control direction of rotation last two speed, +124 as initial y pos of ship wrt rig is -124 so we start with 0 for correct direction
      float yaw = (transform.localPosition.x+3) *yawPosition;
      float roll = horizontalThrow*rollControl;

      transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void LaserFiring()
    {
      if(fire.ReadValue<float>() > 0.5)
      {
        SetLasers(true);
      }

      else
      {
        SetLasers(false);
      }
    
    }
    void SetLasers(bool isActive)
    {
      foreach(GameObject laser in lasers)
      {
        var emissionTrigger = laser.GetComponent<ParticleSystem>().emission; //use var if not sure of data type
        emissionTrigger.enabled = isActive;
      }
    }
}
