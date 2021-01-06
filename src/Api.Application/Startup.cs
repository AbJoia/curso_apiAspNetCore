using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using src.Api.CrossCutting.DependencyInjection;
using src.Api.CrossCutting.Mappings;
using src.Api.Domain.Security;
using Swashbuckle.AspNetCore.Swagger;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment _environment { get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Config Integration Teste
            if(_environment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_Connection",
                                                    "Server=localhost;"
                                                   +"Port=3306;"
                                                   +"Database=dbAPI_Integration;"
                                                   +"Uid=root;"
                                                   +"Pwd=admin123");

                Environment.SetEnvironmentVariable("Database", "MYSQL");
                Environment.SetEnvironmentVariable( "Migration", "APLICAR");
                Environment.SetEnvironmentVariable( "Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable( "Issuer", "ExemploIssuer");
                Environment.SetEnvironmentVariable( "Seconds", "28800");
            }    

            ConfigureService.ConfigurationDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            //AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>{
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();

            //Implementação Autenticação
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);            

            services.AddAuthentication(authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>{
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });
            //Final Autenticação


            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Curso de API com AspNetCore 3.1 - Na Pratica",
                    Description = "Arquitetura DDD",
                    TermsOfService = new Uri("http://www.mfrinfo.com.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Abner Joia",
                        Email = "abnerjoia@gmail",
                        Url = new Uri("http://www.sitequalquer.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Termo de licença de uso",
                        Url = new Uri("http://www.google.com")
                    }        
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o token JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });
            });                      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }                    

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API com AspNetCore 3.1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
