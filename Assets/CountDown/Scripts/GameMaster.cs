using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class GameMaster: MonoBehaviour
    {
        [SerializeField] private EventTime[] events;

        public void StartGame()
        {
            foreach (var @event in events)
            {
                RegisterEvent(@event);
            }
        }
        
        private void RegisterEvent(EventTime @event)
        {
            StartCoroutine(DelayInvoke(@event));
        }

        private IEnumerator DelayInvoke(EventTime @event)
        {
            yield return new WaitForSeconds(@event.timeInMinutes * 60);
            @event.Event.Invoke();
        }
    }
    
    [Serializable]
    public class EventTime
    {
        public UnityEvent Event;
        public float timeInMinutes;
    }
}