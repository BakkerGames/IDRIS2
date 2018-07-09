// Memory.cs - 07/06/2018

namespace IDRIS_Runtime
{
    public static partial class Memory
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

        const int MemPos_FileTable = 9 * 256;
        const int MemPos_TrackBuffer = 16 * 256;
        const int MemPos_DevTable = 48 * 256;
        const int MemPos_VolTable = MemPos_DevTable + 32;
        const int MemPos_TFATable = 51 * 256;
        const int MemPos_ChanTable = 53 * 256;
        const int MemPos_NumHigh = 55 * 256;
        const int MemPos_HighAlpha = 56 * 256;
        const int TotalMemSize = 59 * 256;

        // --- Table Record Sizes ---

        const int FileEntrySize = 16;
        const int DevEntrySize = 1;
        const int VolEntrySize = 10;
        const int TFAEntrySize = 10;
        const int ChanEntrySize = 3;

        // --- Highest Table Entry numbers ---

        const int MaxFile = 31;
        const int MaxDevice = 31;
        const int MaxVolume = 63;
        const int MaxTFA = 31;
        const int MaxChannel = 127;

        // --- Buffer Pointers ---

        const int MemPos_RP = 0; //  x00
        const int MemPos_RP2 = 1; //  x01
        const int MemPos_IRP = 2; //  x02
        const int MemPos_IRP2 = 3; //  x03
        const int MemPos_ZP = 4; //  x04
        const int MemPos_ZP2 = 5; //  x05
        const int MemPos_IZP = 6; //  x06
        const int MemPos_IZP2 = 7; //  x07
        const int MemPos_XP = 8; //  x08
        const int MemPos_XP2 = 9; //  x09
        const int MemPos_IXP = 10; //  x0A
        const int MemPos_IXP2 = 11; //  x0B
        const int MemPos_YP = 12; //  x0C
        const int MemPos_YP2 = 13; //  x0D
        const int MemPos_IYP = 14; //  x0E
        const int MemPos_IYP2 = 15; //  x0F
        const int MemPos_WP = 16; //  x10
        const int MemPos_WP2 = 17; //  x11
        const int MemPos_IWP = 18; //  x12
        const int MemPos_IWP2 = 19; //  x13

        const int MemPos_SP = 112; //  x70
        const int MemPos_SP2 = 113; //  x71
        const int MemPos_ISP = 114; //  x72
        const int MemPos_ISP2 = 115; //  x73
        const int MemPos_TP = 116; //  x74
        const int MemPos_TP2 = 117; //  x75
        const int MemPos_ITP = 118; //  x76
        const int MemPos_ITP2 = 119; //  x77
        const int MemPos_UP = 120; //  x78
        const int MemPos_UP2 = 121; //  x79
        const int MemPos_IUP = 122; //  x7A
        const int MemPos_IUP2 = 123; //  x7B
        const int MemPos_VP = 124; //  x7C
        const int MemPos_VP2 = 125; //  x7D
        const int MemPos_IVP = 126; //  x7E
        const int MemPos_IVP2 = 127; //  x7F

        // --- Byte System Variables ---

        const int MemPos_Lib = 20; //  x14
        const int MemPos_Prog = 21; //  x15-x16
        const int MemPos_Privg = 46; //  x2E
        const int MemPos_Char = 48; //  x30
        const int MemPos_Length = 49; //  x31
        const int MemPos_Status = 50; //  x32
        const int MemPos_EscVal = 51; //  x33
        const int MemPos_CanVal = 52; //  x34
        const int MemPos_LockVal = 53; //  x35
        const int MemPos_TChan = 54; //  x36
        const int MemPos_Term = 73; //  x49
        const int MemPos_Lang = 76; //  x4C
        const int MemPos_PrtNum = 77; //  x4D
        const int MemPos_TFA = 89; //  x59
        const int MemPos_Vol = 90; //  x5A
        const int MemPos_PVol = 91; //  x5B
        const int MemPos_ReqVol = 103; //  x67

        // --- 2-Byte System Variables ---

        const int MemPos_User = 102; //  x66-x67
        const int MemPos_Orig = 104; //  x68-x69
        const int MemPos_Oper = 106; //  x6A-x6B

