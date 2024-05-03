using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParent : MonoBehaviour
{
    [SerializeField]
    private int hp;

    public int HP
    { get { return hp; } }

    [SerializeField]
    private int mp;

    public int MP
    { get { return mp; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
