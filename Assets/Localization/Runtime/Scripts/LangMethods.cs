using UnityEngine;
using TMPro;

namespace Localization
{
    public class LangMethods
    {
        public static bool[] LangArr(LocalizationSettings inf)
        {
            bool[] b = new bool[27];

            b[0] = inf.Languages.ru;
            b[1] = inf.Languages.en;
            b[2] = inf.Languages.tr;
            b[3] = inf.Languages.az;
            b[4] = inf.Languages.be;
            b[5] = inf.Languages.he;
            b[6] = inf.Languages.hy;
            b[7] = inf.Languages.ka;
            b[8] = inf.Languages.et;
            b[9] = inf.Languages.fr;
            b[10] = inf.Languages.kk;
            b[11] = inf.Languages.ky;
            b[12] = inf.Languages.lt;
            b[13] = inf.Languages.lv;
            b[14] = inf.Languages.ro;
            b[15] = inf.Languages.tg;
            b[16] = inf.Languages.tk;
            b[17] = inf.Languages.uk;
            b[18] = inf.Languages.uz;
            b[19] = inf.Languages.es;
            b[20] = inf.Languages.pt;
            b[21] = inf.Languages.ar;
            b[22] = inf.Languages.id;
            b[23] = inf.Languages.ja;
            b[24] = inf.Languages.it;
            b[25] = inf.Languages.de;
            b[26] = inf.Languages.hi;

            return b;
        }

        public static string LangName(int i)
        {
            return i switch
            {
                0 => "ru",
                1 => "en",
                2 => "tr",
                3 => "az",
                4 => "be",
                5 => "he",
                6 => "hy",
                7 => "ka",
                8 => "et",
                9 => "fr",
                10 => "kk",
                11 => "ky",
                12 => "lt",
                13 => "lv",
                14 => "ro",
                15 => "tg",
                16 => "tk",
                17 => "uk",
                18 => "uz",
                19 => "es",
                20 => "pt",
                21 => "ar",
                22 => "id",
                23 => "ja",
                24 => "it",
                25 => "de",
                26 => "hi",
                _ => null
            };
        }
        
        public static TMP_FontAsset[] GetFontTMP(int i, LocalizationSettings inf)
        {
            return i switch
            {
                0 => inf.Fonts.ru,
                1 => inf.Fonts.en,
                2 => inf.Fonts.tr,
                3 => inf.Fonts.az,
                4 => inf.Fonts.be,
                5 => inf.Fonts.he,
                6 => inf.Fonts.hy,
                7 => inf.Fonts.ka,
                8 => inf.Fonts.et,
                9 => inf.Fonts.fr,
                10 => inf.Fonts.kk,
                11 => inf.Fonts.ky,
                12 => inf.Fonts.lt,
                13 => inf.Fonts.lv,
                14 => inf.Fonts.ro,
                15 => inf.Fonts.tg,
                16 => inf.Fonts.tk,
                17 => inf.Fonts.uk,
                18 => inf.Fonts.uz,
                19 => inf.Fonts.es,
                20 => inf.Fonts.pt,
                21 => inf.Fonts.ar,
                22 => inf.Fonts.id,
                23 => inf.Fonts.ja,
                24 => inf.Fonts.it,
                25 => inf.Fonts.de,
                26 => inf.Fonts.hi,
                _ => null
            };
        }

        public static int[] GetFontSize(int i, LocalizationSettings inf)
        {
            return i switch
            {
                0 => inf.FontsSizeCorrect.ru,
                1 => inf.FontsSizeCorrect.en,
                2 => inf.FontsSizeCorrect.tr,
                3 => inf.FontsSizeCorrect.az,
                4 => inf.FontsSizeCorrect.be,
                5 => inf.FontsSizeCorrect.he,
                6 => inf.FontsSizeCorrect.hy,
                7 => inf.FontsSizeCorrect.ka,
                8 => inf.FontsSizeCorrect.et,
                9 => inf.FontsSizeCorrect.fr,
                10 => inf.FontsSizeCorrect.kk,
                11 => inf.FontsSizeCorrect.ky,
                12 => inf.FontsSizeCorrect.lt,
                13 => inf.FontsSizeCorrect.lv,
                14 => inf.FontsSizeCorrect.ro,
                15 => inf.FontsSizeCorrect.tg,
                16 => inf.FontsSizeCorrect.tk,
                17 => inf.FontsSizeCorrect.uk,
                18 => inf.FontsSizeCorrect.uz,
                19 => inf.FontsSizeCorrect.es,
                20 => inf.FontsSizeCorrect.pt,
                21 => inf.FontsSizeCorrect.ar,
                22 => inf.FontsSizeCorrect.id,
                23 => inf.FontsSizeCorrect.ja,
                24 => inf.FontsSizeCorrect.it,
                25 => inf.FontsSizeCorrect.de,
                26 => inf.FontsSizeCorrect.hi,
                _ => null
            };
        }

