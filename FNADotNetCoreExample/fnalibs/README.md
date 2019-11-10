NOTE FROM NICK:

These were downloading on November 10, 2019 from
https://fna-xna.github.io/download and according to the site the native
libraries were last updated November 1, 2019.

I then ran two scripts across the files to clean them up for use in the project:

1. Remove the quarantine bit Safari set which prevents the code from executing:

```sh
cd osx
xattr -d com.apple.quarantine *
```

2. Fix up some paths used by SDL2_image and FAudio to look in the
   `@executable_path` for SDL2:

```sh
cd osx

install_name_tool -change \
  "/usr/local/lib/libSDL2-2.0.0.dylib" \
  "@executable_path/libSDL2-2.0.0.dylib" \
  libSDL2_image-2.0.0.dylib

install_name_tool -change \
  "/usr/local/lib/libSDL2-2.0.0.dylib" \
  "@executable_path/libSDL2-2.0.0.dylib" \
  libFAudio.0.dylib
```

ORIGINAL README:

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
