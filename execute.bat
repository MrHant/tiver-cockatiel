set temptime=%TIME: =0%
.\libs\nunit.consolerunner.3.9.0\tools\nunit3-console.exe tiver_cockatiel.dll --result ./output/%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%_%temptime:~0,2%%temptime:~3,2%%temptime:~6,2%_TestResult.xml
pause