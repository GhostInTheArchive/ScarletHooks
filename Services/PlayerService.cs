using ScarletHooks.Data;
using System.Collections.Generic;
using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;
using System.Linq;
using ScarletHooks.Systems;
using System;

namespace ScarletHooks.Services;

public static class PlayerService {
  public static readonly Dictionary<string, PlayerData> PlayerNames = [];
  public static readonly Dictionary<ulong, PlayerData> PlayerIds = [];
  public static readonly Dictionary<NetworkId, PlayerData> PlayerNetworkIds = [];
  private static readonly List<PlayerData> UnnamedPlayers = [];
  public static readonly List<PlayerData> AllPlayers = [];

  public static void Initialize() {
    ClearCache();
    EntityQueryBuilder queryBuilder = new(Allocator.Temp);

    queryBuilder.AddAll(ComponentType.ReadOnly<User>());
    queryBuilder.AddAll(ComponentType.ReadOnly<NetworkId>());
    queryBuilder.WithOptions(EntityQueryOptions.IncludeDisabled);

    EntityQuery query = Core.EntityManager.CreateEntityQuery(ref queryBuilder);

    try {
      var userEntities = query.ToEntityArray(Allocator.Temp);

      foreach (var entity in userEntities) {
        SetPlayerCache(entity);
      }
    } catch (System.Exception e) {
      Core.Log.LogError(e);
    } finally {
      query.Dispose();
      queryBuilder.Dispose();
    }
  }

  public static void ClearCache() {
    PlayerNames.Clear();
    PlayerIds.Clear();
  }

  public static void SetPlayerCache(Entity userEntity, bool isOffline = false) {
    var networkId = userEntity.Read<NetworkId>();
    var userData = userEntity.Read<User>();
    var name = userData.CharacterName.ToString();

    if (!PlayerIds.ContainsKey(userData.PlatformId)) {
      PlayerData newData = new();

      if (string.IsNullOrEmpty(name)) {
        UnnamedPlayers.Add(newData);
      } else {
        PlayerNames[name.ToLower()] = newData;
        newData.SetName(name);
      }

      PlayerNetworkIds[networkId] = newData;
      PlayerIds[userData.PlatformId] = newData;
      AllPlayers.Add(newData);
    }

    var playerData = PlayerIds[userData.PlatformId];

    playerData.UserEntity = userEntity;

    if (!string.IsNullOrEmpty(playerData.Name) && playerData.Name != name) {
      PlayerNames.Remove(playerData.Name.ToLower());
      playerData.SetName(name);
      PlayerNames[name.ToLower()] = playerData;
    }

    playerData.NetworkId = networkId;

    PlayerNetworkIds[networkId] = playerData;

    var now = DateTime.UtcNow;

    if (isOffline) {
      bool isRecentDisconnect = playerData.DisconnectedSince != default && (now - playerData.DisconnectedSince).TotalSeconds < 10;
      if (!isRecentDisconnect) {
        MessageDispatchSystem.HandleLogoutMessage(playerData.Name, playerData.ClanName);
      }

      PlayerNetworkIds.Remove(networkId);
      playerData.DisconnectedSince = now;
      playerData.ConnectedSince = default;
    } else {
      bool isRecentConnect = playerData.ConnectedSince != default && (now - playerData.ConnectedSince).TotalSeconds < 10;
      if (!isRecentConnect) {
        MessageDispatchSystem.HandleLoginMessage(playerData.Name, playerData.ClanName);
      }

      playerData.ConnectedSince = now;
      playerData.DisconnectedSince = default;
    }

  }

  public static List<PlayerData> GetAdmins() {
    return [.. AllPlayers.Where(p => p.IsAdmin)];
  }

  public static bool TryGetById(ulong platformId, out PlayerData playerData) {
    return PlayerIds.TryGetValue(platformId, out playerData);
  }

  public static bool TryGetByName(string name, out PlayerData playerData) {
    if (PlayerNames.TryGetValue(name.ToLower(), out playerData)) {
      return true;
    }

    if (UnnamedPlayers.Count == 0) return false;

    playerData = UnnamedPlayers.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

    var exist = playerData != null;

    if (exist) {
      PlayerNames[name.ToLower()] = playerData;
      UnnamedPlayers.Remove(playerData);
    }

    return exist;
  }

  public static bool TryGetByNetworkId(NetworkId networkId, out PlayerData playerData) {
    return PlayerNetworkIds.TryGetValue(networkId, out playerData);
  }
}


