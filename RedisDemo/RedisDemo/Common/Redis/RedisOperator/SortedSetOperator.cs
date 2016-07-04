using System;
using System.Collections.Generic;

namespace RedisDemo.Common.Redis.RedisOperator
{
    public class SortedSetOperator : RedisOperatorBase
    {
        public SortedSetOperator()
            : base()
        { }

        public bool AddItemToSortedSet(string key, string value)
        {
            return Redis.AddItemToSortedSet(key, value);
        }

        /// <summary>
        ///  添加数据到 SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public bool Add(string key, string value, double score)
        {
            return Redis.AddItemToSortedSet(key, value, score);
        }

        public bool AddRangeToSortedSet(string key, List<string> valueList, double score)
        {
            return Redis.AddRangeToSortedSet(key, valueList, score);
        }

        public bool AddRangeToSortedSet(string key, List<string> valueList, long score)
        {
            return Redis.AddRangeToSortedSet(key, valueList, score);
        }

        /// <summary>
        /// 移除数据从SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(string key, string value)
        {
            return Redis.RemoveItemFromSortedSet(key, value);
        }

        /// <summary>
        /// 修剪SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="size">保留的条数</param>
        /// <returns></returns>
        public long RemoveRange(string key, int size)
        {
            return Redis.RemoveRangeFromSortedSet(key, size, int.MaxValue);
        }

        public long RemoveRange(string key, int startIndex, int endIndex)
        {
            return Redis.RemoveRangeFromSortedSet(key, startIndex, endIndex);
        }

        public long RemoveRangeByScore(string key, double startScore, double endScore)
        {
            return Redis.RemoveRangeFromSortedSetByScore(key, startScore, endScore);
        }

        public long RemoveRangeByScore(string key, long startScore, long endScore)
        {
            return Redis.RemoveRangeFromSortedSetByScore(key, startScore, endScore);
        }

        /// <summary>
        /// 获取SortedSet的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetCount(string key)
        {
            return Redis.GetSortedSetCount(key);
        }

        /// <summary>
        /// 获取SortedSet的分页数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<string> GetWithPaging(string key, int pageIndex, int pageSize)
        {
            return Redis.GetRangeFromSortedSet(key, (pageIndex - 1) * pageSize, pageIndex * pageSize - 1);
        }

        public List<string> GetRangWithScore(string key, double fromScore, double toScore)
        {
            return Redis.GetRangeFromSortedSetByHighestScore(key, fromScore, toScore);
        }

        /// <summary>
        /// 获取SortedSet的全部数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetAll(string key)
        {
            return Redis.GetRangeFromSortedSet(key, 0, int.MaxValue);
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

        public double GetItemScore(string key, string value)
        {
            return Redis.GetItemScoreInSortedSet(key, value);
        }

    }
}
