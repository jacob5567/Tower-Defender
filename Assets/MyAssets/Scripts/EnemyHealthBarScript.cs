using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarScript : MonoBehaviour
{

    public Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.down, m_Camera.transform.rotation * Vector3.back);
    }
}
