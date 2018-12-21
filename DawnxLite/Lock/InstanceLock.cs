﻿using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dawnx.Lock
{
    public class InstanceLock<TInstance>
    {
        public Expression<Func<TInstance, object>>[] FlagExpressions { get; }
        protected Func<TInstance, object>[] FlagLambdas { get; }

        public InstanceLock(params Expression<Func<TInstance, object>>[] flagExpressions)
        {
            var isAllExpressionValid = flagExpressions.All(x =>
            {
                if (x.Body.NodeType == ExpressionType.Convert)
                    return BasicTypeUtility.IsBasicType((x.Body as UnaryExpression).Operand.Type);
                else return false;
            });

            if (!isAllExpressionValid)
                throw new ArgumentException("Every expression's return type must be basic type.");

            FlagExpressions = flagExpressions;
            FlagLambdas = FlagExpressions.Select(x => x.Compile()).ToArray();
        }

        public virtual string InternString(TInstance instance)
        {
            return string.Intern(
                typeof(TInstance).FullName + "\0"
                + FlagLambdas.Select(x => x(instance).ToString()).Join("\0"));
        }
        
        public Lock Begin(TInstance instance, TimeSpan timeout) => Lock.Get(InternString(instance), timeout);
        public Lock Begin(TInstance instance, int millisecondsTimeout) => Lock.Get(InternString(instance), millisecondsTimeout);
    }
}