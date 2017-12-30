using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] ShipSelector ships;
    [SerializeField] HUD hud;
    [SerializeField] Theatre theatre;
    [SerializeField] Level[] levels;
    [SerializeField] Cinematic levelIntro;

    bool registeringPlayers = true;
    IDictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();
    Level currentLevel;
    int nextLevelIndex;

    void Update()
    {
        if (registeringPlayers)
            CheckForPlayerRegistration();
    }

    void CheckForPlayerRegistration()
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
            BeginNextLevel();
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

    void BeginNextLevel()
    {
        RestorePlayers();
        levelIntro.PlayThen(() =>
            {
                BeginLevel(nextLevelIndex);
                nextLevelIndex++;
            });
    }

    void BeginLevel(int id)
    {
        if (id >= levels.Length || levels[id] == null)
        {
            Debug.LogError("NO LEVEL FOUND!");
            return;
        }

        currentLevel = levels[id];
        currentLevel.Begin(theatre);
        currentLevel.OnVictory += BeginNextLevel;
        registeringPlayers = false;
    }

    void RestorePlayers()
    {
        foreach (var player in players)
            player.Value.Restore();
    }
}
