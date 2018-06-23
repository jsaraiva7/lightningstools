
# Fork from original repo to inclide a GUI to map signals and perform calibations on signals. 
The main goal of this fork is to provide a GUI to configure a PHCC system with falcon BMS. 


Several Viperpits members contacted me in the past to "finnish" my previous attempt to provide a GUI for PHCC system/Falcon BMS. 
I decided to fork original fork, and to make the code available on github so that it can possibly be merged on the mainproject (if changes make sense to the original author) and to make the code available at any point to the comunity. 

Instead of working on the previous code i wrote, i decided to start "fresh", as the original code was a kind of spargetti confusion. (my skills back then were limited). 

Please feel free to comment/contribute/fork/whatever!


#original description:

# Lightning's Tools

***
## Releases

### [End User Applications](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications)

#### [ADI Test Tool](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/ADI%20Test%20Tool)
Desktop application for testing Henkie's F-16 ADI Support Board with a real ARU-42/A Attitude Director Indicator instrument.

#### [Analog Devices AD536x-AD537x Eval Board Test Tool](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/Analog%20Devices%20AD536x-AD537x%20Eval%20Board%20Test%20Tool)
Desktop application for testing Analog Devices AD536x and AD537x digital-to-analog converter evaluation boards [providing +/-10VDC analog outputs in order to drive military-grade simulated flight instruments]. Demonstrates the use of the `AnalogDevices` class library.

#### [F16 Center Pedestal Display](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/F16%20Center%20Pedestal%20Display)
Desktop application providing a semi-realistic simulation of the Raytheon F-16 Center Pedestal Display for use with Falcon BMS.  

#### [Falcon 4 Keyfile Viewer](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/Falcon%204%20Keyfile%20Viewer)
Desktop application for viewing Falcon .key files.  Demonstrates the use of the `F4KeyFile` library.

#### [Falcon 4 Shared Memory Mirror](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/Falcon%204%20Shared%20Memory%20Mirror)
Desktop client-server application that mirrors the contents of Falcon's shared memory areas to one or more remote networked PCs.

#### [Falcon 4 Shared Memory Viewer](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/Falcon%204%20Shared%20Memory%20Viewer)
Desktop application that displays the contents of Falcon's shared memory areas.

#### [Falcon BMS Textures Shared Memory Tester](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/Falcon%20BMS%20Textures%20Shared%20Memory%20Tester)
Desktop application for displaying the contents of Falcon BMS' "Textures Shared Memory" area.   Demonstrates the use of the `F4TexSharedMem` library.

#### [MFDExtractor](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/Falcon%20MFD%20Extractor)
End user client/server application that allows extracting various flight instruments from Falcon.    

#### [JoyMapper](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/JoyMapper)
Desktop application for remapping analog and digital inputs from DirectInput devices, BetaInnovations non-Joystick-class HID devices, and PHCC devices using PPJoy virtual joystick drivers.  

#### [PHCC Test Tool](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/PHCC%20Test%20Tool)
End-user desktop application providing basic testing capabilities for the PHCC motherboard and attached peripherals.  Demonstrates the use of the `PHCC` class library.

#### [SDI Test Tool](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/SDI%20Test%20Tool)
End-user desktop application for testing Henkie's Digital-to-Synchro (SDI) interface card. Demonstrates the use of the `SDI` class library.

#### [SimLinkup](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/SimLinkup) 
##### [ADI Special Edition](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/SimLinkup/ADI%20Special%20Edition)
End-user desktop application for interfacing Falcon BMS with a real ARU-42/A Attitude Director Indicator instrument. 

#### [TlkTool](https://github.com/lightningviper/lightningstools/tree/master/releases/End%20User%20Applications/TlkTool)
End-user command-line (console) application which provides the ability to edit Falcon's AI comms databases (falcon.TLK, commFile.bin, fragFile.bin, and evalFile.bin files).  
- Supports decompressing and extracting any/all of the individual compressed audio files from within the .TLK file, to ordinary .WAV files or compressed .LH or .SPX files.  
- Supports exporting the commFile.bin, fragFile.bin, and evalFile.bin files to plain colon-delimited text files, which can then be edited in any text editor or spreadsheet.  
- Supports regenerating the commFile.bin, fragFile.bin, and evalFile.bin files from these plain text files.  
- Supports creating or regenerating a .TLK file from a set of .WAV, .LH, or .SPX files and updating individual entries in an existing .TLK file