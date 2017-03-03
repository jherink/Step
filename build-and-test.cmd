@echo off
set thisdir=%~dp0

:: download antlr
set antlr_jar=antlr-4.6-complete.jar
mkdir "%thisdir%bin">NUL 2>&1
if not exist "%thisdir%bin\%antlr_jar%" (
    bitsadmin /transfer "Download Antlr" http://www.antlr.org/download/%antlr_jar% "%thisdir%bin\%antlr_jar%"
)

set CLASSPATH=.;%thisdir%bin\%antlr_jar%;%CLASSPATH%
java org.antlr.v4.Tool %thisdir%src\ExpressSchema.g

:: build
set TEST_PROJECT=%thisdir%src\IxMilia.Step.Test\IxMilia.Step.Test.csproj
rem dotnet restore %TEST_PROJECT%
rem dotnet test %TEST_PROJECT%
