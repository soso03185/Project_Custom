using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public Sprite image;
    protected Animator m_animator;
    
    public abstract IEnumerator Play(Player playerObject);

}
