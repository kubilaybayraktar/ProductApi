using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Product.Api.Tests
{
    public abstract class TestBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestBase()
        {
            string assemblyPath = new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath;
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(assemblyPath);
            string conn = cfg.ConnectionStrings.ConnectionStrings["DbContext"].ConnectionString;

            DbContextOptionsBuilder<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseSqlServer(conn);

            DataContext context = new DataContext(dbContextOptions.Options);
            _unitOfWork = new UnitOfWork(context);
        }

        protected virtual StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        protected Product GetRandomProduct(short status = 1)
        {
            return _unitOfWork.Products.GetList().Where(x => x.Status == status).FirstOrDefault();
        }

        protected LookupCategory GetRandomCategory(short status = 1)
        {
            return _unitOfWork.Categories.GetList().Where(x => x.Status == status).FirstOrDefault();
        }

        protected int GetRandomAttributeIdByCategoryId(int categoryId)
        {
            return _unitOfWork.CategoryAttributes.GetByCategoryId(categoryId).FirstOrDefault().AttributeId;
        }

        protected int GetRandomAttributeId()
        {
            return _unitOfWork.Attributes.GetList().Where(x => x.Status == 1).FirstOrDefault().Id;
        }
    }
}
