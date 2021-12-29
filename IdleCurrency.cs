using System;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// Data type to use in Idle Games
/// </summary>

[System.Serializable]
public struct IdleCurrency
{
    public double Value;
    public int Exp;

    //public IdleCurrency()
    //{
    //    Value = 0;
    //    Exp = 0;
    //}

    public IdleCurrency(double d)
    {
        Value = 0;
        Exp = 0;
        ConvertFromDouble(d);
    }

    public IdleCurrency(byte value) : this((double)value)
    {
    }

    public IdleCurrency(bool value)
    {
        this.Exp = 0;
        this.Value = value ? 1 : 0;
    }

    public IdleCurrency(char value) : this((double)value)
    {
    }

    public IdleCurrency(decimal value) : this((double)value)
    {
    }

    public IdleCurrency(float value) : this((double)value)
    {
    }

    public IdleCurrency(short value) : this((double)value)
    {
    }

    public IdleCurrency(int value) : this((double)value)
    {
    }

    public IdleCurrency(long value) : this((double)value)
    {
    }

    public IdleCurrency(sbyte value) : this((double)value)
    {
    }

    public IdleCurrency(ushort value) : this((double)value)
    {
    }

    public IdleCurrency(uint value) : this((double)value)
    {
    }

    public IdleCurrency(ulong value) : this((double)value)
    {
    }

    public IdleCurrency(double value, int exp)
    {
        Value = value;
        Exp = exp;
    }

    public double RealValue
    {
        get
        {
            if (this.Exp < 308)
            {
                double expandedPower = Math.Pow(10.0, (ulong)this.Exp);
                double realValue = this.Value * expandedPower;
                return realValue;
            }
            else
            {
                return double.MaxValue;
            }
        }
    }

    void ConvertFromDouble(double d)
    {
        int newExp = 0;
        double newValue = d;
        while (newValue >= 1.0)
        {
            newValue /= 10.0;
            newExp++;
        }

        Value = newValue;
        Exp = newExp;
    }

    void Simplify()
    {

        if (Value <= 0)
        {
            Value = 0;
            Exp = 0;
        }
        else
        {
            if (Value >= 1.0)
            {
                int newExp = Exp;
                double newValue = Value;

                while (newValue >= 1.0)
                {
                    newValue /= 10.0;
                    newExp++;
                }

                Value = newValue;
                Exp = newExp;
            }
            else if (Value < 0.1 && Value >= 0.0)
            {
                int newExp = Exp;
                double newValue = Value;

                while (newValue < 0.1)
                {
                    newValue *= 10.0;
                    newExp--;
                }

                Value = newValue;
                Exp = newExp;
            }
        }
    }

    public static IdleCurrency operator +(IdleCurrency a, long b)
    {
        return a + new IdleCurrency((float)b);
    }

    public static IdleCurrency operator +(IdleCurrency a, IdleCurrency b)
    {
        if (a.Exp == b.Exp)
        {
            IdleCurrency m = new IdleCurrency();
            m.Exp = a.Exp;
            m.Value = a.Value + b.Value;
            m.Simplify();
            return m;
        }
        else if (a.Exp > b.Exp)
        {
            // a is bigger
            int deltaExp = a.Exp - b.Exp;
            if (deltaExp <= 16)
            {
                double bX = b.Value / Math.Pow(10, (double)deltaExp);
                IdleCurrency m = new IdleCurrency();
                m.Exp = a.Exp;
                m.Value = a.Value + bX;
                m.Simplify();
                return m;
            }
            else
            {
                return a;
            }
        }
        else
        {
            // b is bigger
            int deltaExp = b.Exp - a.Exp;
            if (deltaExp <= 16)
            {
                double aX = a.Value / Math.Pow(10, (double)deltaExp);
                IdleCurrency m = new IdleCurrency();
                m.Exp = b.Exp;
                m.Value = b.Value + aX;
                m.Simplify();
                return m;
            }
            else
            {
                return b;
            }
        }
    }


