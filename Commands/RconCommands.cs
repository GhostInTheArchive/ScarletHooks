using System.Collections.Generic;
using ScarletHooks.Data;
using ScarletHooks.Services;
using ScarletHooks.Systems;
using ScarletRCON.Shared;

namespace ScarletHooks.Commands;

[RconCommandCategory("ScarletHooks")]
public class AdminRconCommands {
  [RconCommand("add", "Add a new clan to the list. The clan name is case-sensitive, if you are unsure use 'scarlethooks.afp' add from the player's name.")]
  public static string Add(string clanName) {
    if (string.IsNullOrEmpty(clanName)) {
      return "You must provide a clan name.";
    }

    if (MessageDispatchSystem.ClanWebHookUrls.ContainsKey(clanName)) {
      return "This clan already exists, please use the 'scarlethooks.setclanwebhook' command to change the webhook url.";
    }

    MessageDispatchSystem.AddClan(clanName);
    return $"Clan {clanName} added to the list of clans, please use the 'scarlethooks.setclanwebhook' command to set the webhook url.";
  }

  [RconCommand("afp", "Add a clan to the list using a player's name.")]
  public static string AddFromPlayer(string playerName) {
    if (!PlayerService.TryGetByName(playerName, out PlayerData playerData) || string.IsNullOrEmpty(playerData.ClanName)) {
      return $"Player '{playerName}' not found or does not belong to a clan.";
    }

    string clanName = playerData.ClanName;

    if (MessageDispatchSystem.ClanWebHookUrls.ContainsKey(clanName)) {
      return "This clan already exists, please use the 'scarlethooks.setclanwebhook' command to change the webhook url.";
    }

    MessageDispatchSystem.AddClan(clanName);
    return $"Clan {clanName} (from player '{playerName}') added to the list of clans, please use the 'scarlethooks.setclanwebhook' command to set the webhook url.";
  }

  [RconCommand("remove", "Remove a clan from the list by clan or player name.")]
  public static string Remove(string name) {
    string clanName = null;

    if (PlayerService.TryGetByName(name, out PlayerData playerData)) {
      clanName = playerData.ClanName;
    }

    if (string.IsNullOrEmpty(clanName)) {
      clanName = name;
    }

    if (string.IsNullOrEmpty(clanName)) return "";

    if (!MessageDispatchSystem.ClanWebHookUrls.ContainsKey(clanName)) {
      return $"No clan named {clanName} is loaded.";
    }

    MessageDispatchSystem.RemoveClan(clanName);
    return $"Clan {clanName} removed from the list of clans.";
  }

  [RconCommand("reload settings", "Reload the settings from the configuration file.")]
  public static string ReloadSettings() {
    Settings.Reload();
    return "Settings reloaded.";
  }

  [RconCommand("reload webhooks", "Reload the webhooks from the configuration file.")]
  public static string ReloadWebhooks() {
    MessageDispatchSystem.LoadFromFile();
    return "Webhooks reloaded.";
  }

  [RconCommand("reload", "Reload both settings and webhooks from the configuration file.")]
  public static string Reload() {
    Settings.Reload();
    MessageDispatchSystem.LoadFromFile();
    return "Settings and webhooks reloaded.";
  }

  [RconCommand("list", "List all configured webhook URLs.")]
  public static string ListClanWebHookUrls() {
    var response = new System.Text.StringBuilder();
    response.AppendLine($"Admin: {Settings.Get<string>("AdminWebhookURL")}.");
    response.AppendLine($"Public: {Settings.Get<string>("PublicWebhookURL")}.");
    response.AppendLine("List of clans webhooks:");

    foreach (var (clanName, url) in MessageDispatchSystem.ClanWebHookUrls) {
      response.AppendLine($"{clanName}: {url}.");
    }

    response.AppendLine($"Total webhook urls: {MessageDispatchSystem.ClanWebHookUrls.Count + 2}.");
    return response.ToString();
  }

