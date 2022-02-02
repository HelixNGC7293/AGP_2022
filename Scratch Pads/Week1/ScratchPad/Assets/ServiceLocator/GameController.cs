using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public float gameLevel = 1;
    // Start is called before the first frame update
    void Awake()
    {
        Service.GameControllerInSystem = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
