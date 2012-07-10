#region

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Web;

#endregion

namespace BinbinUnitOfWork
{
    public class UnitOfWorkScope : IDisposable, IIUnitOfWorkScope<System.Data.Linq.DataContext>
    {
        /// <summary>
        ///   用来存放WinFrom程序中的事务
        /// </summary>
        [ThreadStatic]
        private static UnitOfWorkScope currentScope;


        private Guid scopeID = Guid.NewGuid();

        /// <summary>
        ///   存放已加入的事务的Connection的
        /// </summary>
        private List<DataContext> scopePool = new List<DataContext>();

        /// <summary>
        ///   构造方法
        /// </summary>
        public UnitOfWorkScope()
        {
            //如果当前没有起动 ,就记下此标志
            if (Current == null)
            {
                Current = this;
            }
        }

        /// <summary>
        ///   取得当前事务
        /// </summary>
        public static UnitOfWorkScope Current
        {
            get
            {
                //如果这不是一个Web项目
                if (HttpContext.Current == null)
                {
                    return currentScope;
                }
                else
                {
                    //Web项目的话,就把事务标志放到HttpContext中
                    HttpContext context = HttpContext.Current;

                    return context.Items["CurrentUnitOfWorkScope"] as UnitOfWorkScope;
                }
            }
            private set
            {
                if (HttpContext.Current == null)
                {
                    currentScope = value;
                }
                else
                {
                    HttpContext context = HttpContext.Current;


                    if (context.Items.Contains("CurrentUnitOfWorkScope"))
                        context.Items["CurrentUnitOfWorkScope"] = value;
                    else
                        context.Items.Add("CurrentUnitOfWorkScope", value);
                }
            }
        }

        /// <summary>
        ///   事务ID
        /// </summary>
        public Guid ScopeID
        {
            get { return scopeID; }
        }

        #region IIUnitOfWorkScope Members

        /// <summary>
        /// </summary>
        /// <param name="datacontext"></param>
        /// <returns></returns>
        public void JoinScope(DataContext datacontext)
        {
            if (!scopePool.Contains(datacontext))
            {
                scopePool.Add(datacontext);
            }
        }

        #endregion



        public override bool Equals(object obj)
        {
            if (obj is UnitOfWorkScope)
            {
                UnitOfWorkScope scope = obj as UnitOfWorkScope;

                return (scope.scopeID == scopeID);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return scopeID.GetHashCode();
        }


        private void RemoveTransaction()
        {
            Current = null;
        }

        #region IDisposable 成员

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (Current == null)
            {
                return;
            }

            if (Current.scopeID != scopeID)
            {
                return;
            }
            try
            {
                foreach (var datacontext in scopePool)
                {

                    datacontext.SubmitChanges();
                }
            }
            finally
            {
                this.scopePool.Clear();
            }

            RemoveTransaction();
        }

        #endregion
    }
}