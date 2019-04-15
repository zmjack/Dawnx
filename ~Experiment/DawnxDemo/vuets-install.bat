@echo off
rem webpack
call npm i -s webpack webpack-cli
rem loader
call npm i -s raw-loader vue-loader css-loader vue-style-loader ts-loader
rem vue
call npm i -s vue vue-template-compiler vue-class-component vue-property-decorator
rem typescript
call npm i -s typescript ts-nameof @types/ts-nameof

echo.
echo Maybe you should disable TypeScript compilation when the project is compiled.
echo To use the part of configuration:
echo.
echo   ^<PropertyGroup^>
echo     ^<TypeScriptCompileBlocked^>true^</TypeScriptCompileBlocked^>
echo   ^</PropertyGroup^>
