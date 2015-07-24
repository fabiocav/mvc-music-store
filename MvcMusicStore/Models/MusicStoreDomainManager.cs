using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;

namespace MvcMusicStore.Models
{
    public class MusicStoreDomainManager<TDto, TModel> : MappedEntityDomainManager<TDto, TModel>
            where TDto : class, ITableData
        where TModel : class
    {
        private Type idAttributeType = typeof(MappedIdAttribute);

        public MusicStoreDomainManager(DbContext context, HttpRequestMessage requestMessage, bool enableSoftDelete = false)
            : base(context, requestMessage, enableSoftDelete)
        {

        }
        public override Task<bool> DeleteAsync(string id)
        {
            return this.DeleteItemAsync(id);
        }

        public override SingleResult<TDto> Lookup(string id)
        {
            PropertyInfo idProperty = GetIdProperty();

            if (idProperty != null)
            {
                var parameterExpression = Expression.Parameter(typeof(TModel));
                var propertyExpression = Expression.MakeMemberAccess(parameterExpression, idProperty);
                var idExpression = Expression.Constant(int.Parse(id));

                var lambda = Expression.Lambda<Func<TModel, bool>>(Expression.MakeBinary(ExpressionType.Equal, propertyExpression, idExpression), parameterExpression);

                return this.LookupEntity(lambda);
            }

            throw new InvalidOperationException(string.Format("Unable to locate ID property for type {0}", typeof(TModel)));
        }

        private System.Reflection.PropertyInfo GetIdProperty()
        {
            return typeof(TModel).GetProperties().Where(p => p.CustomAttributes.Any(d => d.AttributeType == idAttributeType)).FirstOrDefault();
        }

        public override Task<TDto> UpdateAsync(string id, Delta<TDto> patch)
        {
            return this.UpdateEntityAsync(patch, id);
        }
    }
}
