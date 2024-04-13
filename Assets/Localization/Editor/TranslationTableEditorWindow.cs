using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Localization
{
    public class TranslationTableEditorWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            GetWindow<TranslationTableEditorWindow>("Translation Table");
        }

        Vector2 scrollPosition = Vector2.zero;

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true, GUILayout.Width(position.width), GUILayout.Height(position.height));

            int countSelectObj = 0;

            foreach (GameObject obj in Selection.gameObjects)
            {
                countSelectObj++;
                var langYG = obj.GetComponent<Language>();

                if (langYG)
                {
                    TextAsset data = Resources.Load(langYG.settings.CSVFileTranslate.name) as TextAsset;
                    string[] lines = Regex.Split(CSVManager.CommaFormat(data.text), "\n");

                    GUILayout.BeginHorizontal();
                    string[] keys = Regex.Split(lines[0], ",");

                    for (int i = 0; i < keys.Length; i++)
                    {
                        GUILayout.BeginVertical("Box");
                        if (i == 0) GUILayout.Label(keys[i].Replace("*", ",").Replace(@"\n", "\n"), GUILayout.Width(108), GUILayout.Height(15));
                        else GUILayout.Label(keys[i].Replace("*", ",").Replace(@"\n", "\n"), GUILayout.Width(100), GUILayout.Height(15));
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndHorizontal();

                    for (int i = 1; i < lines.Length - 1; i++)
                    {
                        DrawLine(lines[i], langYG);
                    }
                }
                else
                {
                    this.Close();
                }
            }

            if (countSelectObj < 1)
            {
                this.Close();
            }

            GUILayout.EndScrollView();
        }

        void DrawLine(string line, Language lang)
        {
            GUILayout.BeginHorizontal();

            string[] keys = Regex.Split(line, ",");

            for (int i = 0; i < keys.Length; i++)
            {
                if (i == 0)
                {

                    if (GUILayout.Button(keys[i].Replace("*", ",").Replace(@"\n", "\n"), GUILayout.Width(120), GUILayout.Height(26)))
                    {
                        lang.text = keys[i];
                    }
                }
                else
                {
                    GUILayout.BeginVertical("HelpBox");
                    GUILayout.Label(keys[i].Replace("*", ",").Replace(@"\n", "\n"), GUILayout.Width(100), GUILayout.Height(20));
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
