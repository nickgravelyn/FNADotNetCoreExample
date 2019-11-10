This repo is an example of building an FNA game using .NET Core 3, including
steps for code signing and notarization of the app. While this is using FNA the
general approach should work fine for any .NET Core 3 app.

The "game" is just a CornflowerBlue window. Everything interesting is in the
csproj file.

# Building and running

I'm sure Visual Studio works fine but I'm using a Mac so I'm in the Terminal:

```sh
dotnet run -p FNADotNetCoreExample
```

That'll take care of building and copying the correct fnalibs into the output
directory so that you can run the game.

# Publishing

Generally speaking you just use `dotnet publish` to publish the various
platforms. Something like this:

```sh
dotnet publish FNADotNetCoreExample -c Release -r linux-x64 -o path/to/publish/linux-x64
dotnet publish FNADotNetCoreExample -c Release -r osx-x64 -o path/to/publish/osx-x64
dotnet publish FNADotNetCoreExample -c Release -r win-x64 -o path/to/publish/win-x64
dotnet publish FNADotNetCoreExample -c Release -r win-x86 -o path/to/publish/win-x86
```

You should publish each platform to separate directories.

Of sad note it [doesn't look](https://github.com/dotnet/coreclr/issues/9265)
like prebuilt binaries exist for x86 Linux otherwise you'd have `linux-x86` in
there, too.

## macOS

### Code signing

The project supports code signing for you if you are publishing on a Mac and
provide a `MacOSCodeSignIdentity` property. You can pass it on the command line:

```sh
dotnet publish FNADotNetCoreExample \
  -c Release \
  -r osx-x64 \
  -o path/to/publish/osx-x64 \
  "/p:MacOSCodeSignIdentity=Your Certificate Name"
```

or add it to a `PropertyGroup` in your project file:

```xml
<PropertyGroup>
  <MacOSCodeSignIdentity>Your Certificate Name</MacOSCodeSignIdentity>
</PropertyGroup>
```

If you publish for macOS from Windows or Linux, or simply want to sign the code
yourself, you can always take the resulting app bundle and sign it on macOS
using this command:

```sh
codesign \
  -s "Your Certificate Name" \
  --entitlements FNADotNetCoreExample/macOS/Publish.entitlements \
  --force \
  --deep \
  --verbose \
  --options runtime \
  path/to/publish/osx-x64/FNADotNetCoreExample.app
```

This signs the app using the hardened runtime, and the entitlements tell the
system that we're going to be using a JIT compiler.

### Notarization

Once you've signed the app using the project or the `codesign` tool you can
submit for notarization. First you need to ZIP up the app bundle:

```sh
zip -r \
  path/to/publish/osx-x64/FNADotNetCoreExample.zip \
  path/to/publish/osx-x64/FNADotNetCoreExample.app
```

Then you can use existing Xcode tools to [submit for notarization](https://developer.apple.com/documentation/xcode/notarizing_your_app_before_distribution/customizing_the_notarization_workflow?language=objc#3087734):

```sh
xcrun altool \
  --notarize-app \
  --primary-bundle-id com.example.fnadotnetcoreexample \
  --username <username> \
  --password <password> \
  --file path/to/publish/osx-x64/FNADotNetCoreExample.zip
```

That will print out a RequestUUID. You can use that to query for notarization
status:

```sh
xcrun altool \
  --notarization-info <RequestUUID> \
  --username <username> \
  --password <password>
```

When the status for the request is complete you can then staple the results to
the app file:

```sh
xcrun stapler staple path/to/publish/osx-x64/FNADotNetCoreExample.app
```

# Licenses

FNA and it's components are under their own licenses which can be found in
FNA/licenses.

All code for the FNADotNetCoreExample project is available under the MIT
License.
