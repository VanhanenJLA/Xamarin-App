﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace DesignApp.Core
{
    public abstract class ExtendedBindableObject : BindableObject
    {

        bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);

            MemberInfo GetMemberInfo(Expression expression)
            {
                MemberExpression operand;
                LambdaExpression lambdaExpression = (LambdaExpression)expression;
                if (lambdaExpression.Body as UnaryExpression != null)
                {
                    UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                    operand = (MemberExpression)body.Operand;
                }
                else
                {
                    operand = (MemberExpression)lambdaExpression.Body;
                }
                return operand.Member;
            }

        }

    }

}