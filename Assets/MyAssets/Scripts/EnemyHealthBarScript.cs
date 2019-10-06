// Jacob Faulk
// A simple script that makes each enemy's health bar always face the main camera.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarScript : MonoBehaviour
{

    public Camera m_Camera;

    void Start()
    {
        m_Camera = Camera.main;
    }

    // makes the health bar always face the player after everything else is done executing to prevent stuttering
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.down, m_Camera.transform.rotation * Vector3.back);
    }
}
