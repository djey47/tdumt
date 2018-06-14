@ECHO OFF
SET ZIP_ROOT="TDU Modding Tools"
SET ZIP=TDUModdingTools-x.xx-S-L.zip
ECHO MODDING TOOLS DEPLOYER
ECHO + BUILD ALL PROJECTS AS %MODE%!
ECHO + CURRENT TARGET IS %TARGET%
ECHO + 7-ZIP PATH IS %SEVENZIP%
ECHO + BIN PATH IS %BINARY%
ECHO.
PAUSE
ECHO.
CD %BINARY%
ECHO - Cleaning...
DEL %ZIP%
DEL patchs
RD /S /Q %ZIP_ROOT%
DEL %ZIP_ROOT%
ECHO.
ECHO - Directories...
MD patchs
MD %ZIP_ROOT%\patchs
MD %ZIP_ROOT%\xml\default\challenges
MD %ZIP_ROOT%\logos
MD %ZIP_ROOT%\tracks
ECHO.
ECHO - Libs...
COPY *.dll %ZIP_ROOT%
ECHO.
ECHO - Executables...
ECHO TDU Modding Tools.exe
COPY "TDU Modding Tools.exe" %ZIP_ROOT%\"TDU Modding Tools.exe"
ECHO TDU Mod And Play!.exe (deploy)
COPY ..\..\..\..\..\tdumt-patcher\trunk\bin\x86\%MODE%\"TDU Mod And Play!.exe" %ZIP_ROOT%\patchs
ECHO TDU Mod And Play!.exe (testing)
COPY /Y ..\..\..\..\..\tdumt-patcher\trunk\bin\x86\%MODE%\"TDU Mod And Play!.exe" patchs
ECHO TDU TrackPack Client.exe (deploy)
COPY ..\..\..\..\..\tdumt-trackpack\trunk\bin\x86\%MODE%\"TDU TrackPack Client.exe" %ZIP_ROOT%\tracks
ECHO TDU TrackPack Client.exe (testing)
COPY /Y ..\..\..\..\..\tdumt-trackpack\trunk\bin\x86\%MODE%\"TDU TrackPack Client.exe" tracks
ECHO.
ECHO - Data...
COPY xml\*.xml %ZIP_ROOT%\xml
ECHO.
ECHO - Default files...
COPY xml\default\* %ZIP_ROOT%\xml\default
ECHO.
ECHO - Logos...
COPY logos\*.png %ZIP_ROOT%\logos
ECHO.
ECHO - Challenges...
COPY xml\default\challenges\* %ZIP_ROOT%\xml\default\challenges
ECHO.
ECHO - Readmes...
ECHO readme.txt
COPY ..\..\..\ref\readmes\readme.txt %ZIP_ROOT%\readme.txt
ECHO do_not_run_me!.txt
COPY ..\..\..\ref\readmes\do_not_run_me!.txt %ZIP_ROOT%\patchs\do_not_run_me!.txt
ECHO your_tracks_here.txt
COPY tracks\your_tracks_here.txt %ZIP_ROOT%\tracks\your_tracks_here.txt
ECHO.
ECHO - ZIP...
%SEVENZIP% a -r %ZIP% %ZIP_ROOT%
ECHO Done.
GOTO End

:Error
ECHO Deploy error! 

:End
ECHO.
PAUSE