        public static string UnauthorizedTextTranslate(string languageTranslate)
        {
            string name;

            switch (languageTranslate)
            {
                case "ru":
                    name = "неавторизованный";
                    break;
                case "en":
                    name = "unauthorized";
                    break;
                case "tr":
                    name = "yetkisiz";
                    break;
                case "az":
                    name = "icazəsiz";
                    break;
                case "be":
                    name = "неаўтарызаваны";
                    break;
                case "he":
                    name = "---";
                    break;
                case "hy":
                    name = "---";
                    break;
                case "ka":
                    name = "---";
                    break;
                case "et":
                    name = "loata";
                    break;
                case "fr":
                    name = "non autorisé";
                    break;
                case "kk":
                    name = "рұқсат етілмеген";
                    break;
                case "ky":
                    name = "уруксатсыз";
                    break;
                case "lt":
                    name = "neleistinas";
                    break;
                case "lv":
                    name = "neleistinas";
                    break;
                case "ro":
                    name = "neautorizat";
                    break;
                case "tg":
                    name = "беиҷозат";
                    break;
                case "tk":
                    name = "yetkisiz";
                    break;
                case "uk":
                    name = "несанкціонований";
                    break;
                case "uz":
                    name = "ruxsatsiz";
                    break;
                case "es":
                    name = "autorizado";
                    break;
                case "pt":
                    name = "autorizado";
                    break;
                case "ar":
                    name = "---";
                    break;
                case "id":
                    name = "tidak sah";
                    break;
                case "ja":
                    name = "---";
                    break;
                case "it":
                    name = "autorizzato";
                    break;
                case "de":
                    name = "unerlaubter";
                    break;
                case "hi":
                    name = "---";
                    break;
                default:
                    name = "unauthorized";
                    break;
            }

            return name;
        }
        public static string IsHiddenTextTranslate(string languageTranslate)
        {
            string name;

            switch (languageTranslate)
            {
                case "ru":
                    name = "скрыт";
                    break;
                case "en":
                    name = "is hidden";
                    break;
                case "tr":
                    name = "gizli";
                    break;
                case "az":
                    name = "gizlidir";
                    break;
                case "be":
                    name = "скрыты";
                    break;
                case "he":
                    name = "---";
                    break;
                case "hy":
                    name = "---";
                    break;
                case "ka":
                    name = "---";
                    break;
                case "et":
                    name = "on peidetud";
                    break;
                case "fr":
                    name = "est caché";
                    break;
                case "kk":
                    name = "жасырылған";
                    break;
                case "ky":
                    name = "жашыруун";
                    break;
                case "lt":
                    name = "yra paslėpta";
                    break;
                case "lv":
                    name = "ir paslēpts";
                    break;
                case "ro":
                    name = "este ascuns";
                    break;
                case "tg":
                    name = "пинҳон аст";
                    break;
                case "tk":
                    name = "gizlenendir";
                    break;
                case "uk":
                    name = "прихований";
                    break;
                case "uz":
                    name = "yashiringan";
                    break;
                case "es":
                    name = "Está oculto";
                    break;
                case "pt":
                    name = "está escondido";
                    break;
                case "ar":
                    name = "---";
                    break;
                case "id":
                    name = "tersembunyi";
                    break;
                case "ja":
                    name = "---";
                    break;
                case "it":
                    name = "è nascosto";
                    break;
                case "de":
                    name = "ist versteckt";
                    break;
                case "hi":
                    name = "---";
                    break;
                default:
                    name = "is hidden";
                    break;
            }

            return name;
        }
    }
}