using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface
{
    public static class ThreadSafeAccesors
    {
        private delegate void SetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);

        public static void SetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
        {
            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;

            if (propertyInfo == null || (!@this.GetType().Equals(propertyInfo.ReflectedType) && !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType)) || @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (@this.InvokeRequired)
            {
                @this.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe), new object[] { @this, property, value });
            }
            else
            {
                @this.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value });
            }
        }

        private delegate TResult GetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property);

        public static TResult GetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property)
        {
            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;

            if (propertyInfo == null || !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) || @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (@this.InvokeRequired)
            {
                return (TResult)@this.Invoke(new GetPropertyThreadSafeDelegate<TResult>(GetPropertyThreadSafe), new object[] { @this, property });
            }
            else
            {
                return (TResult)@this.GetType().InvokeMember(propertyInfo.Name, BindingFlags.GetProperty, null, @this, null);
            }
        }
    }
}
