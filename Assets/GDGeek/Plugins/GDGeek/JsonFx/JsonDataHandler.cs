/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2015 GDGeek Software Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Pathfinding.Serialization.JsonFx;





public class JsonDataHandler{
	
	//public static JsonData.AffixConverter affixConverter_ = null;
	protected static ColorConverter color = new ColorConverter();
	protected static JsonReaderSettings rsettings_ = new JsonReaderSettings(); 
	protected static JsonWriterSettings wsettings_ = new JsonWriterSettings(); 
	protected static JsonWriterSettings settings = new JsonWriterSettings();
	protected static bool init_ = false;
	private static void init(){
		if (!init_) {
			
			rsettings_.AddTypeConverter(color);
			wsettings_.AddTypeConverter(color);
			init_ = true;		
			
			settings.PrettyPrint = false;
			settings.AddTypeConverter (color);
		}
	}
	public static T reader<T>(string json){
		init ();
		JsonReader reader = new JsonReader(json, rsettings_);
		return (T)reader.Deserialize(typeof(T)); 
	}
	
	public static string write<T>(T obj){
		

		
		init ();
		StringBuilder output = new StringBuilder();
		JsonWriter writer = new JsonWriter(output, settings);	
		writer.Write(obj);
		return output.ToString();
	}

} 
