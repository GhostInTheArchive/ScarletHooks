# ScarletHooks

ScarletHooks is a V Rising server mod that enables advanced webhook integration for all chat and login/logout messages. It allows admins to configure multiple webhooks for admin, public, clan-specific, and dedicated login notifications, with fine-grained control over which message types are sent to each destination. Message formats and prefixes are fully customizable via the config file, supporting placeholders like `{playerName}`, `{clanName}`, and `{targetName}`. All features can be managed through in-game chat commands and configuration, including sending login/logout messages to dedicated webhooks.

**Notes:**
* This mod only supports sending messages from the server to external webhooks. It does not support sending messages to the in-game chat from external sources, if you need that feature, please check out [ScarletRCON](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletRCON/).
* While the mod isn’t restricted to Discord, using other webhook services may require modifying the source code.
   - [See the FAQ for more information](https://thunderstore.io/c/v-rising/p/ScarletMods/ScarletHooks/wiki/3487-faq/).

---

## Added support for RCON Commands from ScarletRCON

## Support & Donations

<a href='https://ko-fi.com/F2F21EWEM7' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi6.png?v=6' alt='Buy Me a Coffee at ko-fi.com' /></a>  <a href='https://ko-fi.com/F2F21EWEM7' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi6.png?v=6' alt='Buy Me a Coffee at ko-fi.com' /></a>

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

This mod requires the following dependencies:

* **[BepInEx](https://wiki.vrisingmods.com/user/bepinex_install.html)**
* **[VampireCommandFramework](https://github.com/decaprime/VampireCommandFramework/releases/tag/v0.10.0)**

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

3. Ensure **VampireCommandFramework** is also installed in the `plugins` folder.
4. Start or restart your server.

## Join the [modding community on Discord](https://discord.com/invite/QG2FmueAG9).