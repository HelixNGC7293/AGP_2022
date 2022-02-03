using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Vector2 pitchRange;
    // Start is called before the first frame update
    void Awake()
    {
        Service.AudioInGame = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
