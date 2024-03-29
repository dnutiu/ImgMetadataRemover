﻿# ImgMetaRemover

This a is a simple project made for personal use. 

The goal is to make it easy to remove metadata from images and compress them. 
This is useful to bloggers and people that upload personal images online, as many of these images contain GPS and other personal identifiable data.

### Usage

Place it into path and run with `--help`. If no arguments are provided it will run inside the default directory.

```
@nutiu ➜ publish git(main) .\metadata_remover.exe --help

metadata_remover 1.0.0
Copyright (C) 2022 metadata_remover

  -c, --compress     (Default: true) Compress images after cleaning.

  -d, --dest         (Default: ./cleaned) The destination directory for the cleaned images.

  --help             Display this help screen.

  --version          Display version information.

  source (pos. 0)    (Default: .) The source directory of images.
```

# Developing

### Publishing

To publish the command line app I run:

```
 dotnet publish --framework net5.0 --os linux -p:PublishReadyToRun=false --configuration Release
 
 dotnet publish --framework net5.0  --configuration Release
 
 dotnet publish --framework net5.0 --os osx -p:PublishReadyToRun=false --configuration Release
 ```

### Tests

To run tests use `dotnet test`.

Ensure that `IMAGE_CORE_TESTS` is set to the path to the image core tests path. Ex: `\RiderProjects\ImgMetadataRemover\ImageCore.Tests`.

### Credits

- https://github.com/dlemstra/Magick.NET
