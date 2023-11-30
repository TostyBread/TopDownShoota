using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChaser : MonoBehaviour
{
    private Transform Chase; // camera will chase something

    private void Start()
    {
        Chase = GameObject.FindWithTag("Player").transform; // it will declare once to chase anyone with "Player" Tag
    }

    void Update()
    {
        transform.position = Chase.transform.position + new Vector3(0, 1 , -5); // and the camera will chase the "Player" Tag
    }
}
