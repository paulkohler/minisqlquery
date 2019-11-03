REM setp both the VS.NET Pro path (local dev), and the community edition path (appveyor)
SET PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\
SET PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin

Tools\nant\bin\nant.exe full-build

@PAUSE