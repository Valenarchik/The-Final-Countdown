using System;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    [RequireComponent(typeof(Button))]
    public class ButtonSpriteLocalization: MonoBehaviour
    {
        [SerializeField] private Sprite ru, en, tr, az, be, he, hy, ka, et, fr, kk, ky, lt, lv, ro, tg, tk, uk, uz, es, pt, ar, id, ja, it, de, hi;
        
        [SerializeField] private Sprite h_ru, h_en, h_tr, h_az, h_be, h_he, h_hy, h_ka, h_et, h_fr, h_kk, 
            h_ky, h_lt, h_lv, h_ro, h_tg, h_tk, h_uk, h_uz, h_es, h_pt, h_ar, h_id, h_ja, h_it, h_de, h_hi;
        private Button button;
        private Image targetGraphic;
        
        private LocalizationSettings settings => LocalizationManager.Settings;

        private void Awake()
        {
            button = GetComponent<Button>();
            targetGraphic = GetComponentInChildren<Image>();
        }

        private void OnEnable()
        {
            SwitchSprite();
            LocalizationManager.SwitchLangEvent += SwitchSprite;
        }

        private void OnDisable() => LocalizationManager.SwitchLangEvent -= SwitchSprite;
        
        private void SwitchSprite()
        {
            SwitchSprite(LocalizationManager.lang);
        }

        private void SwitchSprite(string lang)
        {
            if (!settings.LocalizationEnable)
                return;
            Debug.Log(lang);
            for (var i = 0; i < targetSprites.Length; i++)
            {
                if (lang == LangMethods.LangName(i))
                {
                    Debug.Log("Switch");
                    targetGraphic.sprite = targetSprites[i];
                    button.spriteState = new SpriteState() { highlightedSprite = highligthedSprites[i] };
                }
            }
        }
        
        public Sprite[] targetSprites
        {
            get
            {
                var s = new Sprite[27];

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
        
        public Sprite[] highligthedSprites
        {
            get
            {
                var s = new Sprite[27];

                s[0] = h_ru;
                s[1] = h_en;
                s[2] = h_tr;
                s[3] = h_az;
                s[4] = h_be;
                s[5] = h_he;
                s[6] = h_hy;
                s[7] = h_ka;
                s[8] = h_et;
                s[9] = h_fr;
                s[10] = h_kk;
                s[11] = h_ky;
                s[12] = h_lt;
                s[13] = h_lv;
                s[14] = h_ro;
                s[15] = h_tg;
                s[16] = h_tk;
                s[17] = h_uk;
                s[18] = h_uz;
                s[19] = h_es;
                s[20] = h_pt;
                s[21] = h_ar;
                s[22] = h_id;
                s[23] = h_ja;
                s[24] = h_it;
                s[25] = h_de;
                s[26] = h_hi;

                return s;
            }
            set
            {
                h_ru = value[0];
                h_en = value[1];
                h_tr = value[2];
                h_az = value[3];
                h_be = value[4];
                h_he = value[5];
                h_hy = value[6];
                h_ka = value[7];
                h_et = value[8];
                h_fr = value[9];
                h_kk = value[10];
                h_ky = value[11];
                h_lt = value[12];
                h_lv = value[13];
                h_ro = value[14];
                h_tg = value[15];
                h_tk = value[16];
                h_uk = value[17];
                h_uz = value[18];
                h_es = value[19];
                h_pt = value[20];
                h_ar = value[21];
                h_id = value[22];
                h_ja = value[23];
                h_it = value[24];
                h_de = value[25];
                h_hi = value[26];
            }
        }
    }
}