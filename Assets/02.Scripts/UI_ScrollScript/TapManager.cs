using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapManager : MonoBehaviour
{
    public GameObject[] Tab;
    public Image[] TabBtnImage;
    public Sprite[] IdleSprite, SelectSprite;

    void Start() => TabClick(0);

    public void TabClick(int n)
    {
        for(int i = 0; i < Tab.Length; i++)
        {
            if (Tab[i].activeSelf == true && i == n)
            {
                Tab[i].SetActive(false);
                TabBtnImage[i].sprite = IdleSprite[i];
            }
            else
            {
                Tab[i].SetActive(i == n);
                TabBtnImage[i].sprite = i == n ? SelectSprite[i] : IdleSprite[i];
            }
        }
    }
    
}
