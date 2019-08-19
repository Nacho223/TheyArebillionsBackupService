# They Are Billions Backup Service
Windows Service that autobackups save folder each time the game modifies a file. It saves everything in Documents\My Games\Fuck Numantian Games\Saves

Installation:

Copy the ..\bin\release folder anywhere. Run Command Prompt in admin mode from Task Manager --> Run. Go to the folder in CMD and write this command for 32bit or with Framework64 as below:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe C:\[your folder]\FuckTheyAreBillions.exe

EXAMPLE:
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installutil.exe C:\Program Files\FuckTheyAreBillions\FuckTheyAreBillions.exe

Then Windows will ask for Admin user and password (if you're in an Active Directory domain, set domain first: DOMAIN\User) and you will have service installed in automatic mode, but not running.

If you want to start it, run services.msc and search for "FuckNumantian" --> Right mouse click --> Start, or simply restart your pc.
