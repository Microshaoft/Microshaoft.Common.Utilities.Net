﻿rem only for Windows/dos cmd
            
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MsSQL.Plugin\MsSQL.Plugin.3.x\bin\Debug\netstandard2.1\Microshaoft*.dll $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\Microshaoft*.dll $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\JTokenModelParameterValidatorsPlugins\SamplePlugin\SamplePlugin.3.x\bin\Debug\netcoreapp3.1\Microshaoft*.dll $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MsSQL.Plugin\MsSQL.Plugin.3.x\bin\Debug\netstandard2.1\Microshaoft*.pdb $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\Microshaoft*.pdb $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\JTokenModelParameterValidatorsPlugins\SamplePlugin\SamplePlugin.3.x\bin\Debug\netcoreapp3.1\Microshaoft*.pdb $(TargetDir)CompositionPlugins\ /Y


xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*mysql.data* $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*npgsql* $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*sqlite* $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*oracle* $(TargetDir)CompositionPlugins\ /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*db2* $(TargetDir)CompositionPlugins\ /Y

xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*mysql.data* $(TargetDir) /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*npgsql* $(TargetDir) /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*sqlite* $(TargetDir) /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*oracle* $(TargetDir) /Y
xcopy ..\..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.Plugin\MySQL.Plugin.3.x\bin\Debug\netstandard2.1\*db2* $(TargetDir) /Y
