using UnityEngine;
using UnityEngine.UI;

namespace CountDown
{
    [RequireComponent(typeof(Button))]
    [DisallowMultipleComponent]
    public class ButtonPlaySFX : PlaySFX
    {
        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(Play);
        }
    }
}
