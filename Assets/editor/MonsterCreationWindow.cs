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

    //public static MonsterData monsterInfo { get { return monsterInfo; } }

    [MenuItem("Window/Monster Creator")]
    static void OpenWindow()
    {
        MonsterCreationWindow window = (MonsterCreationWindow)GetWindow(typeof(MonsterCreationWindow));
        window.minSize = new Vector2(200, 300);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
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
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Level");
        
        level = EditorGUILayout.IntField(level);
        EditorGUILayout.EndHorizontal();

        if (data == null)
        {
            EditorGUILayout.HelpBox("You need to define the monster species.", MessageType.Warning);
        }
        else if (GUILayout.Button("Create", GUILayout.Height(40)))
        {
            DisplayData.OpenWindow(data, level);

        }
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }
}

public class DisplayData: EditorWindow
{
    Texture2D headerSectionTexture;

    Rect headerSection;
    Rect dataSection;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255, 44f / 255f, 1f);

    static string prefabName = "";

    static MonsterData monsterData;
    static DisplayData window;
    static int monsterLevel;

    static Monster monster;

    public static void OpenWindow(MonsterData data, int level)
    {
        monster = CreateInstance<Monster>();
        monsterData = data;
        monster.data = data;
        monsterLevel = level;
        prefabName = monsterData.getMonsterSpecies.ToString() + "LV_" + monsterLevel;
        window = (DisplayData)GetWindow(typeof(DisplayData));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
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
        DrawMonsterData();
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

    void DrawMonsterData()
    {
        

        GUI.skin.label.alignment = TextAnchor.MiddleLeft;

        GUILayout.BeginArea(dataSection);

        EditorGUILayout.BeginVertical();
        GUILayout.Label(monsterData.getMonsterSpecies);

        //Level
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name: ");
        
        GUILayout.Label(prefabName);
        EditorGUILayout.EndHorizontal();

        //Level
        DisplayStat("Level: ", monsterLevel);

        //HP
        monster.SetHP(CalculateVital(monsterData.getConstitution));
        DisplayStat("HP: ", monster.currentHealth);

        //MP
        monster.SetMana(CalculateStat(monsterData.getWisdom));
        DisplayStat("MP: ", monster.currentMana);

        //Strength
        monster.strength = CalculateStat(monsterData.getStrength);
        DisplayStat("Strength: ", monster.strength);

        //Dexterity
        monster.dexterity = CalculateStat(monsterData.getDexterety);
        DisplayStat("Dexterity: ", monster.dexterity);
        
        if (GUILayout.Button("Create Monster", GUILayout.Height(40)))
        {
            SaveMonsterData();
            window.Close();
        }
        EditorGUILayout.EndVertical();
        

        GUILayout.EndArea();
    }

    void SaveMonsterData()
    {
        string dataPath = "Assets/prefabs/monsters/";
        dataPath += prefabName + ".asset";
        Debug.Log("Path: " + dataPath);
        Debug.Log("Monster: " + monster.ToString());
        AssetDatabase.CreateAsset(monster, AssetDatabase.GenerateUniqueAssetPath(dataPath));
        
    }

    int CalculateVital(int baseStat)
    {
        int statValue = 0;
        
            statValue = baseStat * 10 * monsterLevel;
        
        if (statValue < 10) {
            statValue = 10;
        }

        return statValue;
    }

    int CalculateStat(int baseStat)
    {
        int statValue = 0;

        if (baseStat != 0)
        {
            statValue = baseStat * 2 * monsterLevel;
        }
        else
        {
            statValue = 1;
        }

        return statValue;
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
}