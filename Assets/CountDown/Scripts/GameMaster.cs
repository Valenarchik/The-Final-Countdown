using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class GameMaster: MonoBehaviour
    {
        [SerializeField, NamedArray("EventName")] private GameEvent[] events;
        [SerializeField] private TimeMultiplier[] timeMultipliers;
        
        private List<GameEvent> gameEvents;
        private float maxTimeInMinutes;
        private bool paused;
        
        private float timeScale = 1;
        private float timeInMinutes;

        public float MaxTimeInMinutes => maxTimeInMinutes;
        public float TimeInMinutes => timeInMinutes;

        public void StartGame()
        {
            gameEvents = new List<GameEvent>(events);
            maxTimeInMinutes = gameEvents.Max(x => x.TimeInMinutes);
            CheckTime();
        }

        public void PauseGame(bool value)
        {
            paused = value;
        }

        private void FixedUpdate()
        {
            if (paused)
                return;
            
            UpdateTime();
            CheckTime();
            UpdateScale();
        }

        private void UpdateScale()
        {
            var curMultiplier = timeMultipliers
                .Where(x => x.timePercent * maxTimeInMinutes < timeInMinutes)
                .OrderByDescending(x => x.timePercent * maxTimeInMinutes)
                .FirstOrDefault();
            var prevScale = timeScale;
            timeScale = curMultiplier?.timeScale ?? 1;

            if (Math.Abs(prevScale - timeScale) > 0.001f)
            {
                Debug.Log($"Update scale in {timeInMinutes} scale = {timeScale}");
            }
        }
        
        private void UpdateTime()
        {
            var deltaTime = Time.fixedDeltaTime / 60 * timeScale;
            UpdateTime(deltaTime);
        }
        
        public void UpdateTime(float deltaTimeInMinutes)
        {
            foreach (var gameEvent in gameEvents)
            {
                gameEvent.TimeInMinutes -= deltaTimeInMinutes;
            }
            
            timeInMinutes += deltaTimeInMinutes;
        }

        public void CheckTime()
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

        public void Skip15Seconds()
        {
        }

        public void SkipCurrentPhase()
        {
            Debug.Log("ScipCurrentPhase");
            var nextEvent = gameEvents
                .OrderBy(x => x.TimeInMinutes)
                .FirstOrDefault(x => timeInMinutes < x.TimeInMinutes);
            if (nextEvent != null)
            {
                UpdateTime(nextEvent.TimeInMinutes - TimeInMinutes);
                CheckTime();
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

    [Serializable]
    public class TimeMultiplier
    {
        [Range(0, 1)] public float timePercent;
        [Range(0, 1)] public float timeScale;
    }
}