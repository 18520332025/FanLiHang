using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace FanLiHang.Redis
{
    public class SERedisHelper
    {
        ConnectionMultiplexer client;
        public SERedisHelper(ConnectionMultiplexer client)
        {
            this.client = client;
        }

        public string StringGet(string key)
        {
            return client.GetDatabase().StringGet(key);
        }




        public string[] StringGetMany(string[] keyStrs)
        {
            var count = keyStrs.Length;
            var keys = new RedisKey[count];
            var addrs = new string[count];

            for (var i = 0; i < count; i++)
            {
                keys[i] = keyStrs[i];
            }
            try
            {
                var values = client.GetDatabase().StringGet(keys);
                for (var i = 0; i < values.Length; i++)
                {
                    addrs[i] = values[i];
                }
                return addrs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region 泛型
        /// <summary>
        /// 存值并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="t">实体</param>
        /// <param name="ts">过期时间间隔</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Set<T>(string key, T t, TimeSpan ts)
        {
            var str = JsonConvert.SerializeObject(t);
            return client.GetDatabase().StringSet(key, str, ts);
        }

        /// <summary>
        /// 获取指定的自增长ID
        /// </summary>
        /// <param name="key">相关业务Key</param>
        /// <returns></returns>
        public long Increment(string key, int num)
        {
            return client.GetDatabase().StringIncrement(key);
        }

        public long Decr(string key, int num)
        {
            return client.GetDatabase().StringDecrement(key);
        }




        /// <summary>
        /// 
        /// 根据Key获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key) where T : class
        {
            var strValue = client.GetDatabase().StringGet(key);
            return string.IsNullOrEmpty(strValue) ? null : JsonConvert.DeserializeObject<T>(strValue);
        }
        #endregion
    }
}