        // --- Read-Only Byte System Variables ---

        const int MemPos_MachType = 109; //  x6D  = Constant 20 in IDRIS;
        const int MemPos_SysRel = 110; //  x6E  = Constant 5 in IDRIS;
        const int MemPos_SysRev = 111; //  x6F  = Constant 0 in IDRIS;

        // --- Flag registers ---

        const int MemPos_F = 92; //  x5C
        const int MemPos_F1 = 93; //  x5D
        const int MemPos_F2 = 94; //  x5E
        const int MemPos_F3 = 95; //  x5F
        const int MemPos_F4 = 96; //  x60
        const int MemPos_F5 = 97; //  x61
        const int MemPos_F6 = 98; //  x62
        const int MemPos_F7 = 99; //  x63
        const int MemPos_F8 = 100; //  x64
        const int MemPos_F9 = 101; //  x65
        const int MinFLow = 0;
        const int MaxFLow = 9;

        const int MemPos_F10 = 160; //  xA0
        const int MemPos_F11 = 161; //  xA1
        const int MemPos_F12 = 162; //  xA2
        const int MemPos_F13 = 163; //  xA3
        const int MemPos_F14 = 164; //  xA4
        const int MemPos_F15 = 165; //  xA5
        const int MemPos_F16 = 166; //  xA6
        const int MemPos_F17 = 167; //  xA7
        const int MemPos_F18 = 168; //  xA8
        const int MemPos_F19 = 169; //  xA9
        const int MemPos_F20 = 170; //  xAA
        const int MemPos_F21 = 171; //  xAB
        const int MemPos_F22 = 172; //  xAC
        const int MemPos_F23 = 173; //  xAD
        const int MemPos_F24 = 174; //  xAE
        const int MemPos_F25 = 175; //  xAF
        const int MemPos_F26 = 176; //  xB0
        const int MemPos_F27 = 177; //  xB1
        const int MemPos_F28 = 178; //  xB2
        const int MemPos_F29 = 179; //  xB3
        const int MemPos_F30 = 180; //  xB4
        const int MemPos_F31 = 181; //  xB5
        const int MemPos_F32 = 182; //  xB6
        const int MemPos_F33 = 183; //  xB7
        const int MemPos_F34 = 184; //  xB8
        const int MemPos_F35 = 185; //  xB9
        const int MemPos_F36 = 186; //  xBA
        const int MemPos_F37 = 187; //  xBB
        const int MemPos_F38 = 188; //  xBC
        const int MemPos_F39 = 189; //  xBD
        const int MemPos_F40 = 190; //  xBE
        const int MemPos_F41 = 191; //  xBF
        const int MemPos_F42 = 192; //  xC0
        const int MemPos_F43 = 193; //  xC1
        const int MemPos_F44 = 194; //  xC2
        const int MemPos_F45 = 195; //  xC3
        const int MemPos_F46 = 196; //  xC4
        const int MemPos_F47 = 197; //  xC5
        const int MemPos_F48 = 198; //  xC6
        const int MemPos_F49 = 199; //  xC7
        const int MemPos_F50 = 200; //  xC8
        const int MemPos_F51 = 201; //  xC9
        const int MemPos_F52 = 202; //  xCA
        const int MemPos_F53 = 203; //  xCB
        const int MemPos_F54 = 204; //  xCC
        const int MemPos_F55 = 205; //  xCD
        const int MemPos_F56 = 206; //  xCE
        const int MemPos_F57 = 207; //  xCF
        const int MemPos_F58 = 208; //  xD0
        const int MemPos_F59 = 209; //  xD1
        const int MemPos_F60 = 210; //  xD2
        const int MemPos_F61 = 211; //  xD3
        const int MemPos_F62 = 212; //  xD4
        const int MemPos_F63 = 213; //  xD5
        const int MemPos_F64 = 214; //  xD6
        const int MemPos_F65 = 215; //  xD7
        const int MemPos_F66 = 216; //  xD8
        const int MemPos_F67 = 217; //  xD9
        const int MemPos_F68 = 218; //  xDA
        const int MemPos_F69 = 219; //  xDB
        const int MemPos_F70 = 220; //  xDC
        const int MemPos_F71 = 221; //  xDD
        const int MemPos_F72 = 222; //  xDE
        const int MemPos_F73 = 223; //  xDF
        const int MemPos_F74 = 224; //  xE0
        const int MemPos_F75 = 225; //  xE1
        const int MemPos_F76 = 226; //  xE2
        const int MemPos_F77 = 227; //  xE3
        const int MemPos_F78 = 228; //  xE4
        const int MemPos_F79 = 229; //  xE5
        const int MemPos_F80 = 230; //  xE6
        const int MemPos_F81 = 231; //  xE7
        const int MemPos_F82 = 232; //  xE8
        const int MemPos_F83 = 233; //  xE9
        const int MemPos_F84 = 234; //  xEA
        const int MemPos_F85 = 235; //  xEB
        const int MemPos_F86 = 236; //  xEC
        const int MemPos_F87 = 237; //  xED
        const int MemPos_F88 = 238; //  xEE
        const int MemPos_F89 = 239; //  xEF
        const int MemPos_F90 = 240; //  xF0
        const int MemPos_F91 = 241; //  xF1
        const int MemPos_F92 = 242; //  xF2
        const int MemPos_F93 = 243; //  xF3
        const int MemPos_F94 = 244; //  xF4
        const int MemPos_F95 = 245; //  xF5
        const int MemPos_F96 = 246; //  xF6
        const int MemPos_F97 = 247; //  xF7
        const int MemPos_F98 = 248; //  xF8
        const int MemPos_F99 = 249; //  xF9
        const int MinFHigh = 10;
        const int MaxFHigh = 99;

