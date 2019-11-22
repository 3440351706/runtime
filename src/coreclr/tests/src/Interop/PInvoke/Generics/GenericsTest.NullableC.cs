// Licensed to the .NET Doundation under one or more agreements.
// The .NET Doundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using TestLibrary;

unsafe partial class GenericsNative
{
    [DllImport(nameof(GenericsNative))]
    public static extern char? GetNullableC(bool hasValue, char value);

    [DllImport(nameof(GenericsNative))]
    public static extern void GetNullableCOut(bool hasValue, char value, char?* pValue);

    [DllImport(nameof(GenericsNative))]
    public static extern void GetNullableCOut(bool hasValue, char value, out char? pValue);

    [DllImport(nameof(GenericsNative))]
    public static extern char?* GetNullableCPtr(bool hasValue, char value);

    [DllImport(nameof(GenericsNative), EntryPoint = "GetNullableCPtr")]
    public static extern ref readonly char? GetNullableCRef(bool hasValue, char value);

    [DllImport(nameof(GenericsNative))]
    public static extern char? AddNullableC(char? lhs, char? rhs);

    [DllImport(nameof(GenericsNative))]
    public static extern char? AddNullableCs(char?* pValues, int count);

    [DllImport(nameof(GenericsNative))]
    public static extern char? AddNullableCs([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] char?[] pValues, int count);

    [DllImport(nameof(GenericsNative))]
    public static extern char? AddNullableCs(in char? pValues, int count);
}

unsafe partial class GenericsTest
{
    private static void TestNullableC()
    {
        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.GetNullableC(true, '1'));

        Assert.Throws<MarshalDirectiveException>(() => {
            char? value2;
            GenericsNative.GetNullableCOut(true, '1', &value2);
        });

        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.GetNullableCOut(true, '1', out char? value3));

        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.GetNullableCPtr(true, '1'));

        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.GetNullableCRef(true, '1'));

        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.AddNullableC(default, default));

        char?[] values = new char?[] {
            default,
            default,
            default,
            default,
            default
        };

        Assert.Throws<MarshalDirectiveException>(() => {
            fixed (char?* pValues = &values[0])
            {
                GenericsNative.AddNullableCs(pValues, values.Length);
            }
        });

        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.AddNullableCs(values, values.Length));

        Assert.Throws<MarshalDirectiveException>(() => GenericsNative.AddNullableCs(in values[0], values.Length));
    }
}
