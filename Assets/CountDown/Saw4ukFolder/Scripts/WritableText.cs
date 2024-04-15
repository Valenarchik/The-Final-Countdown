using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown.Saw4ukFolder.Scripts
{
    public class WritableText : MonoBehaviour
    {
        [SerializeField] private TMP_Text textField;
        [SerializeField] private float timeBetweenLettersInSeconds;

        public UnityEvent textWritten;

        private void Start()
        {
            WriteText(textField.text);
        }

        public void WriteText(string text)
        {
            StopAllCoroutines();
            StartCoroutine(WritingTextCoroutine(text));
        }

        private IEnumerator WritingTextCoroutine(string text)
        {
            var builder = new StringBuilder();
            foreach (var chr in text)
            {
                builder.Append(chr);
                textField.text = builder.ToString();
                yield return new WaitForSeconds(timeBetweenLettersInSeconds);
            }
            textWritten.Invoke();
        }
    }
}