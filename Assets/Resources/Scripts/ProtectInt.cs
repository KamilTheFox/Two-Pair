using System;
using System.Runtime;

public class ProtectInt
{
    private byte[] value;
    public ProtectInt(int _value)
    {
        value = BitConverter.GetBytes(_value);
    }
    public int GetValue
    {
        get
        {
            return BitConverter.ToInt32(value, 0);
        }
    }
}
