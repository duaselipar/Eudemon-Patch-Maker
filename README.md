=== EUDEMON PATCH MAKER ===

WHAT IT DOES:
- Creates self-extracting (.exe) patches
- Auto-runs "AutoPatch.exe" silently
- Overwrites existing files automatically

HOW TO USE:
1. Create "input" folder
2. Add your files + "version.dat" (e.g., "1001")
3. Run PatchMaker.exe
4. Find your patch in "output" as [version].exe

🛠️ FOR DEVELOPERS (Build Instructions):

To create standalone PatchMaker.exe:
1. Open Command Prompt in project folder
2. Run this command:
```
dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true
```
3. Get your EXE here:
\bin\Release\net8.0\win-x64\publish\PatchMaker.exe

FOLDER STRUCTURE:
PatchMaker/
├── input/          ← Put your files here
│   ├── AutoPatch.exe
│   └── version.dat (must contain version number)
├── output/         ← Output patch appears here
└── PatchMaker.exe

REQUIREMENTS:
- Windows 10/11 (64-bit)
- For non-standalone: [.NET 8 Runtime](https://dotnet.microsoft.com)
