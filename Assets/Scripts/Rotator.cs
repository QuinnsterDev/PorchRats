using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
