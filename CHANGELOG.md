<details>
<summary>Update 1.1.0</summary>

- Added PvP kills hook to track player kills in PvP combat.
- Added VBlood kills hook to track VBlood boss eliminations.
- **Major Refactor**: Migrated most services and systems to use existing ScarletCore infrastructure.
- Improved performance and reduced code duplication by leveraging ScarletCore's established systems.
- Enhanced compatibility and stability through unified core architecture.
- Reduced mod footprint and potential conflicts with other ScarletCore-based mods.
</details>

<details>
<summary>Update 1.0.2</summary>

- Improved the handling of login/logout messages to ensure they are sent only once per connection.
- Added new RCON commands for setting webhook URLs without needing to modify or reload the configuration file. (RCON exclusive due to in-game chat limitations)

</details>

<details>
<summary>Update 1.0.1</summary>

- Removed `Destroy_TravelBuffSystem.OnUpdate` patch, as it was causing lag.

</details>

<details>
<summary>Update 0.1.23</summary>

- Removed some logs that were not needed anymore.
- Added support for dedicated login/logout webhooks and message formatting
- Introduced customizable prefixes and formats for all message types
- Implemented granular control over login/logout message routing
- Updated configuration system to support new options
- Improved in-game command management and documentation

</details>