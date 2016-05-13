using System;

using System.Linq;

using System.Collections;

using System.Reflection;



#pragma warning disable CS0414 // Type or member is obsolete
namespace Aperture

{

    public class AotSafe

    {

        private static System.ComponentModel.ArrayConverter array_ = new System.ComponentModel.ArrayConverter(); 

//		private static System.ComponentModel.BaseNumberConverter baseNumber_ = new System.ComponentModel.BaseNumberConverter(); 
		private static System.ComponentModel.ByteConverter boolean_ = new System.ComponentModel.ByteConverter();  
		private static System.ComponentModel.BooleanConverter byte_ = new System.ComponentModel.BooleanConverter();  

		
		
		private static System.ComponentModel.CharConverter char_ = new System.ComponentModel.CharConverter(); 
		private static System.ComponentModel.CollectionConverter collection_ = new System.ComponentModel.CollectionConverter();  
//		private static System.ComponentModel.ComponentConverter component_ = new System.ComponentModel.ComponentConverter();  
		private static System.ComponentModel.CultureInfoConverter cultureInfo_ = new System.ComponentModel.CultureInfoConverter();  

		
		private static System.ComponentModel.DateTimeConverter dateTime_ = new System.ComponentModel.DateTimeConverter();  
		private static System.ComponentModel.DecimalConverter decimal_ = new System.ComponentModel.DecimalConverter();  
		private static System.ComponentModel.DoubleConverter double_ = new System.ComponentModel.DoubleConverter();  

		
		private static System.ComponentModel.ExpandableObjectConverter expandableObject_ = new System.ComponentModel.ExpandableObjectConverter();  


		private static System.ComponentModel.GuidConverter guidConverter_ = new System.ComponentModel.GuidConverter();  
		
		private static System.ComponentModel.Int16Converter int16_ = new System.ComponentModel.Int16Converter(); 
		private static System.ComponentModel.Int32Converter int32_ = new System.ComponentModel.Int32Converter();  
		private static System.ComponentModel.Int64Converter int64_ = new System.ComponentModel.Int64Converter();  

		
		private static System.ComponentModel.MultilineStringConverter multilineString_ = new System.ComponentModel.MultilineStringConverter();  
//		private static System.ComponentModel.NestedContainer nested_ = new System.ComponentModel.NestedContainer();  

		
//		private static System.ComponentModel.ReferenceConverter reference_ = new System.ComponentModel.ReferenceConverter();  

		
		private static System.ComponentModel.SByteConverter sbyte_ = new System.ComponentModel.SByteConverter(); 
		private static System.ComponentModel.SingleConverter single_ = new System.ComponentModel.SingleConverter();  
		private static System.ComponentModel.StringConverter string_ = new System.ComponentModel.StringConverter();  
		
		private static System.ComponentModel.TimeSpanConverter time_ = new System.ComponentModel.TimeSpanConverter();  
		private static System.ComponentModel.TypeConverter type_ = new System.ComponentModel.TypeConverter();  
		
		private static System.ComponentModel.UInt16Converter uint16_ = new System.ComponentModel.UInt16Converter();  
		private static System.ComponentModel.UInt32Converter uint32_ = new System.ComponentModel.UInt32Converter(); 
		private static System.ComponentModel.UInt64Converter uint64_ = new System.ComponentModel.UInt64Converter();

        public static void ForEach<T>(object enumerable, Action<T> action)
		{
			if (enumerable == null)

            {

                return;

            }

 

			Type listType = enumerable.GetType().GetInterfaces().First(x => !(x.IsGenericType) && x == typeof(IEnumerable));

            if (listType == null)

            {

                throw new ArgumentException("Object does not implement IEnumerable interface", "enumerable");

            }

 

            MethodInfo method = listType.GetMethod("GetEnumerator");

            if (method == null)

            {

                throw new InvalidOperationException("Failed to get 'GetEnumberator()' method info from IEnumerable type");

            }

 

            IEnumerator enumerator = null;

            try

            {

                enumerator = (IEnumerator)method.Invoke(enumerable, null);

                if (enumerator is IEnumerator)

                {

                        while (enumerator.MoveNext())

                        {

                            action((T)enumerator.Current);

                        }

                }

                else

                {

                    UnityEngine.Debug.Log(string.Format("{0}.GetEnumerator() returned '{1}' instead of IEnumerator.",

                        enumerable.ToString(),

                        enumerator.GetType().Name));

                }

            }

            finally

            {

                IDisposable disposable = enumerator as IDisposable;

                if (disposable != null)

                {

                    disposable.Dispose();

                }

            }

        }

    }

}

#pragma warning restore CS0414 // Type or member is obsolete