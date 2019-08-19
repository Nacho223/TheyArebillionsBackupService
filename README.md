# They Are Billions Backup Service
Windows Service that autobackups save folder each time the game modifies a file.

This program is born from true rage, after losing a very good survival scenario at the very end. Enjoy.

IMPORTANT NOTE:

************************************************************************************************************************************

The game only creates full save files IF YOU SAVE MANUALLY. Meanwhile, it stores useless backups. So, the service skips these files.

************************************************************************************************************************************

It saves every file stored in "Documents\My Games\They Are Billions" in "Documents\My Games\Fuck Numantian Games\Saves", in separate folders ordered by DateTime, so you can roll back to the moment you want.

Installation:

****If you don't want to download the entire code, go to bin/release, download Release.rar and extract it on any folder.

Copy the ..\bin\release folder anywhere. Run Command Prompt in admin mode from Task Manager --> Run. Go to the folder in CMD and write this command for 32bit or with Framework64 as below:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe C:\[your folder]\FuckTheyAreBillions.exe

EXAMPLE:

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installutil.exe C:\Program Files\FuckTheyAreBillions\FuckTheyAreBillions.exe

Then Windows will ask for Admin user and password (if you're in an Active Directory domain, set domain first: DOMAIN\User) and you will have the service installed in automatic mode, but not running.

If you want to start it, run services.msc and search for "FuckNumantian" --> Right mouse click --> Start, or simply restart your pc.
