// MemPos.GetSize.cs - 07/18/2018

namespace IDRIS.Runtime
{
    public static partial class MemPos
    {
        public static long? GetSizeNumeric(string token)
        {
            switch (token)
            {
                case "PROG": return 2;
                case "USER": return 2;
                case "ORIG": return 2;
                case "OPER": return 2;
                case "G": return 2;
                case "N": return numslotsize;
                case "N1": return numslotsize;
                case "N2": return numslotsize;
                case "N3": return numslotsize;
                case "N4": return numslotsize;
                case "N5": return numslotsize;
                case "N6": return numslotsize;
                case "N7": return numslotsize;
                case "N8": return numslotsize;
                case "N9": return numslotsize;
                case "N10": return numslotsize;
                case "N11": return numslotsize;
                case "N12": return numslotsize;
                case "N13": return numslotsize;
                case "N14": return numslotsize;
                case "N15": return numslotsize;
                case "N16": return numslotsize;
                case "N17": return numslotsize;
                case "N18": return numslotsize;
                case "N19": return numslotsize;
                case "N20": return numslotsize;
                case "N21": return numslotsize;
                case "N22": return numslotsize;
                case "N23": return numslotsize;
                case "N24": return numslotsize;
                case "N25": return numslotsize;
                case "N26": return numslotsize;
                case "N27": return numslotsize;
                case "N28": return numslotsize;
                case "N29": return numslotsize;
                case "N30": return numslotsize;
                case "N31": return numslotsize;
                case "N32": return numslotsize;
                case "N33": return numslotsize;
                case "N34": return numslotsize;
                case "N35": return numslotsize;
                case "N36": return numslotsize;
                case "N37": return numslotsize;
                case "N38": return numslotsize;
                case "N39": return numslotsize;
                case "N40": return numslotsize;
                case "N41": return numslotsize;
                case "N42": return numslotsize;
                case "N43": return numslotsize;
                case "N44": return numslotsize;
                case "N45": return numslotsize;
                case "N46": return numslotsize;
                case "N47": return numslotsize;
                case "N48": return numslotsize;
                case "N49": return numslotsize;
                case "N50": return numslotsize;
                case "N51": return numslotsize;
                case "N52": return numslotsize;
                case "N53": return numslotsize;
                case "N54": return numslotsize;
                case "N55": return numslotsize;
                case "N56": return numslotsize;
                case "N57": return numslotsize;
                case "N58": return numslotsize;
                case "N59": return numslotsize;
                case "N60": return numslotsize;
                case "N61": return numslotsize;
                case "N62": return numslotsize;
                case "N63": return numslotsize;
                case "N64": return numslotsize;
                case "N65": return numslotsize;
                case "N66": return numslotsize;
                case "N67": return numslotsize;
                case "N68": return numslotsize;
                case "N69": return numslotsize;
                case "N70": return numslotsize;
                case "N71": return numslotsize;
                case "N72": return numslotsize;
                case "N73": return numslotsize;
                case "N74": return numslotsize;
                case "N75": return numslotsize;
                case "N76": return numslotsize;
                case "N77": return numslotsize;
                case "N78": return numslotsize;
                case "N79": return numslotsize;
                case "N80": return numslotsize;
                case "N81": return numslotsize;
                case "N82": return numslotsize;
                case "N83": return numslotsize;
                case "N84": return numslotsize;
                case "N85": return numslotsize;
                case "N86": return numslotsize;
                case "N87": return numslotsize;
                case "N88": return numslotsize;
                case "N89": return numslotsize;
                case "N90": return numslotsize;
                case "N91": return numslotsize;
                case "N92": return numslotsize;
                case "N93": return numslotsize;
                case "N94": return numslotsize;
                case "N95": return numslotsize;
                case "N96": return numslotsize;
                case "N97": return numslotsize;
                case "N98": return numslotsize;
                case "N99": return numslotsize;
                case "REC": return numslotsize;
                case "REM": return numslotsize;
            }
            return numslotsize;
        }
    }
}
