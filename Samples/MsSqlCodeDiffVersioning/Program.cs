﻿//rem only for Windows/dos cmd
//rem xcopy ..\..\StoreProcedureWebApiExecutorsPlugins\MsSQL.StoreProcedureWebApiExecutor.Plugin\bin\Debug\netcoreapp2.2\*plugin* $(TargetDir)CompositionPlugins\ /Y
//rem xcopy ..\..\StoreProcedureWebApiExecutorsPlugins\MySQL.StoreProcedureWebApiExecutor.Plugin\bin\Debug\netcoreapp2.2\*plugin* $(TargetDir)CompositionPlugins\ /Y
//rem xcopy ..\..\JTokenModelParameterValidatorsPlugins\JTokenModelParameterValidatorSamplePlugin\bin\Debug\netcoreapp2.2\*plugin* $(TargetDir)CompositionPlugins\ /Y

namespace WebApplication.ASPNetCore
{
    using Microshaoft;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;


    public class Program
    { 
        public static void Main(string[] args)
        {
            OSPlatform OSPlatform
                    = EnumerableHelper
                            .Range
                                (
                                    OSPlatform.Linux
                                    , OSPlatform.OSX
                                    , OSPlatform.Windows
                                )
                            .First
                                (
                                    (x) =>
                                    {
                                        return
                                            RuntimeInformation
                                                .IsOSPlatform(x);
                                    }
                                );
            var s = $"{nameof(RuntimeInformation.FrameworkDescription)}:{RuntimeInformation.FrameworkDescription}";
            s += "\n";
            s += $"{nameof(RuntimeInformation.OSArchitecture)}:{RuntimeInformation.OSArchitecture.ToString()}";
            s += "\n";
            s += $"{nameof(RuntimeInformation.OSDescription)}:{RuntimeInformation.OSDescription}";
            s += "\n";
            s += $"{nameof(RuntimeInformation.ProcessArchitecture)}:{RuntimeInformation.ProcessArchitecture.ToString()}";
            s += "\n";
            s += $"{nameof(OSPlatform)}:{OSPlatform}";
            Console.WriteLine(s);
            var os = Environment.OSVersion;
            Console.WriteLine("Current OS Information:\n");
            Console.WriteLine("Platform: {0:G}", os.Platform);
            Console.WriteLine("Version String: {0}", os.VersionString);
            Console.WriteLine("Version Information:");
            Console.WriteLine("   Major: {0}", os.Version.Major);
            Console.WriteLine("   Minor: {0}", os.Version.Minor);
            Console.WriteLine("Service Pack: '{0}'", os.ServicePack);
            CreateWebHostBuilder
                (args)
                    //.UseKestrel()
                    //.UseContentRoot(Directory.GetCurrentDirectory())
                    //.UseIISIntegration()
                    .Build()
                    .Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var executingDirectory =
                        Path
                            .GetDirectoryName
                                    (
                                        Assembly
                                            .GetExecutingAssembly()
                                            .Location
                                    );
            return
                WebHost
                    .CreateDefaultBuilder(args)
                    //.UseConfiguration(configuration)

                    .ConfigureLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Error);
                        builder.AddConsole();
                    })

                    .ConfigureAppConfiguration
                    (
                        (hostingContext, configuration) =>
                        {
                            configuration
                                .SetBasePath(executingDirectory);
                            configuration
                                .AddJsonFile
                                    (
                                        path: "hostings.json"
                                        , optional: false
                                        , reloadOnChange: true
                                    )
                                .AddJsonFile
                                    (
                                        path: "dbConnections.json"
                                        , optional: false
                                        , reloadOnChange: true
                                    )
                                .AddJsonFile
                                    (
                                        path: "routes.json"
                                        , optional: false
                                        , reloadOnChange: true
                                    )
                                .AddJsonFile
                                    (
                                        path: "dynamicCompositionPluginsPaths.json"
                                        , optional: false
                                        , reloadOnChange: true
                                    )
                                .AddJsonFile
                                    (
                                        path: "JwtValidation.json"
                                        , optional: false
                                        , reloadOnChange: true
                                    );
                        }
                    )
                    //.UseUrls("http://+:5000", "https://+:5001")
                    .UseStartup<Startup>();
        }
    }
}