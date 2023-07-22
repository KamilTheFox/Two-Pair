using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration
{
    private static Configuration instance;
    public static Configuration Instance 
    {
        get
        {
            if(instance == null)
                instance = new Configuration();
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    
    private Dictionary<string, Dictionary<string, string>> parameters = new ();
    /// <summary>
    /// Read and write configuration
    /// </summary>
    /// <param name="param">Write Path parameters  config. class/param </param>
    /// <returns></returns>
    public string this[string param]
        {
            get
            {
                string[] prt = param.Split('/');
                if(parameters.TryGetValue(prt[0],out var value) && value.TryGetValue(prt[1], out string valueConf))
                    return valueConf;
                return null;
            }
            set
            {
                string[] prt = param.Split('/');
                if (parameters.TryGetValue(prt[0], out var _value) && _value.TryGetValue(prt[1], out string valueConf))
                    _value[valueConf] = value;
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
