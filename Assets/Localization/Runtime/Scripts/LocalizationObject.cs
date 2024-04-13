using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Localization
{
    public class LocalizationObject : MonoBehaviour
    {
        private static LocalizationSettings settings => LocalizationManager.LocalizationSettings;
        
        [SerializeField] private TMP_Text textMPComponent;
        [SerializeField] private TMP_FontAsset uniqueFontTMP;
        
        [Space(10)] 
        [SerializeField] private string text;
        public string ru,
            en,
            tr,
            az,
            be,
            he,
            hy,
            ka,
            et,
            fr,
            kk,
            ky,
            lt,
            lv,
            ro,
            tg,
            tk,
            uk,
            uz,
            es,
            pt,
            ar,
            id,
            ja,
            it,
            de,
            hi;

        public bool changeOnlyFont;
        public int fontNumber;
        int baseFontSize;

        private void Awake()
        {
            // Uncomment the bottom line if you get any errors related to infoYG. In some cases, it may help.
            //Serialize();
            if (textMPComponent)
                baseFontSize = Mathf.RoundToInt(textMPComponent.fontSize);
        }

        [ContextMenu("Reserialize")]
        public void Serialize()
        {
            textMPComponent = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            SwitchLanguage();
            LocalizationManager.SwitchLangEvent += SwitchLanguage;
        }

        private void OnDisable() => LocalizationManager.SwitchLangEvent -= SwitchLanguage;

        public void SwitchLanguage(string lang)
        {
            if (!settings.LocalizationEnable)
                return;
            
            for (int i = 0; i < languages.Length; i++)
            {
                if (lang == LangMethods.LangName(i))
                {
                    if (!changeOnlyFont)
                        AssignTranslate(languages[i]);
                    if (textMPComponent)
                        ChangeFont(LangMethods.GetFontTMP(i, settings));

                    FontSizeCorrect(LangMethods.GetFontSize(i, settings));
                }
            }
        }

        public void SwitchLanguage()
        {
            if (settings.LocalizationEnable)
                SwitchLanguage(LocalizationManager.Lang);
        }

        void AssignTranslate(string translation)
        { 
            if (textMPComponent)
                textMPComponent.text = translation;
        }
        
        public void ChangeFont(TMP_FontAsset[] fontArray)
        {
            TMP_FontAsset font;

            if (fontArray.Length >= fontNumber + 1 && fontArray[fontNumber])
            {
                font = fontArray[fontNumber];
            }
            else font = null;

            if (uniqueFontTMP)
            {
                font = uniqueFontTMP;
            }
            else if (font == null)
            {
                if (settings.Fonts.defaultFont.Length >= fontNumber + 1 && settings.Fonts.defaultFont[fontNumber])
                {
                    font = settings.Fonts.defaultFont[fontNumber];
                }
                else if (settings.Fonts.defaultFont.Length >= 1 && settings.Fonts.defaultFont[0])
                {
                    font = settings.Fonts.defaultFont[0];
                }
            }

            if (font != null)
            {
                textMPComponent.font = font;
            }
        }

        void FontSizeCorrect(int[] fontSizeArray)
        { 
            if (textMPComponent)
                textMPComponent.fontSize = baseFontSize;
            
            if (fontSizeArray.Length != 0 && fontSizeArray.Length - 1 >= fontNumber)
            { 
                if (textMPComponent)
                    textMPComponent.fontSize += fontSizeArray[fontNumber];
            }
        }

        public string[] languages
        {
            get
            {
                string[] s = new string[27];

                s[0] = ru;
                s[1] = en;
                s[2] = tr;
                s[3] = az;
                s[4] = be;
                s[5] = he;
                s[6] = hy;
                s[7] = ka;
                s[8] = et;
                s[9] = fr;
                s[10] = kk;
                s[11] = ky;
                s[12] = lt;
                s[13] = lv;
                s[14] = ro;
                s[15] = tg;
                s[16] = tk;
                s[17] = uk;
                s[18] = uz;
                s[19] = es;
                s[20] = pt;
                s[21] = ar;
                s[22] = id;
                s[23] = ja;
                s[24] = it;
                s[25] = de;
                s[26] = hi;

                return s;
            }
            set
            {
                ru = value[0];
                en = value[1];
                tr = value[2];
                az = value[3];
                be = value[4];
                he = value[5];
                hy = value[6];
                ka = value[7];
                et = value[8];
                fr = value[9];
                kk = value[10];
                ky = value[11];
                lt = value[12];
                lv = value[13];
                ro = value[14];
                tg = value[15];
                tk = value[16];
                uk = value[17];
                uz = value[18];
                es = value[19];
                pt = value[20];
                ar = value[21];
                id = value[22];
                ja = value[23];
                it = value[24];
                de = value[25];
                hi = value[26];
            }
        }
    }
}