    public static IdleCurrency operator +(IdleCurrency a, float b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a + B);
    }

    public static IdleCurrency operator +(float a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A + b);
    }

    public static IdleCurrency operator -(IdleCurrency a, float b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a - B);
    }

    public static IdleCurrency operator -(float a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A - b);
    }

    public static IdleCurrency operator *(IdleCurrency a, float b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a * B);
    }

    public static IdleCurrency operator *(float a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A * b);
    }

    public static IdleCurrency operator /(IdleCurrency a, float b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a / B);
    }

    public static IdleCurrency operator /(float a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A / b);
    }

    public static IdleCurrency operator +(IdleCurrency a, int b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a + B);
    }

    public static IdleCurrency operator +(int a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A + b);
    }

    public static IdleCurrency operator -(IdleCurrency a, int b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a - B);
    }

    public static IdleCurrency operator -(int a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A - b);
    }

    public static IdleCurrency operator *(IdleCurrency a, int b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a * B);
    }

    public static IdleCurrency operator *(int a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A * b);
    }

    public static IdleCurrency operator /(IdleCurrency a, int b)
    {
        IdleCurrency B = new IdleCurrency(b);
        return (a / B);
    }

    public static IdleCurrency operator /(int a, IdleCurrency b)
    {
        IdleCurrency A = new IdleCurrency(a);
        return (A / b);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Negate()
    {
        Value = -Value;
    }

    public static int Compare(IdleCurrency left, object right)
    {
        if (right is IdleCurrency)
        {
            return Compare(left, (IdleCurrency)right);
        }

        if (right is bool)
        {
            return Compare(left, new IdleCurrency((bool)right));
        }

        if (right is byte)
        {
            return Compare(left, new IdleCurrency((byte)right));
        }

        if (right is char)
        {
            return Compare(left, new IdleCurrency((char)right));
        }

        if (right is decimal)
        {
            return Compare(left, new IdleCurrency((decimal)right));
        }

        if (right is double)
        {
            return Compare(left, new IdleCurrency((double)right));
        }

        if (right is short)
        {
            return Compare(left, new IdleCurrency((short)right));
        }

        if (right is int)
        {
            return Compare(left, new IdleCurrency((int)right));
        }

        if (right is long)
        {
            return Compare(left, new IdleCurrency((long)right));
        }

        if (right is sbyte)
        {
            return Compare(left, new IdleCurrency((sbyte)right));
        }

        if (right is float)
        {
            return Compare(left, new IdleCurrency((float)right));
        }

        if (right is ushort)
        {
            return Compare(left, new IdleCurrency((ushort)right));
        }

        if (right is uint)
        {
            return Compare(left, new IdleCurrency((uint)right));
        }

        if (right is ulong)
        {
            return Compare(left, new IdleCurrency((ulong)right));
        }

        throw new ArgumentException();
    }

    public int Sign
    {
        get
        {
            if (this.Value == 0)
            {
                return 0;
            }

            return Value > 0.0 ? 1 : -1;
        }
    }

    public static int Compare(IdleCurrency left, IdleCurrency right)
    {
        left.Format();
        right.Format();
        int leftSign = left.Sign;
        int rightSign = right.Sign;

        // Compare signs
        if (leftSign == 0 && rightSign == 0)
        {
            return 0;
        }

        if (leftSign >= 0 && rightSign < 0)
        {
            return 1;
        }

        if (leftSign < 0 && rightSign >= 0)
        {
            return -1;
        }

        // Compare exponents
        if (left.Exp > right.Exp)
        {
            return 1;
        }

        if (left.Exp < right.Exp)
        {
            return -1;
        }

        return left.Value.CompareTo(right.Value);
    }


    public override int GetHashCode()
    {
        return this.Value.GetHashCode() ^ this.Exp.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public int CompareTo(IdleCurrency value)
    {
        return Compare(this, value);
    }

    public IdleCurrency ToAbs()
    {
        return Abs(this);
    }

    public static IdleCurrency Abs(IdleCurrency value)
    {
        if (value.Sign < 0)
        {
            return -value;
        }

        return value;
    }

    public static IdleCurrency Add(IdleCurrency left, IdleCurrency right)
    {
        return left + right;
    }

    public static IdleCurrency Subtract(IdleCurrency left, IdleCurrency right)
    {
        return left - right;
    }

    public static IdleCurrency Divide(IdleCurrency dividend, IdleCurrency divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException();
        }

        IdleCurrency bd = new IdleCurrency(
            dividend.Value / divisor.Value,
            dividend.Exp - divisor.Exp);
        bd.Simplify();
        return bd;
    }

    public static IdleCurrency Multiply(IdleCurrency left, IdleCurrency right)
    {
        IdleCurrency bd = new IdleCurrency(
            left.Value * right.Value,
            left.Exp + right.Exp);
        bd.Simplify();
        return bd;
    }

    public static implicit operator IdleCurrency(bool value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(byte value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(char value)
    {
        return new IdleCurrency(value);
    }

    public static explicit operator IdleCurrency(decimal value)
    {
        return new IdleCurrency(value);
    }

    public static explicit operator IdleCurrency(double value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(short value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(int value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(long value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(sbyte value)
    {
        return new IdleCurrency(value);
    }

    public static explicit operator IdleCurrency(float value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(ushort value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(uint value)
    {
        return new IdleCurrency(value);
    }

    public static implicit operator IdleCurrency(ulong value)
    {
        return new IdleCurrency(value);
    }

    public static explicit operator bool(IdleCurrency value)
    {
        return value.Sign != 0;
    }

    public static explicit operator byte(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if ((value.RealValue < byte.MinValue) || (value.RealValue > byte.MaxValue))
        {
            throw new OverflowException();
        }

        return (byte)value.RealValue;
    }

    public static explicit operator char(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return (char)0;
        }

        if ((value.RealValue < char.MinValue) || (value.RealValue > char.MaxValue))
        {
            throw new OverflowException();
        }

        return (char)(ushort)value.RealValue;
    }

    public static explicit operator double(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if (value.Exp > 307)
        {
            throw new OverflowException();
        }

        return value.RealValue;
    }

    public static explicit operator float(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if (value.Exp > 37)
        {
            throw new OverflowException();
        }

        return (float)value.RealValue;
    }

    public static explicit operator short(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if ((value.RealValue < short.MinValue) || (value.RealValue > short.MaxValue))
        {
            throw new OverflowException();
        }

        return (short)value.RealValue;
    }

    public static explicit operator int(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if ((value.RealValue < int.MinValue) || (value.RealValue > int.MaxValue))
        {
            throw new OverflowException();
        }

        return ((int)value.RealValue);
    }

    public static explicit operator long(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if ((value.RealValue < long.MinValue) || (value.RealValue > long.MaxValue))
        {
            throw new OverflowException();
        }

        return (long)value.RealValue;
    }

    public static explicit operator uint(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if ((value.RealValue < uint.MinValue) || (value.RealValue > uint.MaxValue))
        {
            throw new OverflowException();
        }

        return (uint)value.RealValue;
    }

    public static explicit operator ushort(IdleCurrency value)
    {
        if (value.Sign == 0)
        {
            return 0;
        }

        if ((value.RealValue < ushort.MinValue) || (value.RealValue > ushort.MaxValue))
        {
            throw new OverflowException();
        }

        return (ushort)value.RealValue;
    }

    public static explicit operator ulong(IdleCurrency value)
    {
        if ((value.RealValue < ulong.MinValue) || (value.RealValue > ulong.MaxValue))
        {
            throw new OverflowException();
        }

        return (ulong)value.RealValue;
    }

    public static bool operator >(IdleCurrency left, IdleCurrency right)
    {
        return Compare(left, right) > 0;
    }

    public static bool operator <(IdleCurrency left, IdleCurrency right)
    {
        return Compare(left, right) < 0;
    }

    public static bool operator >=(IdleCurrency left, IdleCurrency right)
    {
        return Compare(left, right) >= 0;
    }

    public static bool operator <=(IdleCurrency left, IdleCurrency right)
    {
        return Compare(left, right) <= 0;
    }

    public static bool operator !=(IdleCurrency left, IdleCurrency right)
    {
        return Compare(left, right) != 0;
    }

    public static bool operator ==(IdleCurrency left, IdleCurrency right)
    {
        return Compare(left, right) == 0;
    }

    public static IdleCurrency operator +(IdleCurrency value)
    {
        return value;
    }

    public static IdleCurrency operator -(IdleCurrency value)
    {
        value.Negate();
        return value;
    }

    public static IdleCurrency operator -(IdleCurrency left, IdleCurrency right)
    {
        return left + -right;
    }

    public static IdleCurrency operator /(IdleCurrency dividend, IdleCurrency divisor)
    {
        return Divide(dividend, divisor);
    }

    public static IdleCurrency operator *(IdleCurrency left, IdleCurrency right)
    {
        return Multiply(left, right);
    }

    public static IdleCurrency operator ++(IdleCurrency value)
    {
        value.Value++;
        return value;
    }

    public static IdleCurrency operator --(IdleCurrency value)
    {
        value.Value--;
        return value;
    }

    public void Format()
    {
        IdleCurrency r = new IdleCurrency(Value, Exp);
        r = Format(r);
        Value = r.Value;
        Exp = r.Exp;
    }

    public static IdleCurrency Format(IdleCurrency a)
    {
        IdleCurrency r = a;
        int p = r.Exp;
        double rV = r.Value;

        if (rV < 1 && p <= 3)
        {
            rV = rV * Math.Pow(10, (double)p);
            p = 0;
        }

        while (rV < 100 && p > 3)
        {
            rV = rV * 10;
            p--;
        }

        if (p < 3)
        {
            rV = rV * Math.Pow(10, (double)p);
            p = 0;
            if (rV >= 1000)
            {
                rV = rV / 1000;
                p += 3;
            }
        }
        else
        {
            if (p % 3 != 0)
            {
                rV = rV * Math.Pow(10, (double)(p % 3));
                p -= (p % 3);
            }

            if (rV >= 1000)
            {
                rV = rV / 1000;
                p += 3;
            }
        }

        r.Value = rV;
        r.Exp = p;

        return r;
    }


    public string ToShortString(bool isForceDecimal = false, string format = "#.##")
    {
        string[] PremitiveAlphapets = { "K", "M", "B", "T" };
        string[] FutureAlphapets = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        Format();
        string result = "";
        int index = 0;

        //if (Exp < 3)
        //{

        if (Exp == 0 && Value < 0.001)
        {
            result = "0";
        }
        else
        {
            if (!isForceDecimal)
            {
                if (Exp > 2 || Value > 999)
                    result = Value.ToString(format);
                else
                    result = Value.ToString("#");
            }
            else
            {
                result = Value.ToString(format);
            }
            //}
            if (Exp < 3)
            {
            }
            else if (Exp >= 3 && Exp < 15)
            {
                index = (int)(Exp / 3);
                result += PremitiveAlphapets[index - 1];
            }
            else
            {
                int tExp = (int)(Exp - 15);
                string tS = "";

                //0-77
                if (tExp < 78)
                {
                    tS += FutureAlphapets[0] + FutureAlphapets[tExp / 3];
                }
                //78-155
                else if (tExp < 78 * 2)
                {
                    tExp = tExp - 78;
                    tS += FutureAlphapets[1] + FutureAlphapets[tExp / 3];

                }
                //156-233
                else if (tExp < 78 * 3)
                {
                    tExp = tExp - (78 * 2);
                    tS += FutureAlphapets[2] + FutureAlphapets[tExp / 3];

                }
                //234-311
                else if (tExp < 78 * 4)
                {
                    tExp = tExp - (78 * 3);
                    tS += FutureAlphapets[3] + FutureAlphapets[tExp / 3];

                }
                //312-390
                else if (tExp < 78 * 5)
                {
                    tExp = tExp - (78 * 4);
                    tS += FutureAlphapets[4] + FutureAlphapets[tExp / 3];

                }
                //391-467
                else if (tExp < 78 * 6)
                {
                    tExp = tExp - (78 * 5);
                    tS += FutureAlphapets[5] + FutureAlphapets[tExp / 3];

                }
                else
                {
                    tS = "+e" + (tExp + 15).ToString();
                }
                result += tS;
            }
        }
        return result;
    }

    public string GetShortStringSuffix(string format = "#.##")
    {
        string[] PremitiveAlphapets = { "K", "M", "B", "T" };
        string[] FutureAlphapets = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        Format();
        string suffix = "";
        int index = 0;

        //if (Exp < 3)
        //{

        if (Exp == 0 && Value < 0.001)
        {
            //result = "0";
        }
        else
        {

            //result = Value.ToString(format);
            //}
            if (Exp < 3)
            {
            }
            else if (Exp >= 3 && Exp < 15)
            {
                index = (int)(Exp / 3);
                suffix += PremitiveAlphapets[index - 1];
            }
            else
            {
                int tExp = (int)(Exp - 15);
                string tS = "";

                //0-77
                if (tExp < 78)
                {
                    tS += FutureAlphapets[0] + FutureAlphapets[tExp / 3];
                }
                //78-155
                else if (tExp < 78 * 2)
                {
                    tExp = tExp - 78;
                    tS += FutureAlphapets[1] + FutureAlphapets[tExp / 3];

                }
                //156-233
                else if (tExp < 78 * 3)
                {
                    tExp = tExp - (78 * 2);
                    tS += FutureAlphapets[2] + FutureAlphapets[tExp / 3];

                }
                //234-311
                else if (tExp < 78 * 4)
                {
                    tExp = tExp - (78 * 3);
                    tS += FutureAlphapets[3] + FutureAlphapets[tExp / 3];

                }
                //312-390
                else if (tExp < 78 * 5)
                {
                    tExp = tExp - (78 * 4);
                    tS += FutureAlphapets[4] + FutureAlphapets[tExp / 3];

                }
                //391-467
                else if (tExp < 78 * 6)
                {
                    tExp = tExp - (78 * 5);
                    tS += FutureAlphapets[5] + FutureAlphapets[tExp / 3];

                }
                else
                {
                    tS = "+e" + (tExp + 15).ToString();
                }
                suffix += tS;
            }
        }
        return suffix;
    }


    public string ToLongString()
    {
        string[] PremitiveSuffixes = { "Thousand", "Million", "Billion", "Trillion" };
        string[] FutureSuffixes = { "Quadrillion","Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion",
            "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion",
            "Vigintillion", "Unvigintillion", "Duovigintillion", "Trevigintillion","Quattuorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septenvigintillion", "Octovigintillion", "Novemvigintillion",
        "Trigintillion","Untrigintillion","Duotrigintillion","Tretrigintillion","Quattuortrigintillion","Quintrigintillion","Sextrigintillion","Septentrigintillion","Octotrigintillion","Novemtrigintillion",
            "Quadragintillion","Unquadragintillion","Duoquadragintillion","Trequadragintillion","Quattuorquadragintillion","Quinquadragintillion","Sexquadragintillion","Septenquadragintillion","Octaquadragintillion","Novemquadragintillion",
        "Quinragintillion","Unquinquagintillion","Duoquinquagintillion","Trequinquagintillion","Quattuorquinquagintillion","Quinquinquagintillion","Sexquinquagintillion","Septenquinquagintillion","Octoquinquagintillion","Novemquinquagintillion",
        "Sexagintillion","Unsexagintillion","Duosexagintillion","Tresexagintillion","Quattuorsexagintillion","Quinsexagintillion","Sexsexagintillion","Septsexagintillion","Octosexagintillion","Novemsexagintillion",
        "Septuagintillion","Unseptuagintillion","Duoseptuagintillion","Treseptuagintillion","Quattuorseptuagintillion","Quinseptuagintillion","Sexseptuagintillion","Septseptuagintillion","Octoseptuagintillion","Novemseptuagintillion"};

        Format();
        string result = "";
        int index = 0;

        //if (Exp < 3)
        //{

        if (Exp == 0 && Value < 0.001)
        {
            result = "0";
        }
        else
        {

            result = Value.ToString("#.##");
            //}
            if (Exp < 3)
            {
            }
            else if (Exp >= 3 && Exp < 15)
            {
                index = (int)(Exp / 3);
                result += " " + PremitiveSuffixes[index - 1];
            }
            else
            {
                int tExp = (int)(Exp - 15);
                string tS = "";

                //0-77
                if (tExp < 226)
                {
                    tS += " " + FutureSuffixes[tExp / 3];
                }
                else
                {
                    tS = "+e" + (tExp + 15).ToString();
                }
                result += tS;
            }
        }
        return result;
    }

    public string ToExponentForm()
    {
        string[] PremitiveSuffixes = { "Thousand", "Million", "Billion", "Trillion" };

        Format();
        string result = "";
        int index = 0;

        //if (Exp < 3)
        //{

        if (Exp == 0 && Value < 0.001)
        {
            result = "0";
        }
        else
        {

            result = Value.ToString("#.##");
            string tS = "";
            //}
            if (Exp >= 3)
            {
                tS = "+e" + (Exp).ToString();
            }
            result += tS;
        }
        return result;
    }

    public string GetStringForSave()
    {
        string s;
        s = Value.ToString() + "," + Exp.ToString();
        return s;
    }

    public void SetValueFromString(string s)
    {
        string[] split = s.Split(',');
        Value = double.Parse(split[0]);
        if (split.Length > 1)
            Exp = int.Parse(split[1]);
        else
            Exp = 0;
    }

    public float GetFloat()
    {
        return (float)(Value * Math.Pow(10f, Exp));
    }

    public static IdleCurrency GetIdleCurrency(string key)
    {
        return GetIdleCurrency(key, new IdleCurrency(0, 0));
    }
    public static IdleCurrency GetIdleCurrency(string key,float defaultValue)
    {
        return GetIdleCurrency(key, new IdleCurrency(defaultValue));
    }

    public static IdleCurrency GetIdleCurrency(string key, IdleCurrency defaultValue)
    {
        string data = PlayerPrefs.GetString(key, string.Empty);
        if (!string.IsNullOrEmpty(data) && data.Contains("-"))
        {
            try
            {
                var part = data.Split('-');

                double valuePart = double.Parse(part[0]);
                int expPart = int.Parse(part[1]);
                try
                {
                    if (valuePart < 0)
                        valuePart = 0;
                }
                catch (System.Exception e)
                {
                    valuePart = 0;
                }
                try
                {
                    if (expPart < 0)
                        expPart = 0;
                }
                catch (System.Exception e)
                {
                    expPart = 0;
                }
                return new IdleCurrency(valuePart, expPart);
            }
            catch (System.Exception e)
            {
                Debug.Log("Caught Exception --> " + data);
            }
            return defaultValue;
        }
        else
        {
            return defaultValue;
        }
    }

    public static void SetIdleCurrency(string key, IdleCurrency value)
    {
        if (value.Exp < 0)
        {
            value.Exp = 0;
        }
        if (value.Value < 0)
        {
            value.Value = 0;
        }
        PlayerPrefs.SetString(key, value.Value.ToString() + "-" + value.Exp.ToString());
    }

}
