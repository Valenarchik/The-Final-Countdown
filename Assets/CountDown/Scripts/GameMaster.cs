using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class GameMaster: MonoBehaviour
    {
        [SerializeField, NamedArray("EventName")] private GameEvent[] events;

        private List<GameEvent> gameEvents;
        
        public void StartGame()
        {
            gameEvents = new List<GameEvent>(events);
            CheckTime();
        }

        private void FixedUpdate()
        {
            UpdateTime();
            CheckTime();
        }

        private void UpdateTime()
        {
            foreach (var gameEvent in gameEvents)
                gameEvent.TimeInMinutes -= Time.deltaTime / 60;
        }

        private void CheckTime()
        {
            foreach (var gameEvent in gameEvents.ToArray())
            {
                if (gameEvent.TimeInMinutes <= 0)
                {
                    gameEvent.Event?.Invoke();
                    gameEvents.Remove(gameEvent);
                }
            }
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