using System;
using System.Runtime;

public class ProtectInt
{
    private byte[] value;
    public ProtectInt(int _value)
    {
        value = BitConverter.GetBytes(_value);
    }
    private int GetValue
    {
        get
        {
            return BitConverter.ToInt32(value, 0);
        }
    }
    public static implicit operator ProtectInt(int value)
    {
        return new ProtectInt(value);
    }
    public static implicit operator int(ProtectInt value)
    {
        if(value == null)
        {
            value = 0;
        }
        return value.GetValue;
    }
}
