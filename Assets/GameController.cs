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
    [SerializeField] Cinematic levelOutro;

    bool registeringPlayers = true;
    IDictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();
    Level currentLevel;
    int nextLevelIndex;
    bool canTransition = true;

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

        if (canTransition && players.Count > 0 && Input.GetButtonDown("Next"))
            DoLevelTransition();
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

    void DoLevelTransition()
    {
        canTransition = false;
        levelOutro.PlayThen(delegate
            {
                BeginNextLevel();
            });
    }

    void BeginNextLevel()
    {
        
        RestorePlayers();
        levelIntro.PlayThen(() =>
            {
                BeginLevel(nextLevelIndex);
                nextLevelIndex++;
                canTransition = true;
            });
    }

    void RestorePlayers()
    {
        foreach (var player in players)
            player.Value.Restore();
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
        currentLevel.OnVictory += DoLevelTransition;
        registeringPlayers = false;
    }

    #if UNITY_EDITOR
    public void UpLevel(int index)
    {
        var temp = levels[index - 1];
        levels[index - 1] = levels[index];
        levels[index] = temp;
    }
    #endif
}
