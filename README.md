# MSFSAutostartManager

This is a small console application to automatically add or remove autostart entries into EXE.xml from Microsoft Flight Simulator 2020 (MSFS)

It has **no** user interface and only uses command-line options to select desired functions and parameters. 

Let's say you have created an Add-on for MSFS which should be started automatically with MSFS. You could make the installer of your tool run MSFSAutostartManager.exe to handle that for you.
No scripting, error checking or anything else required.

The location of the EXE.xml will be detected automatically, since it differs depending where you bought MSFS (MS Store or Steam)

In case the program runs successfully, it will return 0.
A return value of -1 indicates an error.

### Examples

Creates an autostart entry with the name "MyAddon" which launches MyAddon.exe

    MSFSAutostartManager.exe add -n "MyAddon" -p "C:\Folder\MyAddon.exe"
**<br>**
   
   Same as above, but also adds an Tag <CommandLine>-auto</CommandLine>
   Note the \ just before -auto. This is needed as an escape character and will be removed automatically  

    MSFSAutostartManager.exe add -n "MyAddon" -p "C:\Folder\MyAddon.exe" -c "\-auto"
    
**<br>**
Removes (if found) the autostart entry with the name "MyAddon"

    MSFSAutostartManager.exe remove -n "MyAddon"

