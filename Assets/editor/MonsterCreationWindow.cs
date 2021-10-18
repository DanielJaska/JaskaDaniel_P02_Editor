using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterCreationWindow : EditorWindow
{
    Texture2D headerSectionTexture;

    Color headerSectionColor = new Color(13f/255f, 32f/255, 44f/255f, 1f);

    Rect headerSection;
    Rect dataSection;

    MonsterData data = null;

    GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

    string prefabName = "";
    Monster monster;

    //public static MonsterData monsterInfo { get { return monsterInfo; } }

    [MenuItem("Window/Monster Creator")]
    static void OpenWindow()
    {
        MonsterCreationWindow window = (MonsterCreationWindow)GetWindow(typeof(MonsterCreationWindow));
        window.minSize = new Vector2(300, 300);
        
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
        monster = CreateInstance<Monster>();
    }

    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMonsterSettings();
    }

    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        dataSection.x = 0;
        dataSection.y = 50;
        dataSection.width = Screen.width;
        dataSection.height = Screen.height;

        GUI.DrawTexture(headerSection, headerSectionTexture);
    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Monster Creator");

        GUILayout.EndArea();
    }
    int level = 1;
    void DrawMonsterSettings()
    {
        

        GUILayout.BeginArea(dataSection);

        //GUILayout.Label(monsterInfo.getMonsterSpecies);

        //monsterInfo.element = (MonsterData.Element)EditorGUILayout.EnumPopup(monsterInfo.element);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("MonsterSpecies");
        data = (MonsterData)EditorGUILayout.ObjectField(data, typeof(MonsterData));
        Debug.Log(data);
        //monster.SetupMonster(data, level);
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        if (data == null)
        {
            EditorGUILayout.HelpBox("You need to define the monster species.", MessageType.Warning);
        } else if (data != null)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Level");

            level = EditorGUILayout.IntSlider(level, 1, 100);
            EditorGUILayout.EndHorizontal();

            monster.SetupMonster(data, level);
            prefabName = monster.data.getMonsterSpecies + "Lv" + level.ToString() + "_0";
            DisplayStats();
        }
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void DisplayStats()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Species: " + monster.data.getMonsterSpecies);

        //Level
        EditorGUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUILayout.Label("Prefab Name: ");
        GUI.skin.label.alignment = TextAnchor.MiddleRight;
        GUILayout.Label(prefabName);
        EditorGUILayout.EndHorizontal();

        //Level
        DisplayStat("Level: ", level);

        //HP
        DisplayStat("HP: ", monster.currentHealth);

        //MP
        DisplayStat("MP: ", monster.currentMana);

        //Strength
        DisplayStat("Strength: ", monster.strength);

        //Dexterity
        DisplayStat("Dexterity: ", monster.dexterity);

        if (GUILayout.Button("Create Monster", GUILayout.Height(40)))
        {
            SaveMonsterData();
        }
        EditorGUILayout.EndVertical();

    }

    void DisplayStat(string name, int value)
    {
        EditorGUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUILayout.Label(name);
        GUI.skin.label.alignment = TextAnchor.MiddleRight;
        GUILayout.Label(value.ToString());
        EditorGUILayout.EndHorizontal();
    }

    void SaveMonsterData()
    {
        string dataPath = "Assets/prefabs/monsters/";
        dataPath += prefabName + ".asset";
        AssetDatabase.CreateAsset(monster, AssetDatabase.GenerateUniqueAssetPath(dataPath));
        monster = CreateInstance<Monster>();

    }
}

//public class DisplayData: EditorWindow
//{
//    Texture2D headerSectionTexture;

//    Rect headerSection;
//    Rect dataSection;

//    Color headerSectionColor = new Color(13f / 255f, 32f / 255, 44f / 255f, 1f);



//    static MonsterData monsterData;
//    static DisplayData window;
//    static int monsterLevel;



//    public static void OpenWindow(MonsterData data, int level)
//    {
//        monster = CreateInstance<Monster>();
//        monsterData = data;
//        monster.data = data;
//        monsterLevel = level;
//        prefabName = monsterData.getMonsterSpecies.ToString() + "LV_" + monsterLevel;
//        window = (DisplayData)GetWindow(typeof(DisplayData));
//        window.minSize = new Vector2(250, 200);
//        window.Show();
//    }

//    private void OnEnable()
//    {
//        InitTextures();
//    }

//    void InitTextures()
//    {
//        headerSectionTexture = new Texture2D(1, 1);
//        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
//        headerSectionTexture.Apply();
//    }

//    private void OnGUI()
//    {
//        DrawLayouts();
//        DrawHeader();
//        DrawMonsterData();
//    }

//    void DrawLayouts()
//    {
//        headerSection.x = 0;
//        headerSection.y = 0;
//        headerSection.width = Screen.width;
//        headerSection.height = 50;

//        dataSection.x = 0;
//        dataSection.y = 50;
//        dataSection.width = Screen.width;
//        dataSection.height = Screen.height;

//        GUI.DrawTexture(headerSection, headerSectionTexture);
//    }

//    void DrawHeader()
//    {
//        GUILayout.BeginArea(headerSection);

//        GUILayout.Label("Monster Creator");

//        GUILayout.EndArea();
//    }

//    void DrawMonsterData()
//    {


//        GUI.skin.label.alignment = TextAnchor.MiddleLeft;

//        GUILayout.BeginArea(dataSection);

//        EditorGUILayout.BeginVertical();
//        GUILayout.Label(monsterData.getMonsterSpecies);

//        //Level
//        EditorGUILayout.BeginHorizontal();
//        GUILayout.Label("Name: ");

//        GUILayout.Label(prefabName);
//        EditorGUILayout.EndHorizontal();

//        //Level
//        DisplayStat("Level: ", monsterLevel);

//        //HP
//        monster.SetHP(CalculateVital(monsterData.getConstitution));
//        DisplayStat("HP: ", monster.currentHealth);

//        //MP
//        monster.SetMana(CalculateStat(monsterData.getWisdom));
//        DisplayStat("MP: ", monster.currentMana);

//        //Strength
//        monster.strength = CalculateStat(monsterData.getStrength);
//        DisplayStat("Strength: ", monster.strength);

//        //Dexterity
//        monster.dexterity = CalculateStat(monsterData.getDexterety);
//        DisplayStat("Dexterity: ", monster.dexterity);

//        if (GUILayout.Button("Create Monster", GUILayout.Height(40)))
//        {
//            SaveMonsterData();
//            window.Close();
//        }
//        EditorGUILayout.EndVertical();


//        GUILayout.EndArea();
//    }


//}