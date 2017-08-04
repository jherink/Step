@echo off

goto main

:build_and_test
set TEST_PROJECT=%1
dotnet restore %TEST_PROJECT%
if errorlevel 1 exit /b 1
dotnet test %TEST_PROJECT%
if errorlevel 1 exit /b 1
exit /b 0

:main
set PROJECTS=
set PROJECTS=%PROJECTS% .\src\IxMilia.Step.SchemaParser.Test\IxMilia.Step.SchemaParser.Test.fsproj
set PROJECTS=%PROJECTS% .\src\IxMilia.Step.Test\IxMilia.Step.Test.csproj
for %%p in (%PROJECTS%) do (
    call :build_and_test %%p
    if errorlevel 1 exit /b 1
)
