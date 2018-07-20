// MemPos.GetPos.cs - 07/20/2018

namespace IDRIS.Runtime
{
    public static partial class MemPos
    {
        public static long? GetPos(string token)
        {
            long? result = null;
            result = GetPosByte(token);
            if (result == null)
            {
                result = GetPosNumeric(token);
            }
            if (result == null)
            {
                result = GetPosAlpha(token);
            }
            return result;
        }

        public static long? GetPosByte(string token)
        {
            switch (token)
            {
                case "RP": return rp;
                case "RP2": return rp2;
                case "IRP": return irp;
                case "IRP2": return irp2;
                case "ZP": return zp;
                case "ZP2": return zp2;
                case "IZP": return izp;
                case "IZP2": return izp2;
                case "XP": return xp;
                case "XP2": return xp2;
                case "IXP": return ixp;
                case "IXP2": return ixp2;
                case "YP": return yp;
                case "YP2": return yp2;
                case "IYP": return iyp;
                case "IYP2": return iyp2;
                case "WP": return wp;
                case "WP2": return wp2;
                case "IWP": return iwp;
                case "IWP2": return iwp2;
                case "SP": return sp;
                case "SP2": return sp2;
                case "ISP": return isp;
                case "ISP2": return isp2;
                case "TP": return tp;
                case "TP2": return tp2;
                case "ITP": return itp;
                case "ITP2": return itp2;
                case "UP": return up;
                case "UP2": return up2;
                case "IUP": return iup;
                case "IUP2": return iup2;
                case "VP": return vp;
                case "VP2": return vp2;
                case "IVP": return ivp;
                case "IVP2": return ivp2;
                case "LIB": return lib;
                case "PRIVG": return privg;
                case "CHARVAL": return charval;
                case "LENGTH": return length;
                case "STATUS": return status;
                case "ESC": return esc;
                case "CAN": return can;
                case "LOCKVAL": return lockval;
                case "TCHAN": return tchan;
                case "TERM": return term;
                case "LANG": return lang;
                case "PRTNUM": return prtnum;
                case "TFA": return tfa;
                case "VOL": return vol;
                case "PVOL": return pvol;
                case "REQVOL": return reqvol;
                case "MACHTYPE": return machtype;
                case "SYSREL": return sysrel;
                case "SYSREV": return sysrev;
                case "F": return f;
                case "F1": return f1;
                case "F2": return f2;
                case "F3": return f3;
                case "F4": return f4;
                case "F5": return f5;
                case "F6": return f6;
                case "F7": return f7;
                case "F8": return f8;
                case "F9": return f9;
                case "F10": return f10;
                case "F11": return f11;
                case "F12": return f12;
                case "F13": return f13;
                case "F14": return f14;
                case "F15": return f15;
                case "F16": return f16;
                case "F17": return f17;
                case "F18": return f18;
                case "F19": return f19;
                case "F20": return f20;
                case "F21": return f21;
                case "F22": return f22;
                case "F23": return f23;
                case "F24": return f24;
                case "F25": return f25;
                case "F26": return f26;
                case "F27": return f27;
                case "F28": return f28;
                case "F29": return f29;
                case "F30": return f30;
                case "F31": return f31;
                case "F32": return f32;
                case "F33": return f33;
                case "F34": return f34;
                case "F35": return f35;
                case "F36": return f36;
                case "F37": return f37;
                case "F38": return f38;
                case "F39": return f39;
                case "F40": return f40;
                case "F41": return f41;
                case "F42": return f42;
                case "F43": return f43;
                case "F44": return f44;
                case "F45": return f45;
                case "F46": return f46;
                case "F47": return f47;
                case "F48": return f48;
                case "F49": return f49;
                case "F50": return f50;
                case "F51": return f51;
                case "F52": return f52;
                case "F53": return f53;
                case "F54": return f54;
                case "F55": return f55;
                case "F56": return f56;
                case "F57": return f57;
                case "F58": return f58;
                case "F59": return f59;
                case "F60": return f60;
                case "F61": return f61;
                case "F62": return f62;
                case "F63": return f63;
                case "F64": return f64;
                case "F65": return f65;
                case "F66": return f66;
                case "F67": return f67;
                case "F68": return f68;
                case "F69": return f69;
                case "F70": return f70;
                case "F71": return f71;
                case "F72": return f72;
                case "F73": return f73;
                case "F74": return f74;
                case "F75": return f75;
                case "F76": return f76;
                case "F77": return f77;
                case "F78": return f78;
                case "F79": return f79;
                case "F80": return f80;
                case "F81": return f81;
                case "F82": return f82;
                case "F83": return f83;
                case "F84": return f84;
                case "F85": return f85;
                case "F86": return f86;
                case "F87": return f87;
                case "F88": return f88;
                case "F89": return f89;
                case "F90": return f90;
                case "F91": return f91;
                case "F92": return f92;
                case "F93": return f93;
                case "F94": return f94;
                case "F95": return f95;
                case "F96": return f96;
                case "F97": return f97;
                case "F98": return f98;
                case "F99": return f99;
                case "G1": return g1;
                case "G2": return g2;
                case "G3": return g3;
                case "G4": return g4;
                case "G5": return g5;
                case "G6": return g6;
                case "G7": return g7;
                case "G8": return g8;
                case "G9": return g9;
            }
            return null;
        }

