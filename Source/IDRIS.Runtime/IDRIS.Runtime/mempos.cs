// mempos.cs - 07/18/2018

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

        public const int rp = 0; // x0000
        public const int rp2 = 1; // x0001
        public const int irp = 2; // x0002
        public const int irp2 = 3; // x0003
        public const int zp = 4; // x0004
        public const int zp2 = 5; // x0005
        public const int izp = 6; // x0006
        public const int izp2 = 7; // x0007
        public const int xp = 8; // x0008
        public const int xp2 = 9; // x0009
        public const int ixp = 10; // x000a
        public const int ixp2 = 11; // x000b
        public const int yp = 12; // x000c
        public const int yp2 = 13; // x000d
        public const int iyp = 14; // x000e
        public const int iyp2 = 15; // x000f
        public const int wp = 16; // x0010
        public const int wp2 = 17; // x0011
        public const int iwp = 18; // x0012
        public const int iwp2 = 19; // x0013

        public const int sp = 112; // x0070
        public const int sp2 = 113; // x0071
        public const int isp = 114; // x0072
        public const int isp2 = 115; // x0073
        public const int tp = 116; // x0074
        public const int tp2 = 117; // x0075
        public const int itp = 118; // x0076
        public const int itp2 = 119; // x0077
        public const int up = 120; // x0078
        public const int up2 = 121; // x0079
        public const int iup = 122; // x007a
        public const int iup2 = 123; // x007b
        public const int vp = 124; // x007c
        public const int vp2 = 125; // x007d
        public const int ivp = 126; // x007e
        public const int ivp2 = 127; // x007f

        // --- Byte System Variables ---

        public const int lib = 20; // x0014
        public const int prog = 21; // x0015-x0016
        public const int privg = 46; // x002e
        public const int charval = 48; // x0030
        public const int length = 49; // x0031
        public const int status = 50; // x0032
        public const int esc = 51; // x0033
        public const int can = 52; // x0034
        public const int lockval = 53; // x0035
        public const int tchan = 54; // x0036
        public const int term = 73; // x0049
        public const int lang = 76; // x004c
        public const int prtnum = 77; // x004d
        public const int tfa = 89; // x0059
        public const int vol = 90; // x005a
        public const int pvol = 91; // x005b
        public const int reqvol = 103; // x0067

        // --- 2-Byte System Variables ---

        public const int user = 102; // x0066-x0067
        public const int orig = 104; // x0068-x0069
        public const int oper = 106; // x006a-x006b

        // --- Read-Only Byte System Variables ---

        public const int machtype = 109; // x006d  = public constant 21 in idris2;
        public const int sysrel = 110; // x006e  = public constant 5 in idris2;
        public const int sysrev = 111; // x006f  = public constant 1 in idris2;

        // --- Flag registers ---

        public const int f = 92; // x005c
        public const int f1 = 93; // x005d
        public const int f2 = 94; // x005e
        public const int f3 = 95; // x005f
        public const int f4 = 96; // x0060
        public const int f5 = 97; // x0061
        public const int f6 = 98; // x0062
        public const int f7 = 99; // x0063
        public const int f8 = 100; // x0064
        public const int f9 = 101; // x0065
        public const int minflow = 0;
        public const int maxflow = 9;

        public const int f10 = 160; // x00a0
        public const int f11 = 161; // x00a1
        public const int f12 = 162; // x00a2
        public const int f13 = 163; // x00a3
        public const int f14 = 164; // x00a4
        public const int f15 = 165; // x00a5
        public const int f16 = 166; // x00a6
        public const int f17 = 167; // x00a7
        public const int f18 = 168; // x00a8
        public const int f19 = 169; // x00a9
        public const int f20 = 170; // x00aa
        public const int f21 = 171; // x00ab
        public const int f22 = 172; // x00ac
        public const int f23 = 173; // x00ad
        public const int f24 = 174; // x00ae
        public const int f25 = 175; // x00af
        public const int f26 = 176; // x00b0
        public const int f27 = 177; // x00b1
        public const int f28 = 178; // x00b2
        public const int f29 = 179; // x00b3
        public const int f30 = 180; // x00b4
        public const int f31 = 181; // x00b5
        public const int f32 = 182; // x00b6
        public const int f33 = 183; // x00b7
        public const int f34 = 184; // x00b8
        public const int f35 = 185; // x00b9
        public const int f36 = 186; // x00ba
        public const int f37 = 187; // x00bb
        public const int f38 = 188; // x00bc
        public const int f39 = 189; // x00bd
        public const int f40 = 190; // x00be
        public const int f41 = 191; // x00bf
        public const int f42 = 192; // x00c0
        public const int f43 = 193; // x00c1
        public const int f44 = 194; // x00c2
        public const int f45 = 195; // x00c3
        public const int f46 = 196; // x00c4
        public const int f47 = 197; // x00c5
        public const int f48 = 198; // x00c6
        public const int f49 = 199; // x00c7
        public const int f50 = 200; // x00c8
        public const int f51 = 201; // x00c9
        public const int f52 = 202; // x00ca
        public const int f53 = 203; // x00cb
        public const int f54 = 204; // x00cc
        public const int f55 = 205; // x00cd
        public const int f56 = 206; // x00ce
        public const int f57 = 207; // x00cf
        public const int f58 = 208; // x00d0
        public const int f59 = 209; // x00d1
        public const int f60 = 210; // x00d2
        public const int f61 = 211; // x00d3
        public const int f62 = 212; // x00d4
        public const int f63 = 213; // x00d5
        public const int f64 = 214; // x00d6
        public const int f65 = 215; // x00d7
        public const int f66 = 216; // x00d8
        public const int f67 = 217; // x00d9
        public const int f68 = 218; // x00da
        public const int f69 = 219; // x00db
        public const int f70 = 220; // x00dc
        public const int f71 = 221; // x00dd
        public const int f72 = 222; // x00de
        public const int f73 = 223; // x00df
        public const int f74 = 224; // x00e0
        public const int f75 = 225; // x00e1
        public const int f76 = 226; // x00e2
        public const int f77 = 227; // x00e3
        public const int f78 = 228; // x00e4
        public const int f79 = 229; // x00e5
        public const int f80 = 230; // x00e6
        public const int f81 = 231; // x00e7
        public const int f82 = 232; // x00e8
        public const int f83 = 233; // x00e9
        public const int f84 = 234; // x00ea
        public const int f85 = 235; // x00eb
        public const int f86 = 236; // x00ec
        public const int f87 = 237; // x00ed
        public const int f88 = 238; // x00ee
        public const int f89 = 239; // x00ef
        public const int f90 = 240; // x00f0
        public const int f91 = 241; // x00f1
        public const int f92 = 242; // x00f2
        public const int f93 = 243; // x00f3
        public const int f94 = 244; // x00f4
        public const int f95 = 245; // x00f5
        public const int f96 = 246; // x00f6
        public const int f97 = 247; // x00f7
        public const int f98 = 248; // x00f8
        public const int f99 = 249; // x00f9
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

        public const int g = 128; // x0080-x0081
        public const int g1 = 130; // x0082
        public const int g2 = 131; // x0083
        public const int g3 = 132; // x0084
        public const int g4 = 133; // x0085
        public const int g5 = 134; // x0086
        public const int g6 = 135; // x0087
        public const int g7 = 136; // x0088
        public const int g8 = 137; // x0089
        public const int g9 = 138; // x008a

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

        public const int sortstate = 139;       // x008b - new in idris
        public const int printdev = 140;        // x008c - new in idris
        public const int printon = 141;         // x008d - new in idris
        public const int background = 142;      // x008e - new in idris
        public const int tballoc = 143;         // x008f - new in idris
        public const int ffpending = 144;       // x0090 - new in idris
        public const int pagehasdata = 145;     // x0091 - new in idris
        public const int linehasdata = 146;     // x0092 - new in idris
        public const int ilmflag = 147;         // x0093 - new in idris
        public const int localedit = 148;       // x0094 - new in idris
        public const int scriptrunflag = 149;   // x0095 - new in idris
        public const int scriptwriteflag = 150; // x0096 - new in idris
        public const int progline = 151;        // x0097-x0098 - new in idris

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

        public const int rbuff = rpage * 256; // x0400
        public const int zbuff = zpage * 256; // x0500
        public const int xbuff = xpage * 256; // x0600
        public const int ybuff = ypage * 256; // x0700
        public const int wbuff = wpage * 256; // x0800
        public const int sbuff = spage * 256; // x0b00
        public const int tbuff = tpage * 256; // x0c00
        public const int ubuff = upage * 256; // x0d00
        public const int vbuff = vpage * 256; // x0e00
    }
}
