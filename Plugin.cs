using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using VampireCommandFramework;
using ScarletCore.Data;
using ScarletHooks.Systems;
using ScarletRCON.Shared;
using System.Reflection;
using ScarletCore.Events;

namespace ScarletHooks;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("markvaaz.ScarletCore")]
[BepInDependency("markvaaz.ScarletRCON", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("gg.deca.VampireCommandFramework")]
public class Plugin : BasePlugin {
  static Harmony _harmony;
  public static Harmony Harmony => _harmony;
  public static Plugin Instance { get; private set; }
  public static ManualLogSource LogInstance { get; private set; }
  public static Settings Settings { get; private set; }
  public static Database Database { get; private set; }

  public override void Load() {
    Instance = this;
    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

    _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    _harmony.PatchAll(Assembly.GetExecutingAssembly());

    Settings = new Settings(MyPluginInfo.PLUGIN_GUID, Instance);
    Database = new Database(MyPluginInfo.PLUGIN_GUID);

    LoadSettings();

    MessageDispatchSystem.Initialize();
    CommandRegistry.RegisterAll();
    RconCommandRegistrar.RegisterAll();
  }

  public override bool Unload() {
    CommandRegistry.UnregisterAssembly();
    RconCommandRegistrar.UnregisterAssembly();
    EventManager.UnregisterAssembly(Assembly.GetExecutingAssembly());
    _harmony?.UnpatchSelf();
    return true;
  }

  public static void LoadSettings() {
    var generalSection = Settings.Section("General");

    generalSection.Add("AdminWebhookURL", "null", "Admin Webhook URL. All messages configured for admin will be sent to this address.");
    generalSection.Add("PublicWebhookURL", "null", "Public Webhook URL. All messages configured for public will be sent to this address.");
    generalSection.Add("LoginWebhookURL", "null", "Login Webhook URL. Only login messages will be sent to this address.");
    generalSection.Add("PvpKillWebhookURL", "null", "PvP Kill Webhook URL. Only PvP kill messages will be sent to this address.");
    generalSection.Add("VBloodWebhookURL", "null", "VBlood Death Webhook URL. Only VBlood death messages will be sent to this address.");
    generalSection.Add("EnableBatching", true, "Enable or disable batching messages to avoid rate limiting.\nUseful for large servers or when many messages are sent at once.");
    generalSection.Add("MessageInterval", 0.2f, "Interval in seconds between sending messages.\nUseful to prevent rate limiting when sending messages to webhooks.");
    generalSection.Add("OnFailInterval", 2f, "Interval in seconds to wait before retrying after a webhook failure.");

    var customizationSection = Settings.Section("Customization");

    customizationSection.Add("LoginMessageFormat", "{playerName} has joined the game.", "Format for login messages.\nAvailable placeholders: {playerName}, {clanName}");
    customizationSection.Add("LogoutMessageFormat", "{playerName} has left the game.", "Format for logout messages.\nAvailable placeholders: {playerName}, {clanName}");
    customizationSection.Add("GlobalPrefix", "[Global] {playerName}:", "Prefix for global chat messages.\nAvailable placeholders: {playerName}, {clanName}");
    customizationSection.Add("LocalPrefix", "[Local] {playerName}:", "Prefix for local chat messages.\nAvailable placeholders: {playerName}, {clanName}");
    customizationSection.Add("ClanPrefix", "[Clan][{clanName}] {playerName}:", "Prefix for clan chat messages.\nAvailable placeholders: {playerName}, {clanName}");
    customizationSection.Add("WhisperPrefix", "[Whisper to {targetName}] {playerName}:", "Prefix for whisper messages.\nAvailable placeholders: {playerName}, {clanName}, {targetName}");
    customizationSection.Add("VBloodDeathMessageFormat", "{playerName} has killed {VBloodName}.", "Format for VBlood death messages.\nAvailable placeholders: {playerName}, {vBloodName}");
    customizationSection.Add("PvpKillMessageFormat", "[{clanName}] {playerName} has killed [{clanName}] {targetName}.", "Format for PvP kill messages.\nAvailable placeholders: {playerName}, {targetName}, {clanName}");

    var adminSection = Settings.Section("Admin");

    adminSection.Add("AdminGlobalMessages", true, "Enable or disable sending global chat messages to the Admin Webhook.");
    adminSection.Add("AdminLocalMessages", true, "Enable or disable sending local chat messages to the Admin Webhook.");
    adminSection.Add("AdminClanMessages", true, "Enable or disable sending clan chat messages to the Admin Webhook.");
    adminSection.Add("AdminWhisperMessages", true, "Enable or disable sending whisper messages to the Admin Webhook.");
    adminSection.Add("AdminLoginMessages", true, "Enable or disable sending login messages to the Admin Webhook.");
    adminSection.Add("AdminVBloodMessages", true, "Enable or disable sending VBlood death messages to the Admin Webhook.");
    adminSection.Add("AdminPvpMessages", true, "Enable or disable sending PvP kill messages to the Admin Webhook.");

    var publicSection = Settings.Section("Public");

    publicSection.Add("PublicGlobalMessages", true, "Enable or disable sending global chat messages to the Public Webhook.");
    publicSection.Add("PublicLocalMessages", false, "Enable or disable sending local chat messages to the Public Webhook.");
    publicSection.Add("PublicClanMessages", false, "Enable or disable sending clan chat messages to the Public Webhook.");
    publicSection.Add("PublicWhisperMessages", false, "Enable or disable sending whisper messages to the Public Webhook.");
    publicSection.Add("PublicLoginMessages", false, "Enable or disable sending login messages to the Public Webhook.");
    publicSection.Add("PublicVBloodMessages", true, "Enable or disable sending VBlood death messages to the Public Webhook.");
    publicSection.Add("PublicPvpMessages", true, "Enable or disable sending PvP kill messages to the Public Webhook.");

    var clansSection = Settings.Section("Clans");

    clansSection.Add("ClanLoginMessages", true, "Enable or disable sending login messages to clans.");
  }

  public static void ReloadSettings() {
    LoadSettings();
  }
}

