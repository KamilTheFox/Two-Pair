using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class Configuration
{
    private const string ConfigFileName = @"Assets\Resources\levels.json";
    private static Configuration instance;

    private ConfigurationObject configurationObjects;

    public static Configuration Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Configuration();
                ParseLevelConfig(File.ReadAllText(ConfigFileName));
            }
            return instance;

        }
        private set
        {
            instance = value;
        }
    }

    private Dictionary<string, Dictionary<string, object>> parameters = new();
    /// <summary>
    /// Read and write configuration
    /// </summary>
    /// <param name="param">Write Path parameters  config. class/param </param>
    /// <returns></returns>
    private object this[string param]
    {
        get
        {
            string[] prt = param.ToLower().Split('\\');
            if (parameters.TryGetValue(prt[0], out var value))
            { 
                if(value == null)
                    return null;
                if(value.TryGetValue(prt[1], out object valueConf))
                    return valueConf;
            }
            return null;
        }
        set
        {
            string[] prt = param.ToLower().Split('\\');
            if (!parameters.TryGetValue(prt[0], out var _value))
            {
                parameters.Add(prt[0], new());
            }
            if (_value == null)
                _value = new();
            if (_value.ContainsKey(prt[1]))
            {
                 _value[prt[1]] = value;
            }
            else
                _value.Add(prt[1], value);
        }
    }
    public static void SetValue(string property, string name, object value)
    {
        if (property.Contains("level_"))
            {
                Debug.LogWarning("Not change level_data");
                return;
            }
        Instance[$"{property}\\{name}"] = value;
    }
    public static bool GetValue<T>(string property, string name, out T value)
    {
        string path = $"{property}\\{name}";
        value = default(T);
        Type type = typeof(T);
        object gettedValue = Instance[path];
        if (gettedValue == null)
        {
            Debug.LogWarning($"{path} - is object null");
            return false;
        }
        if (type.IsEnum)
        {
            value = (T)gettedValue;
        }
        if(gettedValue.GetType() != type)
            return false;

        value = (T)gettedValue;

        return true;

    }
   
    public static string Save()
    {
       return JsonUtility.ToJson(Instance.configurationObjects);
    }
    private static void ParseLevelConfig(string JSON_PRT)
    {
        var json = JsonUtility.FromJson<ConfigurationObject>(JSON_PRT);
        Instance.configurationObjects = json;
        int i = 0;
        foreach (Level level in json.levels_data.levels)
        {
            i++;
            Instance[$"level_{i}\\{nameof(Difficulty)}"] = (Difficulty)Enum.Parse(typeof(Difficulty), level.level_type);
            Instance[$"level_{i}\\AddTimeForPair"] = level.give_time_per_pair;
            Instance[$"level_{i}\\TimeRound"] = level.starting_time_sec;
            List<Game_Level.CardInfo> cards = new List<Game_Level.CardInfo>();
            foreach (Card_Sets card in level.card_sets)
            {
                cards.Add(new((TypeCard)Enum.Parse(typeof(TypeCard), card.name_set), card.num_cards));
            }
            Instance[$"level_{i}\\Cards"] = cards;
        }
    }

    [Serializable]
    private struct ConfigurationObject
    {
        public Levels_Data levels_data;
    }
    [Serializable]
    private struct Levels_Data
    {
        public Level[] levels;
    }
    [Serializable]
    private struct Level
    {
        public Card_Sets[] card_sets;
        public int starting_time_sec;
        public int give_time_per_pair;
        public string level_type;
    }
    [Serializable]
    private struct Card_Sets
    {
        public string name_set;
        public int num_cards;
    }
}
