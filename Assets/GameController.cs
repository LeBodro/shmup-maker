using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] ShipSelector ships;
    [SerializeField] HUD hud;
    [SerializeField] Theatre theatre;

    Action executeState;
    IDictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();
    Level currentLevel;

    void Start()
    {
        executeState = PlayerRegistrationState;
    }

    void Update()
    {
        executeState();
    }

    void PlayerRegistrationState()
    {
        for (int i = 0; i < 5; i++)
        {
            if (!players.ContainsKey(i))
            {
                if (Input.GetButtonDown(string.Format("P{0}Action1", i)))
                    AddPlayer(i);
            }
            else if (Input.GetButtonDown(string.Format("P{0}Cycle", i)))
            {
                SwitchShipForPlayer(i);
            }
        }

        if (players.Count > 0 && Input.GetButtonDown("Next"))
            BeginLevel();
    }

    void SwitchShipForPlayer(int id)
    {
        Destroy(players[id].gameObject);
        players.Remove(id);
        AddPlayer(id);
    }

    void AddPlayer(int id)
    {
        var ship = ships.GetInstanceForId(id);
        var controller = ship.gameObject.AddComponent<PlayerController>();
        controller.ControllerId = id;
        players.Add(id, controller);
        CrackleAudio.SoundController.PlaySound("Reactor");
        
        hud.RegisterPlayer(controller);
    }

    void BeginLevel()
    {
        currentLevel = FindObjectOfType<Level>();
        if (currentLevel == null)
        {
            Debug.LogError("NO LEVEL FOUND!");
            return;
        }

        currentLevel.Begin(theatre);
        executeState = delegate
        {
        };
    }
}
