using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using static KmnlkCommon.Shareds.SharedEnums;

namespace KmnlkCommon.Shareds
{
    public class CacheManagement
    {
        private static string ENCODING = "$$##$$";
        public static double expireIntervalDefault = 10;

        private static ObjectCache cacheList=MemoryCache.Default;

        public static string getKeyHash(string key)
        {
            if (key != null)
            {
                return key.GetHashCode().ToString();
            }
            return "";
        }

        public static void addToCache(string key,string group,object value,bool enableExpire=false,Int64 expiryAfter=-1)
        {
            try
            {
                if (cacheList == null || cacheList.GetCount() == 0)
                {
                    cacheList = null;
                    cacheList = MemoryCache.Default;
                }

                
                key = getKeyHash(key);
                string hashKey = group + ENCODING + key;
                object item = cacheList.Get(hashKey);

                if (item == null)
                {
                    item = new CacheItem(hashKey, value);
                    DateTimeOffset time;
                    if (enableExpire && expiryAfter > 0)
                    {
                        time = DateTimeOffset.Now.AddMinutes(expiryAfter);
                    }
                    else
                    {
                        time = DateTimeOffset.Now.AddMinutes(expireIntervalDefault);
                    }
                    cacheList.Set(hashKey, value, time);
                }
            }catch(Exception e) { }
        }

        public static Enum_Status_Code updateExpiry(string key,string group, Int64 expiryAfter)
        {
            try
            {
               
                key = getKeyHash(key);
                string hashKey = group + ENCODING + key;
                object item = cacheList.Get(hashKey);
                DateTimeOffset time = DateTimeOffset.Now.AddMinutes(expiryAfter);
                cacheList.Set(hashKey, item , time);
                return SharedEnums.Enum_Status_Code.SUCCESS;
            }
            catch (Exception e) { }
            return SharedEnums.Enum_Status_Code.NOTSUCCESS;
        }
        public static object getFromCache(string key,string group,object defaultValue)
        {
            try
            {
             
                key = getKeyHash(key);
                string hashKey = group + ENCODING + key;
                if (cacheList == null)
                {
                    cacheList = MemoryCache.Default;
                }
                object item = cacheList.Get(hashKey);
                if (item != null)
                {
                    return item;
                }
            }catch(Exception e) { }
            return defaultValue;
        }
        public static Enum_Status_Code deleteFromCache(string key,string group)
        {
            try
            {

                key = getKeyHash(key);
                string hashKey = group + ENCODING + key;
                cacheList.Remove(hashKey);
                return SharedEnums.Enum_Status_Code.SUCCESS;
            }catch(Exception e) { }
            return SharedEnums.Enum_Status_Code.NOTSUCCESS;
        }
        public static Enum_Status_Code clearAll()
        {
            try
            {
                if (cacheList != null)
                {
                    foreach(var item in cacheList)
                    {
                        cacheList.Remove(item.Key);
                    }
                }
                return Enum_Status_Code.SUCCESS;
            }catch(Exception e)
            {

            }
            return Enum_Status_Code.NOTSUCCESS;
        }
        public static Enum_Status_Code clearAllByGroup(string group)
        {
            try
            {
                if (cacheList != null)
                {
                    foreach (var item in cacheList)
                    {
                        if(item.Key.StartsWith(group+ENCODING))
                        cacheList.Remove(item.Key);
                    }
                }
                return Enum_Status_Code.SUCCESS;
            }
            catch (Exception e)
            {

            }
            return Enum_Status_Code.NOTSUCCESS;
        }
    }
}
