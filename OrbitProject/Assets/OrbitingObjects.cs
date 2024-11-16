using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjects : MonoBehaviour
{
   [SerializeField] GameObject @object1,@object2,@object3; 
 

    [SerializeField] float rotationSpeedS1 = 50f;   
    [SerializeField] float rotationSpeedS2 = 30f;
    [SerializeField] float rotationSpeedS3 = 20f;
    [SerializeField] float orbitRadiusS2 = 2f;
    [SerializeField] float orbitRadiusS3 = 1f;

    private void MoveObjectOnOrbit(GameObject pivot, GameObject orbitingObject, float orbitRadius, float orbitSpeed)
    { 
    
        float angle = orbitSpeed * Time.deltaTime;
        Matrix4x4 translationMatrix = Matrix4x4.Translate(new Vector3(orbitRadius, 0, 0));
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
        Matrix4x4 transformMatrix;
        if (orbitRadius != 0)
        {
            transformMatrix = pivot.transform.localToWorldMatrix * rotationMatrix * translationMatrix;
        }
        else
        {
            transformMatrix = rotationMatrix;
        }
        orbitingObject.transform.position = transformMatrix.GetColumn(3);
        orbitingObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, transformMatrix.GetColumn(1));

    }


    private void Update()
    {
        MoveObjectOnOrbit(object1,object1, 0, rotationSpeedS1);
        MoveObjectOnOrbit(object1, object2, orbitRadiusS2, rotationSpeedS2);
        MoveObjectOnOrbit(object2, object3, orbitRadiusS3, rotationSpeedS3);
    }
}


