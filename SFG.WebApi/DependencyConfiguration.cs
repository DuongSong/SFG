using System.Reflection;
using NetCore.AutoRegisterDi;
using SFG.Core.Repositories.Implementations;
using SFG.Core.Repositories.Interfaces;

namespace SFG.WebApi
{
	public static class DependencyConfiguration
	{
		public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
		{
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("SFG.Core"))
				.Where(x => x.Name.Contains("Service") || x.Name.Contains("Services"))
				.AsPublicImplementedInterfaces();

			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }
	}
}

