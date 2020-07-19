using System;

public static class CLZF2
{
    public static readonly long[] HashTable = new long[HSIZE];
    public static readonly uint HLOG = 14;
    public static readonly uint HSIZE = 0x4000;
    public static readonly uint MAX_LIT = 0x20;
    public static readonly uint MAX_OFF = 0x2000;
    public static readonly uint MAX_REF = 0x108;
}

