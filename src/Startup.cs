using FakeOmmerce.Data;
using FakeOmmerce.Models;
using FakeOmmerce.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;

namespace FakeOmmerce
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
            var config = new ServerConfig();
            Configuration.Bind(config);
            
            BsonClassMap.RegisterClassMap<Product>(cm => {
                cm.MapIdField(c => c.Id).SetElementName("id");
                cm.MapField(c => c.Name).SetElementName("name");
                cm.MapField(c => c.Brand).SetElementName("brand");
                cm.MapField(c => c.Categories).SetElementName("categories");
                cm.MapField(c => c.Images).SetElementName("images");
                cm.MapField(c => c.Price).SetElementName("price");
                cm.MapField(c => c.Description).SetElementName("description");       
                cm.MapCreator( p => new Product(p.Id, p.Name, p.Images, p.Categories, p.Price, p.Brand, p.Description));         
            });

            
            var productContext = new ProductContext(config.MongoDB);
            var productRepository = new ProductRepository(productContext);
            
            services.AddSingleton<IProductRepository>(productRepository);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
