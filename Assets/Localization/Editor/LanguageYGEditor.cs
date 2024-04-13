using Localization;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using TMPro;

namespace Localization
{
    [CustomEditor(typeof(Language))]
    public class LanguageYGEditor : Editor
    {
        Language scr;

        GUIStyle red;
        GUIStyle green;

        int processTranslateLabel;

        private void OnEnable()
        {
            scr = (Language)target;
            scr.Serialize();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            scr = (Language)target;
            Undo.RecordObject(scr, "Undo LanguageYG");

            red = new GUIStyle(EditorStyles.label);
            red.normal.textColor = Color.red;
            green = new GUIStyle(EditorStyles.label);
            green.normal.textColor = Color.green;

            bool isNullTextComponent = scr.textMPComponent == null;


            if (isNullTextComponent)
            {
                if (GUILayout.Button("Add component - Text Mesh Pro UGUI", GUILayout.Height(23)))
                {
                    scr.textMPComponent = scr.gameObject.AddComponent<TextMeshProUGUI>();
                }
                return;
            }

            if (scr.settings)
            {
                if (scr.settings.translateMethod == TranslateMethod.CSVFile)
                {
                    GUILayout.BeginVertical("HelpBox");

                    scr.componentTextField = EditorGUILayout.ToggleLeft("Component Text/TextMeshPro Translate", scr.componentTextField);

                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button(">", GUILayout.Width(20)))
                    {
                        TranslationTableEditorWindow.ShowWindow();
                    }

                    bool availableStr = true;

                    if (scr.componentTextField)
                    {
                        if (scr.textMPComponent)
                        {
                            GUILayout.Label(scr.textMPComponent.text);

                            if (scr.textMPComponent == null || scr.textMPComponent.text.Length == 0)
                                availableStr = false;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(scr.text))
                            availableStr = false;

                        scr.text = EditorGUILayout.TextField(scr.text, GUILayout.Height(20));
                    }

                    if (availableStr)
                    {
                        GUILayout.Label("ID Translate");
                    }
                    else
                    {
                        GUILayout.Label("ID Translate (necessarily)", red);
                    }

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button("Import"))
                    {
                        string[] translfers = CSVManager.ImportTransfersByKey(scr);
                        if (translfers != null)
                            scr.languages = CSVManager.ImportTransfersByKey(scr);
                    }
                    if (GUILayout.Button("Export"))
                    {
                        CSVManager.SetIDLineFile(scr.settings, scr);
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();

                    scr.textHeight = EditorGUILayout.Slider("Text Height", scr.textHeight, 20f, 400f);
                    UpdateLanguages(true);
                }
                else
                {
                    if (scr.settings.translateMethod == TranslateMethod.AutoLocalization)
                    {
                        GUILayout.BeginVertical("HelpBox");

                        scr.componentTextField = EditorGUILayout.ToggleLeft("Component Text/TextMeshPro Translate", scr.componentTextField);
                        scr.textHeight = EditorGUILayout.Slider("Text Height", scr.textHeight, 20f, 400f);

                        if (!scr.componentTextField)
                            scr.text = EditorGUILayout.TextArea(scr.text, GUILayout.Height(scr.textHeight));
                        else
                        { 
                            if (scr.textMPComponent)
                                GUILayout.Label(scr.textMPComponent.text);
                        }

                        GUILayout.BeginHorizontal();

                        if (scr.componentTextField)
                        { 
                            if (scr.textMPComponent)
                            {
                                if (scr.textMPComponent.text != null && scr.textMPComponent.text.Length > 0)
                                {
                                    GUILayout.Label("TextMeshPro Component", green);

                                    if (GUILayout.Button("TRANSLATE"))
                                        TranslateButton();
                                }
                                else
                                    GUILayout.Label("TextMeshPro Component", red);
                            }
                        }
                        else
                        {
                            if (scr.componentTextField || string.IsNullOrEmpty(scr.text))
                            {
                                GUILayout.Label("FILL IN THE FIELD", red);
                            }
                            else if (GUILayout.Button("TRANSLATE"))
                                TranslateButton();
                        }

                        if (GUILayout.Button("CLEAR"))
                        {
                            scr.ru = "";
                            scr.en = "";
                            scr.tr = "";
                            scr.az = "";
                            scr.be = "";
                            scr.he = "";
                            scr.hy = "";
                            scr.ka = "";
                            scr.et = "";
                            scr.fr = "";
                            scr.kk = "";
                            scr.ky = "";
                            scr.lt = "";
                            scr.lv = "";
                            scr.ro = "";
                            scr.tg = "";
                            scr.tk = "";
                            scr.uk = "";
                            scr.uz = "";
                            scr.es = "";
                            scr.pt = "";
                            scr.ar = "";
                            scr.id = "";
                            scr.ja = "";
                            scr.it = "";
                            scr.de = "";
                            scr.hi = "";

                            scr.processTranslateLabel = "";
                            scr.countLang = processTranslateLabel;
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();
                    }

                    GUILayout.BeginVertical("box");
                    GUILayout.BeginHorizontal();

                    bool labelProcess = false;

                    if (scr.settings.translateMethod == TranslateMethod.AutoLocalization)
                    {
                        if (scr.processTranslateLabel != "")
                        {
                            if (scr.countLang == processTranslateLabel)
                            {
                                GUILayout.Label(scr.processTranslateLabel, green, GUILayout.Height(20));
                                labelProcess = true;
                            }
                            else if (scr.processTranslateLabel == "")
                            {
                                labelProcess = true;
                            }
                            else
                            {
                                GUILayout.Label(scr.processTranslateLabel, GUILayout.Height(20));
                                labelProcess = true;
                            }

                            try
                            {
                                if (scr.processTranslateLabel.Contains("error"))
                                {
                                    GUILayout.Label(scr.processTranslateLabel, red, GUILayout.Height(20));
                                    labelProcess = true;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (labelProcess == false)
                        GUILayout.Label(processTranslateLabel + " Languages", GUILayout.Height(20));

                    try
                    {
                        if (!scr.processTranslateLabel.Contains("completed"))
                            GUILayout.Label("Go back to the inspector!", GUILayout.Height(20));
                    }
                    catch
                    {
                    }

                    GUILayout.EndHorizontal();

                    UpdateLanguages(false);
                    GUILayout.EndVertical();
                }
            }

            if (scr.textMPComponent)
            {
                GUILayout.Space(10);
                GUILayout.BeginVertical("box");
                if (scr.textMPComponent)
                {
                    scr.uniqueFontTMP = (TMP_FontAsset)EditorGUILayout.ObjectField("Unique Font", scr.uniqueFontTMP, typeof(TMP_FontAsset), false);
                    FontTMPSettingsDraw();
                }
                GUILayout.EndVertical();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(scr.gameObject);
                EditorSceneManager.MarkSceneDirty(scr.gameObject.scene);
            }
        }

        readonly string buttonText_ReplaseFont = "Replace the font with the standard one";
        
        void FontTMPSettingsDraw()
        {
            if (scr.settings.fonts.defaultFont.Length == 0)
                return;

            scr.fontNumber = Mathf.Clamp(scr.fontNumber, 0, scr.settings.fonts.defaultFont.Length - 1);
            if (scr.settings.fonts.defaultFont.Length > 1)
                scr.fontNumber = EditorGUILayout.IntField("Font Index (in array default fonts)", scr.fontNumber);

            if (scr.textMPComponent.font == scr.settings.fonts.defaultFont[scr.fontNumber])
                return;

            if (GUILayout.Button(buttonText_ReplaseFont))
            {
                Undo.RecordObject(scr.textMPComponent, "Undo TextMPComponent.font");
                scr.textMPComponent.font = scr.settings.fonts.defaultFont[scr.fontNumber];
            }
        }

        void TranslateButton()
        {
            scr.processTranslateLabel = "";
            scr.Translate(processTranslateLabel);
        }

        void UpdateLanguages(bool CSVFile)
        {
            processTranslateLabel = 0;
            bool[] langArr = LangMethods.LangArr(scr.settings);

            for (int i = 0; i < langArr.Length; i++)
            {
                if (langArr[i])
                {
                    processTranslateLabel++;
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(new GUIContent(LangMethods.LangName(i), CSVManager.FullNameLanguages()[i]), GUILayout.Width(20), GUILayout.Height(20));

                    switch (i)
                    {
                        case 0:
                            scr.ru = EditorGUILayout.TextArea(scr.ru, GUILayout.Height(scr.textHeight));
                            break;
                        case 1:
                            scr.en = EditorGUILayout.TextArea(scr.en, GUILayout.Height(scr.textHeight));
                            break;
                        case 2:
                            scr.tr = EditorGUILayout.TextArea(scr.tr, GUILayout.Height(scr.textHeight));
                            break;
                        case 3:
                            scr.az = EditorGUILayout.TextArea(scr.az, GUILayout.Height(scr.textHeight));
                            break;
                        case 4:
                            scr.be = EditorGUILayout.TextArea(scr.be, GUILayout.Height(scr.textHeight));
                            break;
                        case 5:
                            scr.he = EditorGUILayout.TextArea(scr.he, GUILayout.Height(scr.textHeight));
                            break;
                        case 6:
                            scr.hy = EditorGUILayout.TextArea(scr.hy, GUILayout.Height(scr.textHeight));
                            break;
                        case 7:
                            scr.ka = EditorGUILayout.TextArea(scr.ka, GUILayout.Height(scr.textHeight));
                            break;
                        case 8:
                            scr.et = EditorGUILayout.TextArea(scr.et, GUILayout.Height(scr.textHeight));
                            break;
                        case 9:
                            scr.fr = EditorGUILayout.TextArea(scr.fr, GUILayout.Height(scr.textHeight));
                            break;
                        case 10:
                            scr.kk = EditorGUILayout.TextArea(scr.kk, GUILayout.Height(scr.textHeight));
                            break;
                        case 11:
                            scr.ky = EditorGUILayout.TextArea(scr.ky, GUILayout.Height(scr.textHeight));
                            break;
                        case 12:
                            scr.lt = EditorGUILayout.TextArea(scr.lt, GUILayout.Height(scr.textHeight));
                            break;
                        case 13:
                            scr.lv = EditorGUILayout.TextArea(scr.lv, GUILayout.Height(scr.textHeight));
                            break;
                        case 14:
                            scr.ro = EditorGUILayout.TextArea(scr.ro, GUILayout.Height(scr.textHeight));
                            break;
                        case 15:
                            scr.tg = EditorGUILayout.TextArea(scr.tg, GUILayout.Height(scr.textHeight));
                            break;
                        case 16:
                            scr.tk = EditorGUILayout.TextArea(scr.tk, GUILayout.Height(scr.textHeight));
                            break;
                        case 17:
                            scr.uk = EditorGUILayout.TextArea(scr.uk, GUILayout.Height(scr.textHeight));
                            break;
                        case 18:
                            scr.uz = EditorGUILayout.TextArea(scr.uz, GUILayout.Height(scr.textHeight));
                            break;
                        case 19:
                            scr.es = EditorGUILayout.TextArea(scr.es, GUILayout.Height(scr.textHeight));
                            break;
                        case 20:
                            scr.pt = EditorGUILayout.TextArea(scr.pt, GUILayout.Height(scr.textHeight));
                            break;
                        case 21:
                            scr.ar = EditorGUILayout.TextArea(scr.ar, GUILayout.Height(scr.textHeight));
                            break;
                        case 22:
                            scr.id = EditorGUILayout.TextArea(scr.id, GUILayout.Height(scr.textHeight));
                            break;
                        case 23:
                            scr.ja = EditorGUILayout.TextArea(scr.ja, GUILayout.Height(scr.textHeight));
                            break;
                        case 24:
                            scr.it = EditorGUILayout.TextArea(scr.it, GUILayout.Height(scr.textHeight));
                            break;
                        case 25:
                            scr.de = EditorGUILayout.TextArea(scr.de, GUILayout.Height(scr.textHeight));
                            break;
                        case 26:
                            scr.hi = EditorGUILayout.TextArea(scr.hi, GUILayout.Height(scr.textHeight));
                            break;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }
    }
}
