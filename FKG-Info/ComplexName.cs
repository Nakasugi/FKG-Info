using System;

namespace FKG_Info
{
    public class ComplexName
    {
        public string Kanji;
        public string Romaji;
        public string EngNutaku;
        public string EngDMM;

        public ComplexName()
        {
            Kanji = null;
            Romaji = null;
            EngNutaku = null;
            EngDMM = null;
        }

        public void AutoRomaji()
        {
            int pos = 0;
            int len = Kanji.Length;

            char chKnCurr, chKnNext='\0', chRmLast='\0';

            Romaji = "";

            while (pos < len)
            {
                chKnCurr = Kanji[pos];
                
                if (pos + 1 < len) chKnNext = Kanji[pos + 1];
                if (Romaji.Length > 0) chRmLast = Romaji[Romaji.Length - 1];

                switch (chKnCurr)
                {
                    case 'ア': Romaji += "a"; break;
                    case 'イ': Romaji += "i"; break;
                    case 'ウ':
                        switch (chKnNext)
                        {
                            case 'ィ': Romaji += "wi"; pos++; break;
                            case 'ェ': Romaji += "we"; pos++; break;
                            case 'ォ': Romaji += "wo"; pos++; break;
                            default: Romaji += "u"; break;
                        }
                        break;
                    case 'エ': Romaji += "e"; break;
                    case 'オ': Romaji += "o"; break;

                    case 'カ': Romaji += "ka"; break;
                    case 'キ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "kya"; pos++; break;
                            case 'ュ': Romaji += "kyu"; pos++; break;
                            case 'ョ': Romaji += "kyo"; pos++; break;
                            default: Romaji += "ki"; break;
                        }
                        break;
                    case 'ク': Romaji += "ku"; break;
                    case 'ケ': Romaji += "ke"; break;
                    case 'コ': Romaji += "ko"; break;

                    case 'サ': Romaji += "sa"; break;
                    case 'シ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "sha"; pos++; break;
                            case 'ュ': Romaji += "shu"; pos++; break;
                            case 'ェ': Romaji += "she"; pos++; break;
                            case 'ョ': Romaji += "sho"; pos++; break;
                            default: Romaji += "shi"; break;
                        }
                        break;
                    case 'ス': Romaji += "su"; break;
                    case 'セ': Romaji += "se"; break;
                    case 'ソ': Romaji += "su"; break;

                    case 'タ': Romaji += "ta"; break;
                    case 'チ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "cha"; pos++; break;
                            case 'ィ': Romaji += "ti"; pos++; break;
                            case 'ュ': Romaji += "chu"; pos++; break;
                            case 'ェ': Romaji += "che"; pos++; break;
                            case 'ョ': Romaji += "cho"; pos++; break;
                            default: Romaji += "chi"; break;
                        }
                        break;
                    case 'ツ': Romaji += "tsu"; break;
                    case 'テ':
                        switch (chKnNext)
                        {
                            case 'ィ': Romaji += "ti"; pos++; break;
                            default: Romaji += "te"; break;
                        }
                        break;
                    case 'ト':
                        switch (chKnNext)
                        {
                            case 'ゥ': Romaji += "tu"; pos++; break;
                            default: Romaji += "to"; break;
                        }
                        break;

                    case 'ナ': Romaji += "na"; break;
                    case 'ニ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "nya"; pos++; break;
                            case 'ュ': Romaji += "nyu"; pos++; break;
                            case 'ョ': Romaji += "nyo"; pos++; break;
                            default: Romaji += "ni"; break;
                        }
                        break;
                    case 'ヌ': Romaji += "nu"; break;
                    case 'ネ': Romaji += "ne"; break;
                    case 'ノ': Romaji += "no"; break;

