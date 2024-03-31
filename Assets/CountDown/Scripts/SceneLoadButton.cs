using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CountDown
{
    [RequireComponent(typeof(Button))]
    public class SceneLoadButton: MonoBehaviour
    {
        private Button button;
        [SerializeField] private SceneName sceneName;
        
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
            SceneManager.LoadScene((int) sceneName);
        }
    }
}