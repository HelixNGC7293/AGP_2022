using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum
public enum CharacterType {Human, Orc, Elf, Gonlin, Doomslayer, Dog, Cat }

/// <summary>
/// This is a script to show how to organize code.
/// </summary>
public class ScriptOrganization : MonoBehaviour
{




    //Special Classes
    private class CharacterSkill
    {
        public string skillName1 = "Fireball";
        public float skillDamage = 10;
        public float skillRange = 30;
    }






    //Structs
    struct Item
    {
        public int categoryID;
        public string itemName;
    }
    struct Pet
    {
        public CharacterType petType;
        public string petName;

        //Struct Constructor
        public Pet(CharacterType petType, string petName)
        {
            this.petName = petName;
            this.petType = petType;
        }
    }






    //Constants
    public const int SKILLSLOTS = 3;
    public const string LANGUAGE_SETTING = "CN";

    public delegate void GoalScored(int teamID, bool trainingMode = false);
    public GoalScored event_GoalScored;






    //Fields
    [Header("Animator Setting")]
    [Tooltip("Drag the animator to here")]
    [SerializeField]
    private Animator characterAnimator;

    [Header("Character Setting")]
    [Tooltip("Character Speed")]
    [SerializeField]
    private float speed = 2;
    [Tooltip("Total HP")]
    [SerializeField]
    protected float hpTotal = 100;






    //Properties
    [HideInInspector]
    public float secretSpeed = 2;
    private float _hp = 0;
    private float _mp { get; set; }
    int _score;
    public int Score
    {
        get
        {
            Debug.Assert(_score >= 0, "Score should never less than 0");
            return _score;
        }
        set => _score = value;
    }






    //Constructor
    public ScriptOrganization(int mpAmount, int scoreLastRound)
	{
        _score = scoreLastRound;
        _mp = mpAmount;
    }






    #region Static Functions

    //Put your functions here

    public static void GetGameLogic()
    {
        //Execute static content here
    }

    #endregion







    #region Core Functions
    /// <summary>
    /// You know what is this
    /// </summary>
    void Start()
    {
        event_GoalScored += OnGoalScored;

        event_GoalScored.Invoke(0);

        CharacterSkill skill1 = new CharacterSkill();
        skill1.skillName1 = "Magic Storm";

        Item item = new Item();
        item.categoryID = 1;
        item.itemName = "Health Potion";

        Pet pet = new Pet(CharacterType.Cat, "Explosive Kitty");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Two different sets of a function p1
    void GenerateEnemy(CharacterSkill[] skills)
	{

    }

    //Two different sets of a function p2
    void GenerateEnemy(CharacterType enemyType, CharacterSkill[] skills)
    {

    }

    /// <summary>
    /// Explain your function here,
    /// This is second line.
    /// You can add class reference <see cref="Pet(CharacterType, string)"/> here to explain how mutiple classes work together.
    /// </summary>
    /// <param name="targetType1">This is CharacterType</param>
    /// <param name="targetType2">CharacterType 2</param>
    void FunctionWithParameters(CharacterType targetType1, CharacterType targetType2)
    {

    }

    void OnGoalScored(int teamID, bool trainingMode = false)
	{
    }
	#endregion
}
