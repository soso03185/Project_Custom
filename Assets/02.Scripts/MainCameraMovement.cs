using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    public GameObject m_player;

    private Vector3 m_correction;
    // Start is called before the first frame update
    void Start()
    {
        m_correction = transform.position - m_player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraDest = m_player.transform.position + m_correction;
        transform.position = Vector3.Slerp(transform.position, cameraDest, 0.01f);
    }
}
