using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis.Generic;

namespace RedisDemo.Common.Redis.RedisOperator
{
    public class SetOperator : RedisOperatorBase
    {
        public SetOperator()
            : base()
        { }

        public void Set<T>(string key, T value)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.Sets[key].Add(value);
            }
        }

        /// <summary>
        /// 根据lambada表达式获取符合条件的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public List<T> Get<T>(string key, Func<T, bool> func)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Where(func).ToList();
            }
        }

        public List<T> GetTopN<T>(string key, int count)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Take(count).ToList();
            }
        }

        public List<T> GetTopNWithFilter<T>(string key, Func<T, bool> func, int count)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Where(func).Take(count).ToList();
            }
        }

        public List<T> GetWithRange<T>(string key, Func<T, bool> func, int start, int count)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Where(func).Skip(start).Take(count).ToList();
            }
        }

        public List<T> Reverse<T>(string key)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Reverse().ToList();
            }
        }

        public long GetCount<T>(string key)
        {
            return Redis.GetTypedClient<T>().Sets[key].Count;
        }

        public bool Contains<T>(string key, T value)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Contains(value);
            }
        }

        public bool Remove<T>(string key, T t)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Sets[key].Remove(t);
            }
        }

        public void RemoveAll<T>(string key, T t)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.Sets[key].Clear();
            }
        }

    }
}
