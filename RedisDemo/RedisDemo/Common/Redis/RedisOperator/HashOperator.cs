using System;
using System.Collections.Generic;
using ServiceStack.Text;

namespace RedisDemo.Common.Redis.RedisOperator
{
    public class HashOperator : RedisOperatorBase
    {
        public HashOperator()
            : base()
        { }

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public bool Exist<T>(string hashId, string key)
        {
            return Redis.HashContainsEntry(hashId, key);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Set<T>(string hashId, string key, T t)
        {
            var value = JsonSerializer.SerializeToString<T>(t);
            return Redis.SetEntryInHash(hashId, key, value);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public bool Remove(string hashId, string key)
        {
            return Redis.RemoveEntryFromHash(hashId, key);
        }

        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Redis.Remove(key);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public T Get<T>(string hashId, string key)
        {
            string value = Redis.GetValueFromHash(hashId, key);
            return JsonSerializer.DeserializeFromString<T>(value);
        }

        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <typeparam name="T">数据对象</typeparam>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(string hashId)
        {
            var result = new List<T>();
            var list = Redis.GetHashValues(hashId);
            if (list != null && list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var value = JsonSerializer.DeserializeFromString<T>(x);
                    result.Add(value);
                });
            }
            return result;
        }

        /// <summary>
        /// 获取整个hash的数据的key
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public List<string> GetAllKeys(string hashId)
        {
            var keyList = new List<string>();
            var list = Redis.GetHashKeys(hashId);
            if (list != null && list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var key = JsonSerializer.DeserializeFromString<string>(x);
                    keyList.Add(key);
                });
            }
            return keyList;
        }

        /// <summary>
        /// 根据hashId，获取所有键值对
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAllEntriesFromHash(string hashId)
        {
            var dic = Redis.GetAllEntriesFromHash(hashId);
            return dic;
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void SetExpire(string key, DateTime datetime)
        {
            Redis.ExpireEntryAt(key, datetime);
        }
    }
}
