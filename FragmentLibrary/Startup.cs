using System;
using FragmentLibrary.Application;
using FragmentLibrary.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using MongoDB.Driver;

namespace FragmentLibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentDirectoryHelpers.SetCurrentDirectory();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string mongoConnectionString =
                "mongodb://localhost:27017"; //Configuration.GetValue<string>("Repositories.MongoDB")
            services.AddSingleton(new MongoClient(mongoConnectionString));

            services.AddTransient<FragmentRepository>();
            services.AddTransient<FragmentImageRepository>();
            services.AddTransient<FragmetService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerDocument();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "clientapp/build"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(
                //routes =>
                //{
                //    routes.MapRoute(
                //        name: "default",
                //        template: "{controller}/{action=Index}/{id?}");
                //}
            );

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUi3();
            }

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientapp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        internal class CurrentDirectoryHelpers
        {
            internal const string AspNetCoreModuleDll = "aspnetcorev2_inprocess.dll";

            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            private static extern IntPtr GetModuleHandle(string lpModuleName);

            [System.Runtime.InteropServices.DllImport(AspNetCoreModuleDll)]
            private static extern int http_get_application_properties(ref IISConfigurationData iiConfigData);

            [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
            private struct IISConfigurationData
            {
                public IntPtr pNativeApplication;

                [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.BStr)]
                public string pwzFullApplicationPath;

                [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.BStr)]
                public string pwzVirtualApplicationPath;

                public bool fWindowsAuthEnabled;
                public bool fBasicAuthEnabled;
                public bool fAnonymousAuthEnable;
            }

            public static void SetCurrentDirectory()
            {
                try
                {
                    // Check if physical path was provided by ANCM
                    var sitePhysicalPath = Environment.GetEnvironmentVariable("ASPNETCORE_IIS_PHYSICAL_PATH");
                    if (string.IsNullOrEmpty(sitePhysicalPath))
                    {
                        // Skip if not running ANCM InProcess
                        if (GetModuleHandle(AspNetCoreModuleDll) == IntPtr.Zero)
                        {
                            return;
                        }

                        IISConfigurationData configurationData = default(IISConfigurationData);
                        if (http_get_application_properties(ref configurationData) != 0)
                        {
                            return;
                        }

                        sitePhysicalPath = configurationData.pwzFullApplicationPath;
                    }

                    Environment.CurrentDirectory = sitePhysicalPath;
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}