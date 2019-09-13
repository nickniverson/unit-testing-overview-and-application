using System.Diagnostics;
using Lamar;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTestingOverviewAndApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}


		public void ConfigureContainer(ServiceRegistry services)
		{
			services.Scan(x =>
			{
				x.AssemblyContainingType<Startup>();
				x.WithDefaultConventions();
				x.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
				x.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
			});

			//Pipeline
			services.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPreProcessorBehavior<,>));
			services.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPostProcessorBehavior<,>));

			// This is the default but let's be explicit. At most we should be container scoped.
			services.For<IMediator>().Use<Mediator>().Transient();
			services.For<ServiceFactory>().Use(ctx => ctx.GetInstance);
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				var container = (IContainer)app.ApplicationServices;

				// log all registrations
				Debug.WriteLine(container.WhatDidIScan());
				Debug.WriteLine(container.WhatDoIHave());

				// validate container configuration
				container.AssertConfigurationIsValid();
			}

			app.UseMvc();
		}
	}
}
