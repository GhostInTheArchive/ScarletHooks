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