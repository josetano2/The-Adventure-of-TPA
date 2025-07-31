using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondRotation : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
