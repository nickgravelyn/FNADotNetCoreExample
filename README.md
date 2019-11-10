This repo is an example of building an FNA game using .NET Core 3, including a
full publishing setup that also handles code signing and notarization on macOS.

The actual "game" is just a CornflowerBlue. Everything interesting is in the
csproj file.

# Building and running

I'm sure Visual Studio works fine but I'm using a Mac so I'm in the Terminal:

`dotnet run -p FNADotNetCoreExample`

That'll take care of building and copying the correct fnalibs into the output
directory so that you can run the game.

# Publishing

TODO

# Licenses

FNA and it's components are under their own licenses which can be found in
FNA/licenses.

All code for the FNADotNetCoreExample project is available under the MIT
License.
