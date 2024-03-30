using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CountDown;
using UnityEngine;

public class HelpHandler : MonoBehaviour
{
    [SerializeField] private HelpPanel helpPanel;
    [SerializeField] private Player player;

    private void FixedUpdate()
    {
        //Debug.Log(player.IntersectingObjects.Count);
        
        if (player.IntersectingObjects.Count == 0)
        {
            if (player.CanDropItem && helpPanel.CurrentState != HelpPanelState.DemonstratingDrop)
            {
                helpPanel.ReDraw(HelpPanelState.DemonstratingDrop);
            }
            else if (helpPanel.CurrentState != HelpPanelState.Hided)
            {
                helpPanel.ReDraw(HelpPanelState.Hided);
            }
        }
        else
        {
            if (player.IntersectingObjects.Count == 1)
            {
                var obj = player.IntersectingObjects.First();
                var item = obj.GetComponent<Item>();
                if (item != null && helpPanel.CurrentState != HelpPanelState.DemonstaratingItem) 
                {
                    helpPanel.ReDraw(HelpPanelState.DemonstaratingItem);
                    return;
                }
                var rocket = obj.GetComponent<Rocket>();
                if (rocket != null && helpPanel.CurrentState != HelpPanelState.DemonstratingRocket)
                {
                    helpPanel.ReDraw(HelpPanelState.DemonstratingRocket);
                    return;
                }
            }
        }
    }
}
