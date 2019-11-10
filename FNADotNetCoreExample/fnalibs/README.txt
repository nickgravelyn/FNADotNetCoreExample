NOTE FROM NICK:

I have modified the Linux and macOS file names to remove `-2.0` and `-2.0.0`
such that they all have the same base name as Windows. This is required as .NET
Core does not respect the dllmap functionality of Mono and other alternatives
require a bunch of messy code to handle loading. Simplest solution is to just
match file names.

These were downloading from https://fna-xna.github.io/download/ with a last
updated date of November 1, 2019.


ORIGINAL README BELOW:

--

This is fnalibs, an archive containing the native libraries used by FNA.

These are the folders included:

- x86: 32-bit Windows
- x64: 64-bit Windows
- lib: 32-bit Linux
- lib64: 64-bit Linux
- osx: macOS 64-bit

The library dependency list is as follows:

- SDL2, used as the platform layer
- MojoShader, used in the Graphics namespace
- FAudio, used in the Audio/Media namespaces
- SDL2_image, only used for Texture2D.FromStream and Texture2D.SaveAsPng/Jpeg
- libtheorafile, only used for VideoPlayer
