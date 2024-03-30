using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class GameMaster: MonoBehaviour
    {
        [SerializeField, NamedArray("EventName")] private GameEvent[] events;

        public void StartGame()
        {
            foreach (var @event in events)
            {
                RegisterEvent(@event);
            }
        }
        
        private void RegisterEvent(GameEvent gameEvent)
        {
            StartCoroutine(DelayInvoke(gameEvent));
        }

        private IEnumerator DelayInvoke(GameEvent gameEvent)
        {
            yield return new WaitForSeconds(gameEvent.TimeInMinutes * 60);
            gameEvent.Event.Invoke();
        }
    }
    
    [Serializable]
    public class GameEvent
    {
        public string EventName;
        public float TimeInMinutes;
        public UnityEvent Event;
    }
}