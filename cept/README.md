# :electric_plug: Cept! - Conduit

Client-side C# Windows application that powers the Cept! Auto Accept client. It monitors the League Client (LCU) process, manages auto-accept state, and handles the local tray experience.

## Development

Simply opening the `Conduit.sln` file in [Visual Studio](https://www.visualstudio.com) should install all dependencies via NuGet and be ready to go. Packaging for release is as simple as choosing the release target and building, since Fody.Costura will automatically include the required .dlls in the resulting exe.

## License

Cept! Conduit is released under the [MIT](../LICENSE) license.
