# ScarletHooks

ScarletHooks is a V Rising server mod that enables advanced webhook integration for chat messages, login/logout events, PvP kills, and VBlood boss defeats. It allows admins to configure multiple webhooks for admin, public, clan-specific, and dedicated notifications, with fine-grained control over which message types are sent to each destination. Message formats and prefixes are fully customizable via the config file, supporting placeholders like `{playerName}`, `{clanName}`, and `{targetName}`. All features can be managed through in-game chat commands and configuration, including sending various event notifications to dedicated webhooks.

**Notes:**
* This mod only supports sending messages from the server to external webhooks. It does not support sending messages to the in-game chat from external sources, if you need that feature, please check out [ScarletRCON](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletRCON/).
* While the mod isn’t restricted to Discord, using other webhook services may require modifying the source code.
   - [See the FAQ for more information](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3487-faq/).

---

## Added new hooks
- **PvP Kills**: Track player kills in PvP combat.
- **VBlood Kills**: Track VBlood boss eliminations.

## Support & Donations

<a href="https://www.patreon.com/bePatron?u=30093731" data-patreon-widget-type="become-patron-button"><img height='36' style='border:0px;height:36px;' src='https://i.imgur.com/o12xEqi.png' alt='Become a Patron' /></a>  <a href='https://ko-fi.com/F2F21EWEM7' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi6.png?v=6' alt='Buy Me a Coffee at ko-fi.com' /></a>

---

### For more information, please visit the [ScarletHooks Wiki on Thunderstore](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/).

**Wiki Index:**
- [Clan Webhooks](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3486-clan-webhooks/)
- [Commands](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3484-commands/)
- [Configuration Guide](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3480-configuration-guide/)
- [FAQ](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3487-faq/)
- [Features Overview](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3477-features-overview/)
- [Installation Guide](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3479-installation-guide/)
- [Placeholders & Message Formatting](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3483-placeholders-message-formatting/)
- [Troubleshooting](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3482-troubleshooting/)
- [Webhook Routing & Examples](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3485-webhook-routing-examples/)

---

## Installation

### Requirements

This mod requires the following dependencies to function correctly:

* **[BepInEx (RC2)](https://wiki.vrisingmods.com/user/bepinex_install.html)**
* **[ScarletCore](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletCore/)**
* **[VampireCommandFramework](https://thunderstore.io/c/v-rising/p/deca/VampireCommandFramework/)**

Make sure both are installed and loaded **before** installing ScarletHooks.

### Manual Installation

1. Download the latest release of **ScarletHooks**.
2. Extract the contents into your `BepInEx/plugins` folder:

   ```
   <V Rising Server Directory>/BepInEx/plugins/
   ```

   Your folder should now include:

   ```
   BepInEx/plugins/ScarletHooks.dll
   ```

3. Ensure **ScarletCore** and **VampireCommandFramework** are also installed in the `plugins` folder.
4. Start or restart your server.

## Join the [V Rising Mod Community on Discord](https://vrisingmods.com/discord)
