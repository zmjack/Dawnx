set dir=%cd%
set dir=%dir:D:\=/mnt/d/%
set dir=%dir:\=/%
bash.exe -c "cloc %dir% --report-file=cloc.txt"
pause