                    case 'ハ': Romaji += "ha"; break;
                    case 'ヒ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "hya"; pos++; break;
                            case 'ュ': Romaji += "hyu"; pos++; break;
                            case 'ョ': Romaji += "hyo"; pos++; break;
                            default: Romaji += "hi"; break;
                        }
                        break;
                    case 'フ':
                        switch (chKnNext)
                        {
                            case 'ィ': Romaji += "fi"; pos++; break;
                            case 'ュ': Romaji += "fyu"; pos++; break;
                            case 'ェ': Romaji += "fe"; pos++; break;
                            case 'ォ': Romaji += "fo"; pos++; break;
                            default: Romaji += "fu"; break;
                        }
                        break;
                    case 'ヘ': Romaji += "he"; break;
                    case 'ホ': Romaji += "ho"; break;

                    case 'マ': Romaji += "ma"; break;
                    case 'ミ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "mya"; pos++; break;
                            case 'ュ': Romaji += "myu"; pos++; break;
                            case 'ョ': Romaji += "myo"; pos++; break;
                            default: Romaji += "mi"; break;
                        }
                        break;
                    case 'ム': Romaji += "mu"; break;
                    case 'メ': Romaji += "me"; break;
                    case 'モ': Romaji += "mo"; break;

                    case 'ヤ': Romaji += "ya"; break;
                    case 'ユ': Romaji += "yu"; break;
                    case 'ヨ': Romaji += "yo"; break;

                    case 'ラ': Romaji += "ra"; break;
                    case 'リ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "rya"; pos++; break;
                            case 'ュ': Romaji += "ryu"; pos++; break;
                            case 'ョ': Romaji += "ryo"; pos++; break;
                            default: Romaji += "ri"; break;
                        }
                        break;
                    case 'ル': Romaji += "ru"; break;
                    case 'レ': Romaji += "re"; break;
                    case 'ロ': Romaji += "ro"; break;

                    case 'ワ': Romaji += "wa"; break;
                    case 'ヲ': Romaji += "wo"; break;
                    case 'ン': Romaji += "n"; break;


                    case 'ガ': Romaji += "ga"; break;
                    case 'ギ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "gya"; pos++; break;
                            case 'ュ': Romaji += "gyu"; pos++; break;
                            case 'ョ': Romaji += "gyo"; pos++; break;
                            default: Romaji += "gi"; break;
                        }
                        break;
                    case 'グ': Romaji += "gu"; break;
                    case 'ゲ': Romaji += "ge"; break;
                    case 'ゴ': Romaji += "go"; break;

                    case 'ザ': Romaji += "za"; break;
                    case 'ジ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "ja"; pos++; break;
                            case 'ュ': Romaji += "ju"; pos++; break;
                            case 'ェ': Romaji += "je"; pos++; break;
                            case 'ョ': Romaji += "jo"; pos++; break;
                            default: Romaji += "ji"; break;
                        }
                        break;
                    case 'ズ': Romaji += "zu"; break;
                    case 'ゼ': Romaji += "ze"; break;
                    case 'ゾ': Romaji += "zo"; break;

                    case 'ダ': Romaji += "da"; break;
                    case 'ヂ':
                        switch (chKnNext)
                        {
                            case 'ィ': Romaji += "di"; pos++; break;
                            default: Romaji += "ji"; break;
                        }
                        break;
                    case 'ヅ': Romaji += "zu"; break;
                    case 'デ':
                        switch (chKnNext)
                        {
                            case 'ィ': Romaji += "di"; pos++; break;
                            default: Romaji += "de"; break;
                        }
                        break;
                    case 'ド':
                        switch (chKnNext)
                        {
                            case 'ゥ': Romaji += "du"; pos++; break;
                            case 'ュ': Romaji += "dyu"; pos++; break;
                            default: Romaji += "do"; break;
                        }
                        break;

                    case 'バ': Romaji += "ba"; break;
                    case 'ビ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "bya"; pos++; break;
                            case 'ュ': Romaji += "byu"; pos++; break;
                            case 'ョ': Romaji += "byo"; pos++; break;
                            default: Romaji += "bi"; break;
                        }
                        break;
                    case 'ブ': Romaji += "bu"; break;
                    case 'ベ': Romaji += "be"; break;
                    case 'ボ': Romaji += "bo"; break;

                    case 'パ': Romaji += "pa"; break;
                    case 'ピ':
                        switch (chKnNext)
                        {
                            case 'ャ': Romaji += "pya"; pos++; break;
                            case 'ュ': Romaji += "pyu"; pos++; break;
                            case 'ョ': Romaji += "pyo"; pos++; break;
                            default: Romaji += "pi"; break;
                        }
                        break;
                    case 'プ': Romaji += "pu"; break;
                    case 'ペ': Romaji += "pe"; break;
                    case 'ポ': Romaji += "po"; break;

                    case 'ヴ':
                        switch (chKnNext)
                        {
                            case 'ァ': Romaji += "va"; pos++; break;
                            case 'ィ': Romaji += "vi"; pos++; break;
                            case 'ェ': Romaji += "ve"; pos++; break;
                            case 'ォ': Romaji += "vo"; pos++; break;
                            default: Romaji += "vu"; break;
                        }
                        break;

                    case 'ー':
                        switch (chRmLast)
                        {
                            case 'a': Romaji += "a"; break;
                            case 'i': Romaji += "i"; break;
                            case 'u': Romaji += "u"; break;
                            case 'e': Romaji += "e"; break;
                            case 'o': Romaji += "u"; break;
                        }
                        break;

                    case 'ッ':
                        if ((chKnNext == 'カ') || (chKnNext == 'キ') || (chKnNext == 'ク') || (chKnNext == 'ケ') || (chKnNext == 'コ')) Romaji += "k";
                        if ((chKnNext == 'サ') || (chKnNext == 'シ') || (chKnNext == 'ス') || (chKnNext == 'セ') || (chKnNext == 'ソ')) Romaji += "s";
                        if ((chKnNext == 'タ') || (chKnNext == 'チ') || (chKnNext == 'ツ') || (chKnNext == 'テ') || (chKnNext == 'ト')) Romaji += "t";
                        if ((chKnNext == 'ナ') || (chKnNext == 'ニ') || (chKnNext == 'ヌ') || (chKnNext == 'ネ') || (chKnNext == 'ノ')) Romaji += "n";
                        if ((chKnNext == 'ハ') || (chKnNext == 'ヒ') || (chKnNext == 'ヘ') || (chKnNext == 'ホ')) Romaji += "h";
                        if ((chKnNext == 'マ') || (chKnNext == 'ミ') || (chKnNext == 'ム') || (chKnNext == 'メ') || (chKnNext == 'モ')) Romaji += "m";
                        if ((chKnNext == 'ラ') || (chKnNext == 'リ') || (chKnNext == 'ル') || (chKnNext == 'レ') || (chKnNext == 'ロ')) Romaji += "r";
                        if ((chKnNext == 'ガ') || (chKnNext == 'ギ') || (chKnNext == 'グ') || (chKnNext == 'ゲ') || (chKnNext == 'ゴ')) Romaji += "g";
                        if ((chKnNext == 'ザ') || (chKnNext == 'ズ') || (chKnNext == 'ゼ') || (chKnNext == 'ゾ') || (chKnNext == 'ヅ')) Romaji += "z";
                        if ((chKnNext == 'ダ') || (chKnNext == 'デ') || (chKnNext == 'ド')) Romaji += "d";
                        if ((chKnNext == 'バ') || (chKnNext == 'ビ') || (chKnNext == 'ブ') || (chKnNext == 'ベ') || (chKnNext == 'ボ')) Romaji += "b";
                        if ((chKnNext == 'パ') || (chKnNext == 'ピ') || (chKnNext == 'プ') || (chKnNext == 'ペ') || (chKnNext == 'ポ')) Romaji += "p";
                        if ((chKnNext == 'ジ') || (chKnNext == 'ヂ')) Romaji += "j";
                        break;

                    default: pos = len; break;
                }

                pos++;
            }

            if (Romaji.Length > 0) Romaji = Char.ToUpper(Romaji[0]) + Romaji.Remove(0, 1);
            //Romaji.ToTitleCase();

        }
    }
}
