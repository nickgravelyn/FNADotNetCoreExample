This repo is an example of building an FNA game using .NET Core 3, including a
full publishing setup that also handles code signing and notarization on macOS.

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
dotnet publish FNADotNetCoreExample -c Release -r linux-x64 -o ~/Desktop/FNADotNetCoreExample/linux-x64
dotnet publish FNADotNetCoreExample -c Release -r osx-x64 -o ~/Desktop/FNADotNetCoreExample/macOS "/p:MacOSCodeSignIdentity=Your Certificate Name"
dotnet publish FNADotNetCoreExample -c Release -r win-x64 -o ~/Desktop/FNADotNetCoreExample/win-x64
dotnet publish FNADotNetCoreExample -c Release -r win-x86 -o ~/Desktop/FNADotNetCoreExample/win-x86
```

Of sad note it [doesn't look](https://github.com/dotnet/coreclr/issues/9265)
like prebuilt binaries exist for x86 Linux otherwise you'd have `linux-x86` in
there, too.

## macOS

The publishing process on macOS requires an additional `MacOSCodeSignIdentity`
property to facilitate code signing, shown above as being passed along on the
commandline, but you could also add this to a `PropertyGroup` in the csproj if
you wanted to not have to type it in all the time.

```xml
<PropertyGroup>
  <MacOSCodeSignIdentity>Your Certificate Name</MacOSCodeSignIdentity>
</PropertyGroup>
```

When publishing for macOS a nice app bundle is produced, code signed,
and then zipped up.

Afterwards, you can use existing Xcode tools to [submit for notarization](https://developer.apple.com/documentation/xcode/notarizing_your_app_before_distribution/customizing_the_notarization_workflow?language=objc#3087734):

```sh
xcrun altool \
  --notarize-app \
  --primary-bundle-id com.example.fnadotnetcoreexample \
  --username <username> \
  --password <password> \
  --file ~/Desktop/FNADotNetCoreExample/macOS/FNADotNetCoreExample.zip
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
xcrun stapler staple ~/Desktop/FNADotNetCoreExample/macOS/FNADotNetCoreExample.app
```

This process is separate from the actual publish because it requires a full
upload of the ZIP file to Apple and then the time for notarization can be many
minutes. I'd probably write a script to do the upload, wait, and staple but I'm
it's also not a huge piece of the goal of this example.

# Licenses

FNA and it's components are under their own licenses which can be found in
FNA/licenses.

All code for the FNADotNetCoreExample project is available under the MIT
License.
