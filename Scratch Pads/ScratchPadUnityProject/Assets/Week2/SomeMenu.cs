using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeMenu : MonoBehaviour
{
    //<TKey, TValue>
    Dictionary<string, int> dict = new Dictionary<string, int>();
    [SerializeField]
    BasicEventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        eventManager.OnGameStart += CloseMainMenu;
        int gameValue1 = 1;
        dict.Add("Holy Sword Attack Damage", gameValue1);
    }

	private void OnDestroy()
    {
    }

    void UseDictionary()
	{
        if (dict["Holy Sword Attack Damage"] == 1)
        {
            //Do something
        }
    }

	void CloseMainMenu()
    {
        Debug.Log("Closing Main Menu");
        eventManager.OnGameStart -= CloseMainMenu;
    }
}
