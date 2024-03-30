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

    private void Awake()
    {
        player.DropItemEvent.AddListener(OnDropItem);
        player.PickUpItemEvent.AddListener(OnPickUpItem);
        player.PlacedToRocketEvent.AddListener(OnPlacedToRocket);
    }

    private void OnPlacedToRocket(Item arg0)
    {
        helpPanel.ReDraw(HelpPanelState.Hided);
    }

    private void OnPickUpItem(Item arg0)
    {
        helpPanel.ReDraw(HelpPanelState.DemonstratingDrop);
    }

    private void OnDropItem(Item arg0)
    {
        helpPanel.ReDraw(HelpPanelState.DemonstaratingItem);
    }
    

    private void FixedUpdate()
    {
        //Debug.Log(player.IntersectingObjects.Count);
        for (int i = 0; i < 3; i++)
        {
            var x = GetComponent<Player>();
        }
        if (player.IntersectingObjects.Count == 0)
        {
            if (player.CanDropItem && helpPanel.CurrentState != HelpPanelState.DemonstratingDrop)
            {
                helpPanel.ReDraw(HelpPanelState.DemonstratingDrop);
            }
            else if (helpPanel.CurrentState != HelpPanelState.Hided && !player.CanDropItem)
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
                if (rocket != null && helpPanel.CurrentState != HelpPanelState.DemonstratingRocket && player.CanDropItem)
                {
                    helpPanel.ReDraw(HelpPanelState.DemonstratingRocket);
                    return;
                }
            }
            else if (player.IntersectingObjects.Count > 2)
            {
                var orderedList = player.IntersectingObjects.OrderBy(x =>
                    Vector3.Distance(x.gameObject.transform.position, gameObject.transform.position)).ToList();
                foreach (var obj in orderedList)
                {
                    var item = obj.GetComponent<Item>();
                    if (item != null)
                    {
                        helpPanel.ReDraw(HelpPanelState.DemonstaratingItem);
                        return;
                    }
                    var rocket = obj.GetComponent<Rocket>();
                    if (rocket != null && player.CanDropItem)
                    {
                        helpPanel.ReDraw(HelpPanelState.DemonstratingRocket);
                        return;
                    }
                }
            }
        }
    }
}
