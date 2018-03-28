#!/bin/sh -e

build_and_test () {
    TEST_PROJECT=$1
    dotnet restore $TEST_PROJECT
    dotnet test $TEST_PROJECT
}

build_and_test ./src/IxMilia.Step.SchemaParser.Test/IxMilia.Step.SchemaParser.Test.fsproj
build_and_test ./src/IxMilia.Step.Test/IxMilia.Step.Test.csproj
