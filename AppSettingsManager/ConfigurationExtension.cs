namespace AppSettingsManager
{
    public static class ConfigurationExtension
    {
        public static void AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration,
                    string configurationTag=null) where T : class
        {
            if(string.IsNullOrEmpty(configurationTag))
            {
                configurationTag = typeof(T).Name;
            }

            var instance = Activator.CreateInstance<T>();           
            configuration.GetSection(configurationTag).Bind(instance);
            services.AddSingleton(instance);
        }
    }
}
