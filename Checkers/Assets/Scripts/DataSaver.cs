using Unity;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataSaver
{

    public static void Save<T>(string name, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(name, jsonData);
        PlayerPrefs.Save();
    }

    public static void SaveList<T>(string name, List<T> data)
    {
        var serializableList = new SerializableListContainer<T>(data);
        string jsonData = JsonUtility.ToJson(serializableList);
        PlayerPrefs.SetString(name, jsonData);
        PlayerPrefs.Save();
    }

    public static void SaveNewGame(GameStats newGame)
    {
        var currentMatches = LoadList<GameStats>("matches");
        if (currentMatches == null)
        {
            currentMatches = new List<GameStats>();
        }

        currentMatches.Add(newGame);
        SaveList<GameStats>("matches", currentMatches);
    }

    public static void UpdatePlayer(Player player)
    {
        var currentPlayers = LoadList<Player>("possiblePlayers");

        var playersWithoutUpdated = currentPlayers.Where(p => p.name != player.name).ToList();
        playersWithoutUpdated.Add(player);

        SaveList<Player>("possiblePlayers", playersWithoutUpdated);
    }

    public static Player LoadPlayerWithName(string name)
    {
        var currentPlayers = LoadList<Player>("possiblePlayers");

        return currentPlayers.Find(p => p.name == name);
    }

    public static T Load<T>(string name)
    {
        string jsonData = PlayerPrefs.GetString(name);
        if (jsonData.Length == 0) return default(T);
        return JsonUtility.FromJson<T>(jsonData);
    }

    public static List<T> LoadList<T>(string name)
    {
        string jsonData = PlayerPrefs.GetString(name);
        if (jsonData.Length == 0)
        {
            return new List<T>();
        }
        var loadedData = JsonUtility.FromJson<SerializableListContainer<T>>(jsonData);
        return loadedData.list;
    }
}