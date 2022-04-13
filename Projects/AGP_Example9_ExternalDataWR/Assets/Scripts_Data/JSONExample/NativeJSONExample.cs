using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int lives;
    public float health;
    public Skills skills;

    public struct Skills
	{
        public string skill1ID;
        public string skill2ID;
        public string skill3ID;
    }

    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }
    public static void WriteToJSON(string path, PlayerInfo playerInfo)
    {
        string content = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(Application.streamingAssetsPath + path, content);
    }
}

public class NativeJSONExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo newPlayer = new PlayerInfo();
        newPlayer.name = "Bob";
        newPlayer.lives = 10;
        newPlayer.health = 9999;
        newPlayer.skills.skill1ID = "Fireball";
        newPlayer.skills.skill2ID = "Fireball";
        newPlayer.skills.skill3ID = "Fireball";

        PlayerInfo.WriteToJSON("/JSON/Playerinfo.json", newPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
