using System;
using UnityEngine;
using UnityEngine.UI;

namespace CountDown
{
    [RequireComponent(typeof(Button))]
    public class ExitButton: MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Application.Quit();
        }
    }
}