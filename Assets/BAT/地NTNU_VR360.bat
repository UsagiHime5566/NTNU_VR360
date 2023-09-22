@echo off
echo !!!
echo Wait for system prepare...
ping 127.0.0.1 -n 10 -w 1000
cd /D C:\Users\Neoforce\Desktop\v360_0922
setlocal
set regkey="HKEY_CURRENT_USER\Software\Artpower\NTNU_VR360"
reg add %regkey% /v "Screenmanager Resolution Width_h182942802" /T REG_DWORD /D 1920 /f
reg add %regkey% /v "Screenmanager Resolution Height_h2627697771" /T REG_DWORD /D 6000 /f
endlocal
NTNU_VR360.exe -screen-width 1920 -screen-height 6000 -screen-fullscreen 1
