using System;
using ServiceStack.Redis;

namespace RedisDemo.Common.Redis.RedisOperator
{
    public abstract class RedisOperatorBase : IDisposable
    {
        protected IRedisClient Redis { get; private set; }

        private bool _disposed = false;

        protected RedisOperatorBase()
        {
            Redis = RedisManager.GetClient();
            //如有密码，则需要添加密码
            //Redis.Password = "asdfa1231!SSS";
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Redis.Dispose();
                    Redis = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Redis.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Redis.SaveAsync();
        }

    }
}