        // --- Local copy of Global Flag registers ---

        const int MemPos_G = 128; //  x80-x81
        const int MemPos_G1 = 130; //  x82
        const int MemPos_G2 = 131; //  x83
        const int MemPos_G3 = 132; //  x84
        const int MemPos_G4 = 133; //  x85
        const int MemPos_G5 = 134; //  x86
        const int MemPos_G6 = 135; //  x87
        const int MemPos_G7 = 136; //  x88
        const int MemPos_G8 = 137; //  x89
        const int MemPos_G9 = 138; //  x8A

        // --- New Internal IDRIS Variables ---

        const int MemPos_SortState = 139; //  x8B - New in IDRIS
        const int MemPos_PrintDev = 140; //  x8C - New in IDRIS
        const int MemPos_PrintOn = 141; //  x8D - New in IDRIS
        const int MemPos_Background = 142; //  x8E - New in IDRIS
        const int MemPos_TBAlloc = 143; //  x8F - New in IDRIS
        const int MemPos_FFPending = 144; //  x90 - New in IDRIS
        const int MemPos_PageHasData = 145; //  x91 - New in IDRIS
        const int MemPos_LineHasData = 146; //  x92 - New in IDRIS
        const int MemPos_ILMFlag = 147; //  x93 - New in IDRIS
        const int MemPos_LocalEdit = 148; //  x94 - New in IDRIS
        const int MemPos_ScriptRunFlag = 149; //  x95 - New in IDRIS
        const int MemPos_ScriptWriteFlag = 150; //  x96 - New in IDRIS

        // --- Numeric 6-byte Variables ---

        const int NumSlotSize = 6;

        const int MemPos_N = 256; //  x0100
        const int MinNLow = 0;
        const int MaxNLow = 63;

        const int MemPos_Rec = MemPos_N + ((MaxNLow + 1) * NumSlotSize);
        const int MemPos_RemVal = MemPos_N + ((MaxNLow + 2) * NumSlotSize);

        const int MemPos_N64 = 55 * 256; //  x3700
        const int MinNHigh = 64;
        const int MaxNHigh = 99;

        // --- Low Alpha Varables ---

        const int MemPos_DateVal = 746; //  x02EA
        const int MemPos_Key = MemPos_DateVal + 18; //  x02FC
        const int MemPos_A = MemPos_Key + 20; //  x0310
        const int MemPos_A1 = MemPos_A + 40; //  x0338
        const int MemPos_A2 = MemPos_A + 60; //  x034C
        const int MemPos_B = MemPos_A + 80; //  x0360
        const int MemPos_B1 = MemPos_B + 40; //  x0388
        const int MemPos_B2 = MemPos_B + 60; //  x039C
        const int MemPos_C = MemPos_B + 80; //  x03B0
        const int MemPos_C1 = MemPos_C + 40; //  x03D8
        const int MemPos_C2 = MemPos_C + 60; //  x03EC

