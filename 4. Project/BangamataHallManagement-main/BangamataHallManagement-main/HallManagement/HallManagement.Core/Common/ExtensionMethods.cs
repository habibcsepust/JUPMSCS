using AutoMapper;
using AutoMapper.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CFormManagement.Core.Common
{
    public static class ExtensionMethods
    {
        //public static string GetBaseUrl(this HttpRequestBase request)
        //{
        //    if (request.Url == (Uri)null)
        //        return String.Empty;
        //    else
        //        return request.Url.Scheme + "://" + request.Url.Authority + VirtualPathUtility.ToAbsolute("~/");
        //    //return $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //}
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }


        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static object ValueConvert(Type type, object value)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value.ToString());
            return Convert.ChangeType(value, type);

            //var underlyingType = Nullable.GetUnderlyingType(type);
            //if (underlyingType == null)
            //    return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            //return value == null
            //    ? null
            //    : System.Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
        }

        public static T ValueConvert<T>(object value)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), value.ToString());
            return (T)Convert.ChangeType(value, typeof(T));

            //var underlyingType = Nullable.GetUnderlyingType(typeof(T));
            //if (underlyingType == null)
            //    return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            //return (T)(value == null
            //    ? null
            //    : Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture));
        }

        public static List<string> GetProperties(this Type obj)
        {
            //Getting Type of Generic Class Model
            Type tModelType = obj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();
            List<string> result = new List<string>();

            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                result.Add(property.Name);
            }

            return result;
        }

        /// <summary>
        /// LINQ equivalent of foreach for IEnumerable<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
        public static string GetMemberName<T>(this T instance, Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression.Body);
        }
        public static string GetMemberName<T>(this T instance, Expression<Action<T>> expression)
        {
            return GetMemberName(expression.Body);
        }
        private static string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException();
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException();
        }
        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
        public static List<string> GetMemberNames<T>(this T instance, params Expression<Func<T, object>>[] expressions)
        {
            List<string> memberNames = new List<string>();
            foreach (var cExpression in expressions)
            {
                memberNames.Add(GetMemberName(cExpression.Body));
            }

            return memberNames;
        }
        public static DataTable ToDataTable<TSource>(this TSource data)
        {
            var dataTable = new DataTable(typeof(TSource).Name);
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                                 prop.PropertyType);
            }

            var values = new object[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(data, null);
            }
            dataTable.Rows.Add(values);

            return dataTable;
        }
        public static DataTable ToDataTable<TSource>(this IList<TSource> data, string tableName)
        {
            var dataTable = new DataTable(tableName);
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                                 prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static DataTable ToListToDataTable<TSource>(this IList<TSource> data)
        {
            var dataTable = new DataTable(typeof(TSource).Name);
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                                 prop.PropertyType);
            }

            foreach (TSource item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {
                                     aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                                            aProp.PropertyType
                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            //foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value)
                        ? null
                        : dataRow[aField.Name]; //if database field is nullable
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name,
                    Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        /// <summary>
        /// Remove duplicate records from data table
        /// </summary>
        /// <param name="table">DataTable for removing duplicate records</param>
        /// <param name="distinctColumn">Column to check for duplicate values or records</param>
        /// <returns></returns>
        public static DataTable RemoveDuplicateRows(this DataTable table, string distinctColumn)
        {
            try
            {
                var uniqueRecords = new ArrayList();
                var duplicateRecords = new ArrayList();

                // Check if records is already added to UniqueRecords otherwise,
                // Add the records to DuplicateRecords
                foreach (DataRow dRow in table.Rows)
                {
                    if (uniqueRecords.Contains(dRow[distinctColumn]))
                        duplicateRecords.Add(dRow);
                    else
                        uniqueRecords.Add(dRow[distinctColumn]);
                }

                // Remove dupliate rows from DataTable added to DuplicateRecords
                foreach (DataRow dRow in duplicateRecords)
                {
                    table.Rows.Remove(dRow);
                }

                // Return the clean DataTable which contains unique records.
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        //public static EnumerableRowCollection<Dictionary<string, object>> ToDictionary(this DataTable dt)
        //{
        //    return dt.AsEnumerable()
        //        .Select(dr => dt.Columns.Cast<DataColumn>().ToDictionary(dc => dc.ColumnName, dc => dr[dc]));

        //    //return dt.AsEnumerable().ToDictionary<DataRow, dynamic, dynamic>(row => row.Field<string>(0),
        //    //                            row => row.Field<object>(1));
        //}

        public static int DateTimeToInt(this DateTime theDate)
        {
            return (int)(theDate.Date - new DateTime(1900, 1, 1)).TotalSeconds;
        }

        public static string ToTimeStamp(this DateTime theDate)
        {
            return theDate.ToString("yyyyMMddHHmmssfff");
        }

        public static string AppendTimeStamp(this string fileName)
        {
            return String.Concat(Path.GetFileNameWithoutExtension(fileName) + "_",
                DateTime.Now.ToString("yyyy-MM-dd-HHmmssfff"),
                Path.GetExtension(fileName)
                );
        }

        public static string ToSentence(this string valueString)
        {
            return new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture).Replace(valueString.ToLower(),
                s => s.Value.ToUpper());
        }

        public static double ToJulianDate(this DateTime date)
        {
            return date.ToOADate() + 2415018.5;
        }

        public static void WriteLog(this Exception ex)
        {
            var startupPath =
                new DirectoryInfo(
                    Path.GetDirectoryName(
                        Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))).Parent
                    .FullName;
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(st.FrameCount - 1);
            File.AppendAllText(startupPath + "\\ErrorLog.txt",
                DateTime.Now.ToString("dd/MMM/yyyy HH:mm") + ":: " + Path.GetFileName(frame.GetFileName())
                + ":: " + frame.GetMethod().Name + "::" + frame.GetFileLineNumber() + " :: " + ex.Message +
                Environment.NewLine);
        }
        public static void WriteValue(this string value, string fileName = "ErrorLog.txt")
        {
            var startupPath =
               new DirectoryInfo(
                   Path.GetDirectoryName(
                       Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))).Parent
                   .FullName;
            File.AppendAllText(startupPath + "\\" + fileName, DateTime.Now.ToString("dd/MMM/yyyy HH:mm") + "::" + value + Environment.NewLine);
        }

        public static bool IsDefault<T>(this T value) where T : struct
        {
            bool isDefault = value.Equals(default(T));

            return isDefault;
        }

        public static Dictionary<string, object> RemoveUnused<T>(this T obj)
        {
            var t = obj.GetType();
            var returnClass = new ExpandoObject() as IDictionary<string, object>;
            foreach (var pr in t.GetProperties())
            {
                var val = pr.GetValue(obj);
                if (val == null)
                    continue;
                if (val is string && String.IsNullOrWhiteSpace(val.ToString()))
                    continue;
                if (val is int && Convert.ToInt32(val) == 0)
                    continue;
                if (val is long && Convert.ToInt64(val) == 0)
                    continue;
                if (val is double && Math.Abs(Convert.ToDouble(val)) <= 0.0)
                    continue;
                if (val is decimal && Convert.ToDecimal(val) == 0)
                    continue;
                if (val is DateTime && (DateTime)val == default(DateTime))
                    continue;
                returnClass.Add(pr.Name, val);
            }
            return new Dictionary<string, object>(returnClass);
        }

        public static List<T> SetDefaultValue<T>(this List<T> aList)
        {

            foreach (var obj in aList)
            {
                foreach (var prop in obj.GetType().GetProperties())
                {
                    var propertyType = prop.PropertyType.FullName.ToString();
                    var propertyName = prop.Name;
                    var propertyValue = prop.GetValue(obj, null);


                    if (propertyValue == null)
                    {
                        if (propertyType.Contains("System.String"))
                            prop.SetValue(obj, "");
                        else if (propertyType.Contains("System.Boolean"))
                            prop.SetValue(obj, false);
                        else if (propertyType.Contains("System.Int16"))
                            prop.SetValue(obj, 0);
                        else if (propertyType.Contains("System.Int32"))
                            prop.SetValue(obj, 0);
                        else if (propertyType.Contains("System.Int64"))
                            prop.SetValue(obj, 0L);

                        else if (propertyType.Contains("System.DateTime"))
                        {
                            DateTime dt = Convert.ToDateTime("1900/01/01");
                            prop.SetValue(obj, dt);
                        }
                        else if (propertyType.Contains("System.Double"))
                            prop.SetValue(obj, 0.0d);
                        else if (propertyType.Contains("System.Decimal"))
                            prop.SetValue(obj, 0.0M);

                    }
                }
            }
            return aList;
        }

        public static T SetDefaultValue<T>(this T obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                var propertyType = prop.PropertyType.FullName;
                var propertyName = prop.Name;
                var propertyValue = prop.GetValue(obj, null);


                if (propertyValue == null)
                {
                    if (propertyType.Contains("System.String"))
                        prop.SetValue(obj, "");
                    else if (propertyType.Contains("System.Boolean"))
                        prop.SetValue(obj, false);
                    else if (propertyType.Contains("System.Int16"))
                        prop.SetValue(obj, 0);
                    else if (propertyType.Contains("System.Int32"))
                        prop.SetValue(obj, 0);
                    else if (propertyType.Contains("System.Int64"))
                        prop.SetValue(obj, 0L);

                    else if (propertyType.Contains("System.DateTime"))
                    {
                        DateTime dt = Convert.ToDateTime("1900/01/01");
                        prop.SetValue(obj, dt);
                    }
                    else if (propertyType.Contains("System.Double"))
                        prop.SetValue(obj, 0.0d);
                    else if (propertyType.Contains("System.Decimal"))
                        prop.SetValue(obj, 0.0M);

                }
            }

            return obj;
        }

        public static List<string> GetPropertyNames<T>(this T obj)
        {
            return obj.GetType().GetProperties().Select(prop => prop.Name).ToList();
        }

        public static string GetPropertyPath(MemberExpression memberExpression)
        {
            string property = memberExpression.ToString();
            return property.Substring(property.IndexOf('.') + 1);
        }
        public static string GetPropertyPath<T>(Expression<Func<T, object>> expression, out Type targetType)
        {
            MethodCallExpression methodCallExpression = expression.Body as MethodCallExpression;

            if (methodCallExpression != null)
            {
                if (methodCallExpression.Arguments.Count == 2)
                {
                    MemberExpression memberExpression1 = methodCallExpression.Arguments[0] as MemberExpression;
                    LambdaExpression lambdaExpression = methodCallExpression.Arguments[1] as LambdaExpression;

                    if (memberExpression1 != null && lambdaExpression != null)
                    {
                        MemberExpression memberExpression2 = lambdaExpression.Body as MemberExpression;

                        if (memberExpression2 != null)
                        {
                            targetType = memberExpression2.Type;

                            return $"{GetPropertyPath(memberExpression1)}.{GetPropertyPath(memberExpression2)}";
                        }
                    }
                }

                throw new ArgumentException(@"Please provide a lambda expression like 'n => n.Collection.Select(c => c.PropertyName)'", "expression");
            }
            else
            {
                UnaryExpression unaryExpression = expression.Body as UnaryExpression;
                MemberExpression memberExpression = null;

                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
                else
                {
                    memberExpression = expression.Body as MemberExpression;
                }

                if (memberExpression != null)
                {
                    targetType = memberExpression.Type;

                    return GetPropertyPath(memberExpression);
                }

                throw new ArgumentException(@"Please provide a lambda expression like 'n => n.PropertyName'", "expression");
            }
        }
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            var diff = dateTime.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dateTime.AddDays(-1 * diff).Date;
        }

        public static T ToEnum<T>(this string value)
        {
            if (value.Contains(" "))
            {
                value = value.Replace(" ", "_");
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string EnumToString(this Enum value)
        {
            string result = value.ToString();
            if (result.Contains("_"))
            {
                result = result.Replace("_", " ");
            }
            return result;
        }

        public static string ToDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])field
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }

        public static T FromXmlFile<T>(this string path)
        {
            var xmlDoc = new XmlDocument { XmlResolver = null };
            xmlDoc.Load(path);
            string xmlString = xmlDoc.InnerXml;
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(T));
            var instance = (T)serializer.Deserialize(reader);
            return instance;
        }
        public static T FromXmlString<T>(this string xmlString)
        {
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(T));
            var instance = (T)serializer.Deserialize(reader);
            return instance;
        }
        public static string ToXMLString<T>(this T obj)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, obj);
            return stringwriter.ToString();
        }
        public static string ExportToXml<T>(this T obj, string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = new DirectoryInfo(
                    Path.GetDirectoryName(
                        Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))).Parent
                    .FullName;
            }
            var writer = new XmlSerializer(typeof(T));
            var fileName = DateTime.Now.ToTimeStamp() + ".xml";
            var filePath = path + fileName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            var file = File.Create(filePath);
            writer.Serialize(file, obj);
            file.Close();
            return filePath;
        }

        public static bool IsExist(this List<string> list, string value)
        {
            return list.Exists(element => element == value);
        }
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = new RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }

        public static object GetObject(this Dictionary<string, object> dict, Type type)
        {
            var obj = Activator.CreateInstance(type);

            foreach (var item in dict)
            {
                var propertyInfo = obj.GetType().GetProperty(item.Key);
                if (propertyInfo == null) continue;
                var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                if (underlyingType == null)
                {
                    propertyInfo.SetValue(obj, Convert.ChangeType(item.Value, propertyInfo.PropertyType), null);
                }
                propertyInfo.SetValue(obj,
                    string.IsNullOrEmpty(item.Value.ToString())
                        ? null
                        : Convert.ChangeType(item.Value, underlyingType ?? propertyInfo.PropertyType), null);

            }
            return obj;
        }
        public static T GetObject<T>(this Dictionary<string, object> dict)
        {
            return (T)GetObject(dict, typeof(T));
        }
        public static List<T> DictionaryToListObject<T>(this Dictionary<string, object> dictionary)
        {
            if (dictionary.Count == 0) return null;
            List<object> result = dictionary.GroupBy(item => item.Key.Substring(0, item.Key.IndexOf(".", StringComparison.Ordinal)))
                  .Select(group => group.Aggregate(Activator.CreateInstance(typeof(T)), (obj, item) =>
                  {
                      var propertyInfo = obj.GetType().GetProperty(item.Key.Substring(item.Key.IndexOf(".", StringComparison.Ordinal) + 1));
                      if (propertyInfo == null) return obj;
                      var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                      if (underlyingType == null)
                      {
                          propertyInfo.SetValue(obj, Convert.ChangeType(item.Value, propertyInfo.PropertyType), null);
                      }
                      propertyInfo.SetValue(obj,
                          string.IsNullOrEmpty(item.Value.ToString())
                              ? null
                              : Convert.ChangeType(item.Value, underlyingType ?? propertyInfo.PropertyType), null);

                      return obj;

                  })).ToList();
            return result.OfType<T>().ToList();
        }

        public static DateTime ToDateTime(this string value)
        {
            var time = new DateTime();
            var matchingCulture =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .FirstOrDefault(ci => DateTime.TryParse(value, ci, DateTimeStyles.None, out time));
            return time;
        }

        public static string ObjectToString(this object newValue)
        {
            if (newValue == null) return string.Empty;
            var strNew = string.Empty;
            var aPropertyInfo = newValue.GetType().GetProperties();
            strNew = aPropertyInfo.Aggregate(strNew, (current, aProperty) => string.IsNullOrEmpty(current)
                ? aProperty.Name + ": " + aProperty.GetValue(newValue, null)
                : current + "; " + aProperty.Name + ": " + aProperty.GetValue(newValue, null));
            return strNew;
        }

        public static string PrettyName(this Type type)
        {
            if (type.GetGenericArguments().Length == 0)
            {
                return type.Name;
            }
            var genericArguments = type.GetGenericArguments();
            var typeDefinition = type.Name;
            var unmanagedName = typeDefinition.Substring(0, typeDefinition.IndexOf("`", StringComparison.Ordinal));
            return unmanagedName + "<" + string.Join(",", genericArguments.Select(PrettyName)) + ">";
        }

        public static string GetUniqueDigits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return $"{random:D8}";
        }

        #region Additional
        public static IDictionary<string, string> FromLegacyCookieString(this string legacyCookie)
        {
            return legacyCookie.Split('&').Select(s => s.Split('=')).ToDictionary(kvp => kvp[0], kvp => kvp[1]);
        }

        public static string ToLegacyCookieString(this IDictionary<string, string> dict)
        {
            return string.Join("&", dict.Select(kvp => string.Join("=", kvp.Key, kvp.Value)));
        }

        public static CustomTypeSqlQuery<T> SqlQuery<T>(this DatabaseFacade database, string sqlQuery, params object[] parameters) where T : class
        {
            var data = new CustomTypeSqlQuery<T>
            {
                DatabaseFacade = database,
                SqlQuery = sqlQuery,
                Parameters = parameters
            };
            return data;
        }

        public static List<string> CustomSqlQueryString(this DatabaseFacade database, string sqlQuery, params object[] parameters)
        {
            var conn = database.GetDbConnection();
            List<string> results = new List<string>();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    command.Parameters.AddRange(parameters);

                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            results.Add(reader[0].ToString());
                        }
                    }

                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return results;
        }
        public static async Task<List<string>> CustomSqlQueryStringAsync(this DatabaseFacade database, string sqlQuery, params object[] parameters)
        {
            var conn = database.GetDbConnection();
            List<string> results = new List<string>();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    command.Parameters.AddRange(parameters);

                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            results.Add(reader[0].ToString());
                        }
                    }

                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return results;
        }
        public static IEnumerable<T> CustomSqlQueryList<T>(this DatabaseFacade database, string sqlQuery, params object[] parameters) where T : class
        {
            List<T> results = new List<T>();
            var conn = database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    command.Parameters.AddRange(parameters);

                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        //dynamic dynamicReader = new DynamicDataReader(reader);
                        //var b = reader.GetListFromDataReader<dynamic>();
                        //var a = EntityMapper.MapToEntities<string>(dynamicReader);
                        //if (typeof(T) == typeof(string))
                        results = reader.CreateList<T>();
                    }

                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return results;
        }
        public static async Task<IEnumerable<T>> CustomSqlQueryListAsync<T>(this DatabaseFacade database, string sqlQuery, params object[] parameters) where T : class
        {
            List<T> results = new List<T>();
            var conn = database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    command.Parameters.AddRange(parameters);

                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        results = reader.CreateList<T>();
                    }

                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return results;
        }
        public static List<T> CreateList<T>(this DbDataReader reader) where T : class
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                    }
                }
                results.Add(item);
            }
            return results;
        }
        #region DynamicQuery
        public static IEnumerable DynamicExecuteQuery(this DbContext dbContext, string sql, params object[] parameters)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = cmd.Connection.ConnectionTimeout;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                //if (parameters != null)
                //{
                //    foreach (KeyValuePair<string, object> param in parameters)
                //    {
                //        DbParameter dbParameter = cmd.CreateParameter();
                //        dbParameter.ParameterName = param.Key;
                //        dbParameter.Value = param.Value;
                //        cmd.Parameters.Add(dbParameter);
                //    }
                //}

                //var retObject = new List<dynamic>();
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = GetDataRow(dataReader);
                        yield return dataRow;
                    }
                }
            }
        }

        private static dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
            return dataRow;
        }

        public static IEnumerable DynamicSqlQuery(this DbContext dbContext, string sql, params object[] parameters)
        {
            TypeBuilder builder = CreateTypeBuilder("EzyDynamicAssembly", "EzyDynamicModule", "EzyDynamicClass");

            using (IDbCommand command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    command.CommandText = sql;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    if (command.Connection.State != ConnectionState.Open)
                        command.Connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        var schema = reader.GetSchemaTable();
                        if (schema != null)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                string name = (string)row["ColumnName"];
                                Type type = (Type)row["DataType"];
                                if (type != typeof(string) && (bool)row.ItemArray[schema.Columns.IndexOf("AllowDbNull")])
                                {
                                    type = typeof(Nullable<>).MakeGenericType(type);
                                }
                                CreateAutoImplementedProperty(builder, name, type);
                            }
                        }

                    }
                }
                finally
                {
                    command.Connection.Close();
                    command.Parameters.Clear();
                }
            }

            try
            {
                Type resultType = builder.MakeByRefType();
            }
            catch (Exception ex)
            {
            }
            return null;
            //return dbContext.SqlQuery(resultType, sql, parameters);
        }

        private static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            AssemblyBuilder myAsmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
            ModuleBuilder intVectorModule = myAsmBuilder.DefineDynamicModule(moduleName);
            TypeBuilder typeBuilder = intVectorModule.DefineType(typeName, TypeAttributes.Public);
            typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

            return typeBuilder;
        }

        private static void CreateAutoImplementedProperty(TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(
                string.Concat(PrivateFieldPrefix, propertyName),
                              propertyType, FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(
                propertyName, System.Reflection.PropertyAttributes.HasDefault, propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes =
                MethodAttributes.Public | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(
                string.Concat(GetterPrefix, propertyName),
                propertyMethodAttributes, propertyType, Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(
                string.Concat(SetterPrefix, propertyName),
                propertyMethodAttributes, null, new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
        #endregion
        #endregion

        public static IEnumerable<T> GetListFromDataReader<T>(this IDataReader reader) where T : new()
        {
            var properties = typeof(T).GetProperties();

            var modelProperties = new List<string>();
            var columnList = (reader.GetSchemaTable().Select()).Select(r => r.ItemArray[0].ToString());
            while (reader.Read())
            {
                var element = Activator.CreateInstance<T>();
                Dictionary<string, string> dbMappings = DBColumn(element);
                string columnName;
                foreach (var f in properties)
                {

                    if (!columnList.Contains(f.Name) && !dbMappings.ContainsKey(f.Name))
                        continue;
                    columnName = dbMappings.ContainsKey(f.Name) ? dbMappings[f.Name] : f.Name;
                    var o = (object)reader[columnName];

                    if (o.GetType() != typeof(DBNull)) f.SetValue(element, ChangeType(o, f.PropertyType), null);
                }
                yield return element;
            }

        }

        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t); ;
            }

            return Convert.ChangeType(value, t);
        }

        public static bool IsNullableType(this Type propertyType)
        {
            return propertyType.IsGenericType
                   && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsValueType(this Type propertyType)
        {
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(ValueType);
        }

        public static bool IsCollectionType(this Type propertyType)
        {
            return propertyType.IsGenericType && typeof(IEnumerable<>)
                       .MakeGenericType(propertyType.GetGenericArguments())
                       .IsAssignableFrom(propertyType);
        }

        public static Dictionary<string, string> DBColumn<T>(T item) where T : new()
        {
            Dictionary<string, string> dbMappings = new Dictionary<string, string>();
            var type = item.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                var columnMapping = attributes
                    .FirstOrDefault(a => a.GetType() == typeof(ColumnAttribute));
                if (columnMapping != null)
                {
                    dbMappings.Add(property.Name, ((ColumnAttribute)columnMapping).Name);
                }
            }
            return dbMappings;
        }
        public static List<string> GetProperties(this object obj)
        {
            //Getting Type of Generic Class Model
            Type tModelType = obj.GetType();

            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = tModelType.GetProperties();
            List<string> result = new List<string>();

            //Now we will loop in all properties one by one to get value
            foreach (PropertyInfo property in arrayPropertyInfos)
            {
                result.Add(property.Name);
            }

            return result;
        }
    }

    public class CustomTypeSqlQuery<T> where T : class
    {
        private readonly IMapper _mapper;

        public DatabaseFacade DatabaseFacade { get; set; }
        public string SqlQuery { get; set; }
        public object[] Parameters { get; set; }

        public CustomTypeSqlQuery()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.CreateMap<IDataRecord, T>();
            }).CreateMapper();
        }

        public async Task<List<T>> ToListAsync()
        {
            List<T> results = new List<T>();
            var conn = DatabaseFacade.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = SqlQuery;
                    command.Parameters.AddRange(Parameters);
                    command.CommandTimeout = command.Connection.ConnectionTimeout;

                    try
                    {
                        DbDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                            results = _mapper.Map<IDataReader, List<T>>(reader);
                        reader.Dispose();
                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return results;
        }

        public async Task<T> FirstOrDefaultAsync()
        {
            T result = null;
            var conn = DatabaseFacade.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = SqlQuery;
                    command.Parameters.AddRange(Parameters);
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        var results = _mapper.Map<IDataReader, IEnumerable<T>>(reader);
                        result = results.FirstOrDefault();
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public async Task<T> SingleAsync()
        {
            T result = null;
            IEnumerable<T> results = null;
            var conn = DatabaseFacade.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = SqlQuery;
                    command.Parameters.AddRange(Parameters);
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        results = _mapper.Map<IDataReader, IEnumerable<T>>(reader);
                    }
                    reader.Dispose();
                }

                // Argument validation elided
                if (results != null)
                    using (IEnumerator<T> iterator = results.GetEnumerator())
                    {
                        if (!iterator.MoveNext())
                        {
                            throw new InvalidOperationException("Sequence was empty");
                        }

                        result = iterator.Current;

                        if (iterator.MoveNext())
                        {
                            throw new InvalidOperationException("Sequence contained multiple elements");
                        }
                    }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }

    public class Enum<EnumType> where EnumType : struct, IConvertible
    {

        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static EnumType[] GetValues()
        {
            return (EnumType[])Enum.GetValues(typeof(EnumType));
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static EnumType Parse(string name)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), name);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static EnumType Parse(string name, bool ignoreCase)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), name, ignoreCase);
        }

        /// <summary>
        /// Converts the specified object with an integer value to an enumeration member.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static EnumType ToObject(object value)
        {
            return (EnumType)Enum.ToObject(typeof(EnumType), value);
        }
    }
    //MyEnum enumValue = (MyEnum)Enum.ToObject(typeof(MyEnum), 5);
}
