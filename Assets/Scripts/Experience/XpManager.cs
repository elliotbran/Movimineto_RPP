using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpManager : MonoBehaviour
{
    public float xp;
    public float xpTarget;
    public int playerLevel;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 1;
        xpTarget = 100;
        xp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(xp >= xpTarget)
        {
            playerLevel++;
            xp = 0;
            xpTarget += xpTarget * 0.2f;
        }
    }
}