  [RconCommand("settings", "Change a boolean setting.")]
  public static string ChangeSettings(string settings, bool value) {
    if (!Settings.Has(settings)) {
      return $"{settings} does not exist.";
    }

    List<string> exludedSettings = ["AdminWebhookURL", "PublicWebhookURL", "MessageInterval", "OnFailInterval"];

    if (exludedSettings.Contains(settings)) {
      return $"{settings} cannot be changed via command.";
    }

    Settings.Set(settings, value);
    return $"{settings} changed to {value}.";
  }

  [RconCommand("settings", "Show all current settings.")]
  public static string ShowSettings() {
    var response = "Current settings:";
    response += $"AdminWebhookURL: {Settings.Get<string>("AdminWebhookURL")}\n";
    response += $"PublicWebhookURL: {Settings.Get<string>("PublicWebhookURL")}\n";
    response += $"MessageInterval: {Settings.Get<float>("MessageInterval")}\n";
    response += $"OnFailInterval: {Settings.Get<float>("OnFailInterval")}\n";
    response += $"EnableBatching: {Settings.Get<bool>("EnableBatching")}\n";
    response += $"AdminGlobalMessages: {Settings.Get<bool>("AdminGlobalMessages")}\n";
    response += $"AdminClanMessages: {Settings.Get<bool>("AdminClanMessages")}\n";
    response += $"AdminLocalMessages: {Settings.Get<bool>("AdminLocalMessages")}\n";
    response += $"AdminWhisperMessages: {Settings.Get<bool>("AdminWhisperMessages")}\n";
    response += $"PublicGlobalMessages: {Settings.Get<bool>("PublicGlobalMessages")}\n";
    response += $"PublicClanMessages: {Settings.Get<bool>("PublicClanMessages")}\n";
    response += $"PublicLocalMessages: {Settings.Get<bool>("PublicLocalMessages")}\n";
    response += $"PublicWhisperMessages: {Settings.Get<bool>("PublicWhisperMessages")}\n";

    return response;
  }

  [RconCommand("start", "Start the message dispatch system.")]
  public static string Start() {
    MessageDispatchSystem.Initialize();
    return "Message dispatch system started.";
  }

  [RconCommand("stop", "Stop the message dispatch system.")]
  public static string Stop() {
    MessageDispatchSystem.Shutdown();
    return "Message dispatch system stopped.";
  }

  [RconCommand("forcestop", "Force stop the message dispatch system and clear all cache.")]
  public static string ForceStop() {
    MessageDispatchSystem.ForceShutdown();
    return "Message dispatch system stopped and cleared all cache.";
  }

  [RconCommand("setadminwebhook", "Set the admin webhook URL.")]
  public static string SetAdminWebhook(string url) {
    if (string.IsNullOrEmpty(url)) {
      return "You must provide a valid URL.";
    }

    Settings.Set("AdminWebhookURL", url);
    return $"Admin webhook URL set to: {url}";
  }

  [RconCommand("setpublicwebhook", "Set the public webhook URL.")]
  public static string SetPublicWebhook(string url) {
    if (string.IsNullOrEmpty(url)) {
      return "You must provide a valid URL.";
    }

    Settings.Set("PublicWebhookURL", url);
    return $"Public webhook URL set to: {url}";
  }

  [RconCommand("setloginwebhook", "Set the login webhook URL.")]
  public static string SetLoginWebhook(string url) {
    if (string.IsNullOrEmpty(url)) {
      return "You must provide a valid URL.";
    }

    Settings.Set("LoginWebhookURL", url);
    return $"Login webhook URL set to: {url}";
  }

  [RconCommand("setclanwebhook", "Set the webhook URL for a specific clan.")]
  public static string SetClanWebhook(string clanName, string url) {
    if (string.IsNullOrEmpty(clanName) || string.IsNullOrEmpty(url)) {
      return "You must provide a clan name and a valid URL.";
    }

    if (!MessageDispatchSystem.ClanWebHookUrls.ContainsKey(clanName)) {
      return $"Clan {clanName} does not exist. Use the 'add' command to add it first.";
    }

    MessageDispatchSystem.ClanWebHookUrls[clanName] = url;
    return $"Webhook URL for clan {clanName} set to: {url}";
  }
}