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
        private float maxTime;
        private bool paused;
        
        private float timeScale = 1;
        private float timeInMinutes;
        
        public void StartGame()
        {
            gameEvents = new List<GameEvent>(events);
            maxTime = gameEvents.Max(x => x.TimeInMinutes);
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
                .Where(x => x.timePercent * maxTime < timeInMinutes)
                .OrderByDescending(x => x.timePercent * maxTime)
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
            foreach (var gameEvent in gameEvents)
            {
                gameEvent.TimeInMinutes -= deltaTime;
            }
            
            timeInMinutes += deltaTime;
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

    [Serializable]
    public class TimeMultiplier
    {
        [Range(0, 1)] public float timePercent;
        [Range(0, 1)] public float timeScale;
    }
}