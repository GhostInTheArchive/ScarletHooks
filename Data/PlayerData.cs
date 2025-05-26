using Unity.Entities;
using ProjectM.Network;
using ProjectM;

namespace ScarletHooks.Data;

public class PlayerData {
  public Entity UserEntity;
  public User User => UserEntity.Read<User>();
  public string Name {
    get {
      if (string.IsNullOrEmpty(_name)) {
        _name = User.CharacterName.ToString();
      }

      return _name;
    }
  }
  private string _name = null;
  public void SetName(string name) {
    _name = name;
  }
  public Entity CharacterEntity => User.LocalCharacter._Entity;
  public ulong PlatformId => User.PlatformId;
  public bool IsOnline => User.IsConnected;
  public bool IsAdmin => User.IsAdmin;
  public NetworkId NetworkId { get; set; }
  public string ClanName {
    get {
      var clanEntity = UserEntity.Read<User>().ClanEntity._Entity;

      if (clanEntity.Equals(Entity.Null)) return null;

      var clanTeam = clanEntity.Read<ClanTeam>();

      if (clanTeam.Equals(default)) return null;

      return clanTeam.Name.ToString();
    }
  }
}