        public static long? GetPosNumeric(string token)
        {
            switch (token)
            {
                case "PROG": return prog;
                case "USER": return user;
                case "ORIG": return orig;
                case "OPER": return oper;
                case "G": return g;
                case "N": return n;
                case "N1": return n + (1 * numslotsize);
                case "N2": return n + (2 * numslotsize);
                case "N3": return n + (3 * numslotsize);
                case "N4": return n + (4 * numslotsize);
                case "N5": return n + (5 * numslotsize);
                case "N6": return n + (6 * numslotsize);
                case "N7": return n + (7 * numslotsize);
                case "N8": return n + (8 * numslotsize);
                case "N9": return n + (9 * numslotsize);
                case "N10": return n + (10 * numslotsize);
                case "N11": return n + (11 * numslotsize);
                case "N12": return n + (12 * numslotsize);
                case "N13": return n + (13 * numslotsize);
                case "N14": return n + (14 * numslotsize);
                case "N15": return n + (15 * numslotsize);
                case "N16": return n + (16 * numslotsize);
                case "N17": return n + (17 * numslotsize);
                case "N18": return n + (18 * numslotsize);
                case "N19": return n + (19 * numslotsize);
                case "N20": return n + (20 * numslotsize);
                case "N21": return n + (21 * numslotsize);
                case "N22": return n + (22 * numslotsize);
                case "N23": return n + (23 * numslotsize);
                case "N24": return n + (24 * numslotsize);
                case "N25": return n + (25 * numslotsize);
                case "N26": return n + (26 * numslotsize);
                case "N27": return n + (27 * numslotsize);
                case "N28": return n + (28 * numslotsize);
                case "N29": return n + (29 * numslotsize);
                case "N30": return n + (30 * numslotsize);
                case "N31": return n + (31 * numslotsize);
                case "N32": return n + (32 * numslotsize);
                case "N33": return n + (33 * numslotsize);
                case "N34": return n + (34 * numslotsize);
                case "N35": return n + (35 * numslotsize);
                case "N36": return n + (36 * numslotsize);
                case "N37": return n + (37 * numslotsize);
                case "N38": return n + (38 * numslotsize);
                case "N39": return n + (39 * numslotsize);
                case "N40": return n + (40 * numslotsize);
                case "N41": return n + (41 * numslotsize);
                case "N42": return n + (42 * numslotsize);
                case "N43": return n + (43 * numslotsize);
                case "N44": return n + (44 * numslotsize);
                case "N45": return n + (45 * numslotsize);
                case "N46": return n + (46 * numslotsize);
                case "N47": return n + (47 * numslotsize);
                case "N48": return n + (48 * numslotsize);
                case "N49": return n + (49 * numslotsize);
                case "N50": return n + (50 * numslotsize);
                case "N51": return n + (51 * numslotsize);
                case "N52": return n + (52 * numslotsize);
                case "N53": return n + (53 * numslotsize);
                case "N54": return n + (54 * numslotsize);
                case "N55": return n + (55 * numslotsize);
                case "N56": return n + (56 * numslotsize);
                case "N57": return n + (57 * numslotsize);
                case "N58": return n + (58 * numslotsize);
                case "N59": return n + (59 * numslotsize);
                case "N60": return n + (60 * numslotsize);
                case "N61": return n + (61 * numslotsize);
                case "N62": return n + (62 * numslotsize);
                case "N63": return n + (63 * numslotsize);
                case "N64": return n64;
                case "N65": return n64 + (1 * numslotsize);
                case "N66": return n64 + (2 * numslotsize);
                case "N67": return n64 + (3 * numslotsize);
                case "N68": return n64 + (4 * numslotsize);
                case "N69": return n64 + (5 * numslotsize);
                case "N70": return n64 + (6 * numslotsize);
                case "N71": return n64 + (7 * numslotsize);
                case "N72": return n64 + (8 * numslotsize);
                case "N73": return n64 + (9 * numslotsize);
                case "N74": return n64 + (10 * numslotsize);
                case "N75": return n64 + (11 * numslotsize);
                case "N76": return n64 + (12 * numslotsize);
                case "N77": return n64 + (13 * numslotsize);
                case "N78": return n64 + (14 * numslotsize);
                case "N79": return n64 + (15 * numslotsize);
                case "N80": return n64 + (16 * numslotsize);
                case "N81": return n64 + (17 * numslotsize);
                case "N82": return n64 + (18 * numslotsize);
                case "N83": return n64 + (19 * numslotsize);
                case "N84": return n64 + (20 * numslotsize);
                case "N85": return n64 + (21 * numslotsize);
                case "N86": return n64 + (22 * numslotsize);
                case "N87": return n64 + (23 * numslotsize);
                case "N88": return n64 + (24 * numslotsize);
                case "N89": return n64 + (25 * numslotsize);
                case "N90": return n64 + (26 * numslotsize);
                case "N91": return n64 + (27 * numslotsize);
                case "N92": return n64 + (28 * numslotsize);
                case "N93": return n64 + (29 * numslotsize);
                case "N94": return n64 + (30 * numslotsize);
                case "N95": return n64 + (31 * numslotsize);
                case "N96": return n64 + (32 * numslotsize);
                case "N97": return n64 + (33 * numslotsize);
                case "N98": return n64 + (34 * numslotsize);
                case "N99": return n64 + (35 * numslotsize);
                case "REC": return rec;
                case "REM": return rem;
            }
            return null;
        }

