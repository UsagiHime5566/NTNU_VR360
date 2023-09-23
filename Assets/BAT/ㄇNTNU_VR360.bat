@echo off
echo !!!
echo Wait for system prepare...
ping 127.0.0.1 -n 10 -w 1000
cd /D C:\Users\Neoforce\Desktop\v360_0922
NTNU_VR360.exe -screen-width 4800 -screen-height 600 -screen-fullscreen 1
