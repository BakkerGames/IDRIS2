// mempos.cs - 07/13/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class MemPos
    {

        // --------------------------------------------------------
        // --- Memory Map (numbers are in decimal):             ---
        // --- Page      0: System Variables                    ---
        // --- Pages  1- 2: Numeric Variables                   ---
        // --- Pages  2- 3: Alpha Variables A, B, C (Start=234) ---
        // --- Pages  4- 8: Buffers R, Z, X, Y, W               ---
        // --- Pages  9-10: File Table (32 entries)             ---
        // --- Pages 11-14: Buffers S, T, U, V                  ---
        // --- Page     15: Alpha Variables D, E                ---
        // --- Pages 16-47: Track Buffer                        ---
        // --- Page     48: Device Table (32 entries)           ---
        // --- Pages 48-50: Volume Table (64 entries, Start=32) ---
        // --- Pages 51-52: TFA Table (32 entries)              ---
        // --- Pages 53-54: Channel Table (128 entries)         ---
        // --- Page     55: High Numeric Variables N64-N99      ---
        // --- Pages 56-58: High Alpha Vars A3-A9 to E3-E9      ---
        // --------------------------------------------------------

        // --------------------------------------------------------
        // --- File table record layout (16 bytes):             ---
        // ---    Ofs 0: Open (True/False)                      ---
        // ---    Ofs 1: Volume Number                          ---
        // ---    Ofs 2: Type (2=Data, 5=Directory, 6=Pseudo)   ---
        // ---    Ofs 3: Name (a8)                              ---
        // --------------------------------------------------------
        // --- Device table record layout (1 byte):             ---
        // ---    Ofs 0: Open (True/False)                      ---
        // --------------------------------------------------------
        // --- Volume table record layout (10 bytes):           ---
        // ---    Ofs 0: Open (True/False)                      ---
        // ---    Ofs 1: Device Number                          ---
        // ---    Ofs 2: Name (a8)                              ---
        // --------------------------------------------------------
        // --- TFA table record layout (10 bytes):              ---
        // ---    Ofs 0: Open (True/False)                      ---
        // ---    Ofs 1: Volume Number                          ---
        // ---    Ofs 2: Name (a8)                              ---
        // --------------------------------------------------------
        // --- Channel table record layout (3 bytes):           ---
        // ---    Ofs 0: Open (True/False)                      ---
        // ---    Ofs 1: TFA Number                             ---
        // ---    Ofs 2: Channel Locked (True/False)            ---
        // ---    Channel path is stored in ChannelPaths().     ---
        // --------------------------------------------------------

        // --- Table Starting Positions ---

        public const int filetable = 9 * 256;
        public const int trackbuffer = 16 * 256;
        public const int devtable = 48 * 256;
        public const int voltable = devtable + 32;
        public const int tfatable = 51 * 256;
        public const int chantable = 53 * 256;
        public const int numhigh = 55 * 256;
        public const int highalpha = 56 * 256;
        public const int totalpagecount = 59;
        public const int totalmemsize = totalpagecount * 256;

        // --- Table Record Sizes ---

        public const int fileentrysize = 16;
        public const int deventrysize = 1;
        public const int volentrysize = 10;
        public const int tfaentrysize = 10;
        public const int chanentrysize = 3;

        // --- Highest Table Entry numbers ---

        public const int maxfile = 31;
        public const int maxdevice = 31;
        public const int maxvolume = 63;
        public const int maxtfa = 31;
        public const int maxchannel = 127;

        // --- Buffer Pointers ---

        public const int rp = 0; // x00
        public const int rp2 = 1; // x01
        public const int irp = 2; // x02
        public const int irp2 = 3; // x03
        public const int zp = 4; // x04
        public const int zp2 = 5; // x05
        public const int izp = 6; // x06
        public const int izp2 = 7; // x07
        public const int xp = 8; // x08
        public const int xp2 = 9; // x09
        public const int ixp = 10; // x0a
        public const int ixp2 = 11; // x0b
        public const int yp = 12; // x0c
        public const int yp2 = 13; // x0d
        public const int iyp = 14; // x0e
        public const int iyp2 = 15; // x0f
        public const int wp = 16; // x10
        public const int wp2 = 17; // x11
        public const int iwp = 18; // x12
        public const int iwp2 = 19; // x13

        public const int sp = 112; // x70
        public const int sp2 = 113; // x71
        public const int isp = 114; // x72
        public const int isp2 = 115; // x73
        public const int tp = 116; // x74
        public const int tp2 = 117; // x75
        public const int itp = 118; // x76
        public const int itp2 = 119; // x77
        public const int up = 120; // x78
        public const int up2 = 121; // x79
        public const int iup = 122; // x7a
        public const int iup2 = 123; // x7b
        public const int vp = 124; // x7c
        public const int vp2 = 125; // x7d
        public const int ivp = 126; // x7e
        public const int ivp2 = 127; // x7f

        // --- Byte System Variables ---

        public const int lib = 20; // x14
        public const int prog = 21; // x15-x16
        public const int privg = 46; // x2e
        public const int charval = 48; // x30
        public const int length = 49; // x31
        public const int status = 50; // x32
        public const int esc = 51; // x33
        public const int can = 52; // x34
        public const int lockval = 53; // x35
        public const int tchan = 54; // x36
        public const int term = 73; // x49
        public const int lang = 76; // x4c
        public const int prtnum = 77; // x4d
        public const int tfa = 89; // x59
        public const int vol = 90; // x5a
        public const int pvol = 91; // x5b
        public const int reqvol = 103; // x67

        // --- 2-Byte System Variables ---

        public const int user = 102; // x66-x67
        public const int orig = 104; // x68-x69
        public const int oper = 106; // x6a-x6b

        // --- Read-Only Byte System Variables ---

        public const int machtype = 109; // x6d  = public constant 21 in idris2;
        public const int sysrel = 110; // x6e  = public constant 5 in idris2;
        public const int sysrev = 111; // x6f  = public constant 1 in idris2;

        // --- Flag registers ---

        public const int f = 92; // x5c
        public const int f1 = 93; // x5d
        public const int f2 = 94; // x5e
        public const int f3 = 95; // x5f
        public const int f4 = 96; // x60
        public const int f5 = 97; // x61
        public const int f6 = 98; // x62
        public const int f7 = 99; // x63
        public const int f8 = 100; // x64
        public const int f9 = 101; // x65
        public const int minflow = 0;
        public const int maxflow = 9;

        public const int f10 = 160; // xa0
        public const int f11 = 161; // xa1
        public const int f12 = 162; // xa2
        public const int f13 = 163; // xa3
        public const int f14 = 164; // xa4
        public const int f15 = 165; // xa5
        public const int f16 = 166; // xa6
        public const int f17 = 167; // xa7
        public const int f18 = 168; // xa8
        public const int f19 = 169; // xa9
        public const int f20 = 170; // xaa
        public const int f21 = 171; // xab
        public const int f22 = 172; // xac
        public const int f23 = 173; // xad
        public const int f24 = 174; // xae
        public const int f25 = 175; // xaf
        public const int f26 = 176; // xb0
        public const int f27 = 177; // xb1
        public const int f28 = 178; // xb2
        public const int f29 = 179; // xb3
        public const int f30 = 180; // xb4
        public const int f31 = 181; // xb5
        public const int f32 = 182; // xb6
        public const int f33 = 183; // xb7
        public const int f34 = 184; // xb8
        public const int f35 = 185; // xb9
        public const int f36 = 186; // xba
        public const int f37 = 187; // xbb
        public const int f38 = 188; // xbc
        public const int f39 = 189; // xbd
        public const int f40 = 190; // xbe
        public const int f41 = 191; // xbf
        public const int f42 = 192; // xc0
        public const int f43 = 193; // xc1
        public const int f44 = 194; // xc2
        public const int f45 = 195; // xc3
        public const int f46 = 196; // xc4
        public const int f47 = 197; // xc5
        public const int f48 = 198; // xc6
        public const int f49 = 199; // xc7
        public const int f50 = 200; // xc8
        public const int f51 = 201; // xc9
        public const int f52 = 202; // xca
        public const int f53 = 203; // xcb
        public const int f54 = 204; // xcc
        public const int f55 = 205; // xcd
        public const int f56 = 206; // xce
        public const int f57 = 207; // xcf
        public const int f58 = 208; // xd0
        public const int f59 = 209; // xd1
        public const int f60 = 210; // xd2
        public const int f61 = 211; // xd3
        public const int f62 = 212; // xd4
        public const int f63 = 213; // xd5
        public const int f64 = 214; // xd6
        public const int f65 = 215; // xd7
        public const int f66 = 216; // xd8
        public const int f67 = 217; // xd9
        public const int f68 = 218; // xda
        public const int f69 = 219; // xdb
        public const int f70 = 220; // xdc
        public const int f71 = 221; // xdd
        public const int f72 = 222; // xde
        public const int f73 = 223; // xdf
        public const int f74 = 224; // xe0
        public const int f75 = 225; // xe1
        public const int f76 = 226; // xe2
        public const int f77 = 227; // xe3
        public const int f78 = 228; // xe4
        public const int f79 = 229; // xe5
        public const int f80 = 230; // xe6
        public const int f81 = 231; // xe7
        public const int f82 = 232; // xe8
        public const int f83 = 233; // xe9
        public const int f84 = 234; // xea
        public const int f85 = 235; // xeb
        public const int f86 = 236; // xec
        public const int f87 = 237; // xed
        public const int f88 = 238; // xee
        public const int f89 = 239; // xef
        public const int f90 = 240; // xf0
        public const int f91 = 241; // xf1
        public const int f92 = 242; // xf2
        public const int f93 = 243; // xf3
        public const int f94 = 244; // xf4
        public const int f95 = 245; // xf5
        public const int f96 = 246; // xf6
        public const int f97 = 247; // xf7
        public const int f98 = 248; // xf8
        public const int f99 = 249; // xf9
        public const int minfhigh = 10;
        public const int maxfhigh = 99;

        public static int fx(int offset)
        {
            if (offset >= minflow && offset <= maxflow)
            {
                return f + offset;
            }
            if (offset >= minfhigh && offset <= maxfhigh)
            {
                return f10 + offset - minfhigh;
            }
            throw new SystemException($"fx({offset}) - invalid offset");
        }

        // --- Local copy of Global Flag registers ---

        public const int g = 128; // x80-x81
        public const int g1 = 130; // x82
        public const int g2 = 131; // x83
        public const int g3 = 132; // x84
        public const int g4 = 133; // x85
        public const int g5 = 134; // x86
        public const int g6 = 135; // x87
        public const int g7 = 136; // x88
        public const int g8 = 137; // x89
        public const int g9 = 138; // x8a

        public static int gx(int offset)
        {
            switch (offset)
            {
                case 0: return g;
                case 1: return g1;
                case 2: return g2;
                case 3: return g3;
                case 4: return g4;
                case 5: return g5;
                case 6: return g6;
                case 7: return g7;
                case 8: return g8;
                case 9: return g9;
            }
            throw new SystemException($"gx({offset}) - invalid offset");
        }

        // --- New Internal IDRIS Variables ---

        public const int sortstate = 139; // x8b - new in idris
        public const int printdev = 140; // x8c - new in idris
        public const int printon = 141; // x8d - new in idris
        public const int background = 142; // x8e - new in idris
        public const int tballoc = 143; // x8f - new in idris
        public const int ffpending = 144; // x90 - new in idris
        public const int pagehasdata = 145; // x91 - new in idris
        public const int linehasdata = 146; // x92 - new in idris
        public const int ilmflag = 147; // x93 - new in idris
        public const int localedit = 148; // x94 - new in idris
        public const int scriptrunflag = 149; // x95 - new in idris
        public const int scriptwriteflag = 150; // x96 - new in idris
        public const int progline = 151; // x97-x98 - new in idris

        // --- Numeric 6-byte Variables ---

        public const int numslotsize = 6;

        public const int n = 256; // x0100
        public const int minnlow = 0;
        public const int maxnlow = 63;

        public const int rec = n + ((maxnlow + 1) * numslotsize);
        public const int rem = n + ((maxnlow + 2) * numslotsize);

        public const int n64 = 55 * 256; // x3700
        public const int minnhigh = 64;
        public const int maxnhigh = 99;

        public static int nx(int offset)
        {
            if (offset >= minnlow && offset <= maxnlow)
            {
                return n + (offset * numslotsize);
            }
            if (offset >= minnhigh && offset <= maxnhigh)
            {
                return n64 + ((offset - minnhigh) * numslotsize);
            }
            throw new SystemException($"nx({offset}) - invalid offset");
        }

        // --- Low Alpha Varables ---

        public const int date = 746; // x02ea
        public const int key = date + 18; // x02fc
        public const int a = key + 20; // x0310
        public const int a1 = a + 40; // x0338
        public const int a2 = a + 60; // x034c
        public const int b = a + 80; // x0360
        public const int b1 = b + 40; // x0388
        public const int b2 = b + 60; // x039c
        public const int c = b + 80; // x03b0
        public const int c1 = c + 40; // x03d8
        public const int c2 = c + 60; // x03ec

        // --- Medium Alpha Varables ---

        public const int d = 3840; // x0f00
        public const int d1 = d + 40; // x0f28
        public const int d2 = d + 60; // x0f3c
        public const int e = d + 80; // x0f50
        public const int e1 = e + 40; // x0f78
        public const int e2 = e + 60; // x0f8c

        // --- High Alpha Varables ---

        public const int a3 = highalpha; // x3800
        public const int a4 = a3 + 20; // x3816
        public const int a5 = a4 + 20; // x382c
        public const int a6 = a5 + 20; // x3842
        public const int a7 = a6 + 20; // x3858
        public const int a8 = a7 + 20; // x386e
        public const int a9 = a8 + 20; // x3884

        public const int b3 = a9 + 20; // x389a
        public const int b4 = b3 + 20; // x38b0
        public const int b5 = b4 + 20; // x38c6
        public const int b6 = b5 + 20; // x38dc
        public const int b7 = b6 + 20; // x38f2
        public const int b8 = b7 + 20; // x3908
        public const int b9 = b8 + 20; // x391e

        public const int c3 = b9 + 20; // x3934
        public const int c4 = c3 + 20; // x394a
        public const int c5 = c4 + 20; // x3960
        public const int c6 = c5 + 20; // x3976
        public const int c7 = c6 + 20; // x398c
        public const int c8 = c7 + 20; // x39a2
        public const int c9 = c8 + 20; // x39b8

        public const int d3 = c9 + 20; // x39ce
        public const int d4 = d3 + 20; // x39e4
        public const int d5 = d4 + 20; // x39fa
        public const int d6 = d5 + 20; // x3a10
        public const int d7 = d6 + 20; // x3a26
        public const int d8 = d7 + 20; // x3a3c
        public const int d9 = d8 + 20; // x3a52

        public const int e3 = d9 + 20; // x3a68
        public const int e4 = e3 + 20; // x3a7e
        public const int e5 = e4 + 20; // x3a94
        public const int e6 = e5 + 20; // x3aaa
        public const int e7 = e6 + 20; // x3ac0
        public const int e8 = e7 + 20; // x3ad6
        public const int e9 = e8 + 20; // x3aec

        // --- Buffers ---

        public const int rpage = 4; // x0400
        public const int zpage = 5; // x0500
        public const int xpage = 6; // x0600
        public const int ypage = 7; // x0700
        public const int wpage = 8; // x0800
        public const int spage = 11; // x0b00
        public const int tpage = 12; // x0c00
        public const int upage = 13; // x0d00
        public const int vpage = 14; // x0e00

        public const int r = rpage * 256; // x0400
        public const int z = zpage * 256; // x0500
        public const int x = xpage * 256; // x0600
        public const int y = ypage * 256; // x0700
        public const int w = wpage * 256; // x0800
        public const int s = spage * 256; // x0b00
        public const int t = tpage * 256; // x0c00
        public const int u = upage * 256; // x0d00
        public const int v = vpage * 256; // x0e00
    }
}
