﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageUploader.Models.Services
{
    public static class InstancePoolService
    {
        private static ICollection<object> InstancePool { get; }

        static InstancePoolService()
        {
            InstancePool = new List<object>();
        }

        public static T CreateInstance<T>() where T : class
        {
            try
            {
                var instance = InstancePool.ToList().Find(x => x.GetType() == typeof(T));
                if (instance != null) return (T)instance;
                instance = Activator.CreateInstance(typeof(T));
                InstancePool.Add(instance);
                return (T)instance;
            }
            catch (Exception e)
            {
                CreateInstance<AppService>().LoggingHelper.Save(e);
                return null;
            }
        }
    }
}