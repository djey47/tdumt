@ECHO OFF
ECHO Please, build project as Release!
PAUSE
copy /Y ..\bin\Release\*.dll ..\bin\Release\*.exe  ..\..\tduf\tools\tdumt-cli
ECHO Please, add changes and commit to TDUF project!
ECHO Done!
PAUSE