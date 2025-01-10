using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjects : MonoBehaviour
{
   [SerializeField] GameObject S1,S2,S3; 
 

    [SerializeField] float S1RotationSpeed = 0f;   
    [SerializeField] float S2OrbitSpeed = 0f;
    [SerializeField] float S3OrbitSpeed = 0f;
    [SerializeField] float S2OrbitRadius = 0f;



    void OrbitObjects(float rotationSpeed, float orbitRadius, GameObject pivot, GameObject orbitingObject)
    {
        float angle = rotationSpeed * Time.deltaTime;
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
        Matrix4x4 translationMatrix = Matrix4x4.Translate(new Vector3(orbitRadius, 0, 0));
        Matrix4x4 transformMatrix = Matrix4x4.zero;
        if (pivot != null && rotationMatrix != Matrix4x4.zero && translationMatrix!= Matrix4x4.zero)
        {
            transformMatrix = pivot.transform.localToWorldMatrix * rotationMatrix * translationMatrix;
        }
        else
        {
            orbitingObject.transform.localRotation = Quaternion.LookRotation(Vector3.forward, (orbitingObject.transform.localToWorldMatrix * rotationMatrix).GetColumn(1));
            return;
        }
        orbitingObject.transform.localPosition = transformMatrix.GetColumn(3);
        orbitingObject.transform.localRotation = Quaternion.LookRotation(Vector3.forward, transformMatrix.GetColumn(1));


    }
    private void Update()
    {
        OrbitObjects(S1RotationSpeed, 0f, null, S1);
        OrbitObjects(S2OrbitSpeed, S2OrbitRadius, S1, S2);
        OrbitObjects(S3OrbitSpeed, S2OrbitRadius/2, S2, S3);
    }

}


