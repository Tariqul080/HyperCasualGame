using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();
        if (cube != null)
        {
            if(cube!= null)
            {
                if (!cube.IsMainCube && cube.CubeRigidbody.velocity.magnitude < .1f)
                {
                    Debug.Log("Game Over");
                }
               
            }
        }
        
    }
}
