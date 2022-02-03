using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIPage : MonoBehaviour
{
    [SerializeField]
    BasicEventManager eventManager;
    // Start is called before the first frame update
    public void ButtonPressed_GameStart()
    {
        //Press a button of game start
        eventManager.OnGameStart.Invoke();
    }

}
