﻿/**
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Kafka.Client.Utils
{
    using System;
    using System.Reflection;

    public static class ReflectionHelper
    {
        public static T Instantiate<T>(string className)
            where T : class
        {
            object o1;
            if (string.IsNullOrEmpty(className))
            {
                return default(T);
            }

            Type t1 = Type.GetType(className, true);
            if (t1.IsGenericType)
            {
                var t2 = typeof(T).GetGenericArguments();
                var t3 = t1.MakeGenericType(t2);
                o1 = Activator.CreateInstance(t3);
                return o1 as T;
            }

            o1 = Activator.CreateInstance(t1);
            var obj = o1 as T;
            if (obj == null)
            {
                throw new ArgumentException("Unable to instantiate class " + className + ", matching " + typeof(T).FullName);
            }

            return obj;
        }

        public static T GetInstanceField<T>(string name, object obj)
            where T : class
        {
            Type type = obj.GetType();
            FieldInfo info = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            object value = info.GetValue(obj);
            return (T)value;
        }
    }
}
