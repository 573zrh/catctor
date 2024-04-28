
using CharacterAPI.Tables;
using CharacterAPI.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace CharacterAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {

            // ����һ���µ��������������� appsettings.json �ļ��м�������
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            ConfigurationHelper.InitConfiguration(Configuration);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(option => { option.Filters.Add(new GlobalExceptionFilter()); }).AddNewtonsoftJson(o => {
                //�޸��������Ƶ����л���ʽ������ĸСд
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //���ÿ���������������Դ          
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("cors", builder =>
                {
                    builder
                    .WithOrigins("*") //�����κ���Դ����������
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                options.AddPolicy("any", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("any");
            app.UseStaticFiles();
            app.UseAuthorization();

        
            app.MapControllers();

            SqlSugarHelper.Db.DbMaintenance.CreateDatabase();
            //���� �����ĵ�Ǩ�ƣ�
            SqlSugarHelper.Db.CodeFirst.InitTables<AnimationTable>(); //���пⶼ֧��
            SqlSugarHelper.Db.CodeFirst.InitTables<ImgTable>(); //���пⶼ֧��
            SqlSugarHelper.Db.CodeFirst.InitTables<ImgJsonTable>(); //���пⶼ֧��
            app.Run();
        }
    }
}
