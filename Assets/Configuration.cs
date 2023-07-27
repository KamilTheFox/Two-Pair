using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Configuration
{
    private static Configuration instance;
    public static Configuration Instance
    {
        get
        {
            if (instance == null)
                instance = new Configuration();
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private Dictionary<string, Dictionary<string, string>> parameters = new();
    /// <summary>
    /// Read and write configuration
    /// </summary>
    /// <param name="param">Write Path parameters  config. class/param </param>
    /// <returns></returns>
    private string this[string param]
    {
        get
        {
            string[] prt = param.Split('\\');
            if (parameters.TryGetValue(prt[0], out var value))
            { 
                if(value == null)
                    return null;
                if(value.TryGetValue(prt[1], out string valueConf))
                    return valueConf;
            }
            return null;
        }
        set
        {
            string[] prt = param.Split('\\');
            if (!parameters.TryGetValue(prt[0], out var _value))
            {
                parameters.Add(prt[0], new());
            }
            if(_value.ContainsKey(prt[1]))
            {
                _value[prt[1]] = value;
            }
            else
            _value.Add(prt[1], value);
        }
    }
    public static void SetValue(string property, string name, object value)
    {
        Instance[$"{property}\\{name}"] = value.ToString();
    }
    public static bool GetValue<T>(string property, string name, out T value) where T : unmanaged
    {
        value = default(T);
        Type type = typeof(T);
        string stringValue = Instance[$"{property}\\{name}"];
        if(type.IsEnum)
        {
            if(Enum.TryParse<T>(stringValue, out T valueObj))
            {
                value = valueObj;
                return true;
            }
            return false;
        }    
        switch (type.Name)
        {
            case nameof(Int32):
                if(int.TryParse(stringValue,out int objInt))
                {
                    value = (T)(object)objInt;
                    return true;
                }
                return false;
            case nameof(Boolean):
                if (bool.TryParse(stringValue, out bool objBool))
                {
                    value = (T)(object)objBool;
                    return true;
                }
                return false;
            case nameof(UInt16):
                if (bool.TryParse(stringValue, out bool objUInt))
                {
                    value = (T)(object)objUInt;
                    return true;
                }
                return false;
            case nameof(Single):
                if (float.TryParse(stringValue, out float objSingle))
                {
                    value = (T)(object)objSingle;
                    return true;
                }
                return false;
            default:
                if (stringValue == null)
                    return false;
                return false;
        }
    }
    public static string Save()
    {
       return JsonUtility.ToJson(Instance.parameters);
    }
    public static void Load(string loader)
    {
        Instance.parameters = JsonUtility.FromJson<Dictionary<string, Dictionary<string, string>>>(loader);
    }


}
