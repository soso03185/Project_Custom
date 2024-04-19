using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    public GameObject m_player;
    public float m_interpolationRatio = 0.05f;
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
        transform.position = Vector3.Lerp(transform.position, cameraDest, m_interpolationRatio);
    }
}
