using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    Cube cube;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>(); 

        // check if contacted with other cube
        if(otherCube != null && cube.cubeID > otherCube.cubeID)
        {
            //check if both cube have same number
            if(cube.cubeNumber == otherCube.cubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;

                //check if cubes number less than max number in CubeSpawn
                if(otherCube.cubeNumber < CubeSpawner.instance.maxCubeNumber)
                {
                    //spawn a new cube as a result
                    Cube newCube = CubeSpawner.instance.Spawn(cube.cubeNumber*2, contactPoint+Vector3.up*1.6f);

                    // push the new cube up and forward 
                    float pushForce = 2.5f;
                    newCube.cubeRigibody.AddForce(new Vector3(0,.3f,1f)*pushForce,ForceMode.Impulse);

                    // add some torque
                    float ramdomValue = Random.Range(-20f, 20f);
                    Vector3 ramdomDirection = Vector3.one * ramdomValue;
                    newCube.cubeRigibody.AddTorque(ramdomDirection);
          
                }

                //the explosion should affect surround cubes too:
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;

                foreach(Collider coll in surroundedCubes)
                {
                    
                    if(coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce,contactPoint,explosionRadius);
                    }

                    //TODO : explosion FX
                    //FX.instance.PlayCubeExplosionFX(contactPoint, cube.cubeColor);
                    FX.instance.PlayFx(contactPoint, cube.cubeColor);

                    //Destroy the two cubes
                    CubeSpawner.instance.DestroyCube(cube);
                    CubeSpawner.instance.DestroyCube(otherCube);

                }


            }
        }
    }

}
