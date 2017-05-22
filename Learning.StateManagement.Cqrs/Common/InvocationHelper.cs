using System;
using System.Linq.Expressions;
using System.Reflection;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Common
{
    public static class InvocationHelper
    {
        public static MethodInfo GetMethod<T>(Expression<Func<T, object>> expression)
        {
            MethodCallExpression methodCall = (MethodCallExpression) expression.Body;
            return methodCall.Method;
        }


        public static MethodInfo GetMethod(
            Expression<Func<ICommandBus, Action<Func<Type, Action<ICommand>>>>> expression)
        {
            MethodCallExpression methodCall = (MethodCallExpression) expression.Body;
            return methodCall.Method;
        }

        public static object InvokeGenericMethodWithDynamicTypeArguments<T>(T target,
            Expression<Func<ICommandBus, Action<Func<Type, Action<ICommand>>>>> expression, 
            object[] methodArguments,
            params Type[] typeArguments)
        {


            var methodInfo = GetMethod(expression);
            if (methodInfo.GetGenericArguments().Length != typeArguments.Length)
                throw new ArgumentException(
                    string.Format(
                        "The method '{0}' has {1} type argument(s) but {2} type argument(s) were passed. The amounts must be equal.",
                        methodInfo.Name,
                        methodInfo.GetGenericArguments().Length,
                        typeArguments.Length));

            return methodInfo
                .GetGenericMethodDefinition()
                .MakeGenericMethod(typeArguments)
                .Invoke(target, methodArguments);
        }
    }
}