        // --- Medium Alpha Varables ---

        const int MemPos_D = 3840; //  x0F00
        const int MemPos_D1 = MemPos_D + 40; //  x0F28
        const int MemPos_D2 = MemPos_D + 60; //  x0F3C
        const int MemPos_E = MemPos_D + 80; //  x0F50
        const int MemPos_E1 = MemPos_E + 40; //  x0F78
        const int MemPos_E2 = MemPos_E + 60; //  x0F8C

        // --- High Alpha Varables ---

        const int MemPos_A3 = MemPos_HighAlpha; //  x3800
        const int MemPos_A4 = MemPos_A3 + 20; //  x3816
        const int MemPos_A5 = MemPos_A4 + 20; //  x382C
        const int MemPos_A6 = MemPos_A5 + 20; //  x3842
        const int MemPos_A7 = MemPos_A6 + 20; //  x3858
        const int MemPos_A8 = MemPos_A7 + 20; //  x386E
        const int MemPos_A9 = MemPos_A8 + 20; //  x3884

        const int MemPos_B3 = MemPos_A9 + 20; //  x389A
        const int MemPos_B4 = MemPos_B3 + 20; //  x38B0
        const int MemPos_B5 = MemPos_B4 + 20; //  x38C6
        const int MemPos_B6 = MemPos_B5 + 20; //  x38DC
        const int MemPos_B7 = MemPos_B6 + 20; //  x38F2
        const int MemPos_B8 = MemPos_B7 + 20; //  x3908
        const int MemPos_B9 = MemPos_B8 + 20; //  x391E

        const int MemPos_C3 = MemPos_B9 + 20; //  x3934
        const int MemPos_C4 = MemPos_C3 + 20; //  x394A
        const int MemPos_C5 = MemPos_C4 + 20; //  x3960
        const int MemPos_C6 = MemPos_C5 + 20; //  x3976
        const int MemPos_C7 = MemPos_C6 + 20; //  x398C
        const int MemPos_C8 = MemPos_C7 + 20; //  x39A2
        const int MemPos_C9 = MemPos_C8 + 20; //  x39B8

        const int MemPos_D3 = MemPos_C9 + 20; //  x39CE
        const int MemPos_D4 = MemPos_D3 + 20; //  x39E4
        const int MemPos_D5 = MemPos_D4 + 20; //  x39FA
        const int MemPos_D6 = MemPos_D5 + 20; //  x3A10
        const int MemPos_D7 = MemPos_D6 + 20; //  x3A26
        const int MemPos_D8 = MemPos_D7 + 20; //  x3A3C
        const int MemPos_D9 = MemPos_D8 + 20; //  x3A52

        const int MemPos_E3 = MemPos_D9 + 20; //  x3A68
        const int MemPos_E4 = MemPos_E3 + 20; //  x3A7E
        const int MemPos_E5 = MemPos_E4 + 20; //  x3A94
        const int MemPos_E6 = MemPos_E5 + 20; //  x3AAA
        const int MemPos_E7 = MemPos_E6 + 20; //  x3AC0
        const int MemPos_E8 = MemPos_E7 + 20; //  x3AD6
        const int MemPos_E9 = MemPos_E8 + 20; //  x3AEC

        // --- Buffers ---

        const int MemPos_R = 4 * 256; //  x0400
        const int MemPos_Z = 5 * 256; //  x0500
        const int MemPos_X = 6 * 256; //  x0600
        const int MemPos_Y = 7 * 256; //  x0700
        const int MemPos_W = 8 * 256; //  x0800
        const int MemPos_S = 11 * 256; //  x0B00
        const int MemPos_T = 12 * 256; //  x0C00
        const int MemPos_U = 13 * 256; //  x0D00
        const int MemPos_V = 14 * 256; //  x0E00

        // --- Memory block ---

        static byte[] MEM = new byte[TotalMemSize];

    }
}