        public static long? GetPosAlpha(string token)
        {
            switch (token)
            {
                case "DATE": return date;
                case "KEY": return key;
                case "A": return a;
                case "A1": return a1;
                case "A2": return a2;
                case "A3": return a3;
                case "A4": return a4;
                case "A5": return a5;
                case "A6": return a6;
                case "A7": return a7;
                case "A8": return a8;
                case "A9": return a9;
                case "B": return b;
                case "B1": return b1;
                case "B2": return b2;
                case "B3": return b3;
                case "B4": return b4;
                case "B5": return b5;
                case "B6": return b6;
                case "B7": return b7;
                case "B8": return b8;
                case "B9": return b9;
                case "C": return c;
                case "C1": return c1;
                case "C2": return c2;
                case "C3": return c3;
                case "C4": return c4;
                case "C5": return c5;
                case "C6": return c6;
                case "C7": return c7;
                case "C8": return c8;
                case "C9": return c9;
                case "D": return d;
                case "D1": return d1;
                case "D2": return d2;
                case "D3": return d3;
                case "D4": return d4;
                case "D5": return d5;
                case "D6": return d6;
                case "D7": return d7;
                case "D8": return d8;
                case "D9": return d9;
                case "E": return e;
                case "E1": return e1;
                case "E2": return e2;
                case "E3": return e3;
                case "E4": return e4;
                case "E5": return e5;
                case "E6": return e6;
                case "E7": return e7;
                case "E8": return e8;
                case "E9": return e9;
            }
            return null;
        }

        public static long? GetPosBufferPtr(string token)
        {
            switch (token)
            {
                case "R": return rp;
                case "Z": return zp;
                case "X": return xp;
                case "Y": return yp;
                case "W": return wp;
                case "S": return sp;
                case "T": return tp;
                case "U": return up;
                case "V": return vp;
                case "IR": return irp;
                case "IZ": return izp;
                case "IX": return ixp;
                case "IY": return iyp;
                case "IW": return iwp;
                case "IS": return isp;
                case "IT": return itp;
                case "IU": return iup;
                case "IV": return ivp;
            }
            return null;
        }

        public static long? GetPosBufferPage(string token)
        {
            switch (token)
            {
                case "R": return rpage;
                case "Z": return zpage;
                case "X": return xpage;
                case "Y": return ypage;
                case "W": return wpage;
                case "S": return spage;
                case "T": return tpage;
                case "U": return upage;
                case "V": return vpage;
            }
            return null;
        }

        public static long? GetPosBufferAlpha(string token)
        {
            switch (token)
            {
                case "RA": return rp;
                case "ZA": return zp;
                case "XA": return xp;
                case "YA": return yp;
                case "WA": return wp;
                case "SA": return sp;
                case "TA": return tp;
                case "UA": return up;
                case "VA": return vp;
            }
            return null;
        }
    }
}
