using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    private Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        //Check if contocted to other cube;
        if (otherCube != null && cube.cubeID > otherCube.cubeID ) 
        {
            // check if both  cube are same other 
            if (cube.CubeNumber == otherCube.CubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                // Check if cube number less then on max number in cubeSpawner;
                if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    //spawn a new Cube as a result
                    Cube newcube = CubeSpawner.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                    // push the new cube up and forward;
                    float pusForce = 2.5f;
                    newcube.CubeRigidbody.AddForce(new Vector3(0, 0.3f, 1f) * pusForce, ForceMode.Impulse);
                    // odd same torque;
                    float randomvalu = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomvalu;
                    newcube.CubeRigidbody.AddForce(randomDirection);
                }
                //the exploston shold affect surrounded cudes too;
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosinRadious = 1.5f;

                foreach (Collider coll  in surroundedCubes)
                {
                    if(coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosinRadious);
                    }
                }

                // ToDo explosion Fx
                FXScript.Instance.PlayCubeExlosionFX(contactPoint, cube.CubeColor);
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);
            }
        }
    }
}
