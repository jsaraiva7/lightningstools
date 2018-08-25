@ECHO OFF
SET MASTERBUILDDIR=%~dp0

REM Download and Install Git for Windows
CALL %MASTERBUILDDIR%InstallGitForWindows.bat

REM Clone Git repository
CALL %MASTERBUILDDIR%Clone.bat

REM Install Visual Studio 2017 Community Edition
CALL %MASTERBUILDDIR%InstallVisualStudio2017CommunityEdition.bat

REM Install Bootstrappers
CALL %MASTERBUILDDIR%InstallBootstrappers.bat

REM Install Visual Studio Installer Projects Add-In for Visual Studio
CALL %MASTERBUILDDIR%SetRegCapCompatibilityModeFlags.bat
CALL %MASTERBUILDDIR%InstallVisualStudio2017InstallerProjectAddIn.bat
CALL %MASTERBUILDDIR%SetRegCapCompatibilityModeFlags.bat

