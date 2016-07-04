using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis.Generic;

namespace RedisDemo.Common.Redis.RedisOperator
{
    public class ListOperator : RedisOperatorBase
    {

        public ListOperator()
            : base()
        { }

        public void AddItem<T>(string key, T value)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.AddItemToList(redisTypedClient.Lists[key], value);
            }
        }

        public void AddItem<T>(string key, T value, TimeSpan expiresIn)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.AddItemToList(redisTypedClient.Lists[key], value);
                DateTime expiresTime = DateTime.Now;
                expiresTime = expiresTime.Add(expiresIn);
                redisTypedClient.ExpireEntryAt(key, expiresTime);
            }
        }

        public void AddItem<T>(string key, T value, DateTime expiresTime)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.AddItemToList(redisTypedClient.Lists[key], value);
                redisTypedClient.ExpireEntryAt(key, expiresTime);
            }
        }

        public void AddList<T>(string key, IEnumerable<T> values)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.Lists[key].AddRange(values);
            }
        }

        /// <summary>
        /// 根据IEnumerable数据添加链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="expiresTime"></param>
        public void AddList<T>(string key, IEnumerable<T> values, DateTime? expiresTime)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.Lists[key].AddRange(values);
                if (expiresTime.HasValue)
                {
                    redisTypedClient.ExpireEntryAt(key, expiresTime.Value);
                }
            }
        }

        public long GetCount(string key)
        {
            return Redis.GetListCount(key);
        }

        public List<T> GetAll<T>(string key)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Lists[key].ToList();
            }
        }

        public List<T> Get<T>(string key, Func<T, bool> func)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                var redisList = redisTypedClient.Lists[key];
                IEnumerable<T> values = redisList.Where(func);
                return values.ToList();
            }
        }

        public List<T> GetWithRange<T>(string key, int start, int count)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Lists[key].GetRange(start, start + count - 1);
            }
        }

        public List<T> GetWithPaging<T>(string key, int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1);
            return GetWithRange<T>(key, start, pageSize);
        }

        public bool Remove<T>(string key, T value)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], value) > 0;
            }
        }

        public void RemoveAt<T>(string key, int index)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.Lists[key].RemoveAt(index);
            }
        }

        public T RemoveStart<T>(string key)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Lists[key].RemoveStart();
            }
        }

        public T RemoveEnd<T>(string key)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                return redisTypedClient.Lists[key].RemoveEnd();
            }
        }

        /// <summary>
        /// 根据lambada表达式删除符合条件的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        public void RemoveEntityFromList<T>(string key, Func<T, bool> func)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                var redisList = redisTypedClient.Lists[key];
                IEnumerable<T> values = redisList.Where(func);
                foreach (T value in values)
                {
                    redisList.RemoveValue(value);
                }
            }
        }

        public void RemoveAll<T>(string key)
        {
            using (IRedisTypedClient<T> redisTypedClient = Redis.GetTypedClient<T>())
            {
                redisTypedClient.Lists[key].RemoveAll();
            }
        }
    }
}
