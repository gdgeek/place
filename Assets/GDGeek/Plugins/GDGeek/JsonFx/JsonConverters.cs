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
using System;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;
using System.Collections.Generic;

/** \file Some converters you can use with basic Unity built-in types. Supports Bounds, LayerMask and Vector2,3,4. */

	public class BoundsConverter : JsonConverter {
		public override bool CanConvert (Type type) {
			return type == typeof(Bounds);
		}
		
		public override object ReadJson ( Type objectType, Dictionary<string,object> values) {
			Bounds b = new Bounds();
			b.center = new Vector3(	CastFloat(values["cx"]),CastFloat(values["cy"]),CastFloat(values["cz"]));
			b.extents = new Vector3(CastFloat(values["ex"]),CastFloat(values["ey"]),CastFloat(values["ez"]));
			return b;
		}
		
		public override Dictionary<string,object> WriteJson (Type type, object value) {
			Bounds b = (Bounds)value;
			return new Dictionary<string, object>() {
				{"cx",b.center.x},
				{"cy",b.center.y},
				{"cz",b.center.z},
				{"ex",b.extents.x},
				{"ey",b.extents.y},
				{"ez",b.extents.z}
			};
		}
	}
	
	public class LayerMaskConverter : JsonConverter {
		public override bool CanConvert (Type type) {
			return type == typeof(LayerMask);
		}
		
		public override object ReadJson (Type type, Dictionary<string,object> values) {
			return (LayerMask)(int)values["value"];
		}
		
		public override Dictionary<string,object> WriteJson (Type type, object value) {
			return new Dictionary<string, object>() {{"value",((LayerMask)value).value}};
		}
	}
	
public class VectorConverter : JsonConverter
{
	public override bool CanConvert (Type type) {
		return type == typeof(Vector2) || type==typeof(Vector3)||type==typeof(Vector4);
	}
	
	public override object ReadJson (Type type, Dictionary<string,object> values) {
		if (type == typeof(Vector2)) {
			return new Vector2(CastFloat(values["x"]),CastFloat(values["y"]));
		} else if (type == typeof(Vector3)) {
			return new Vector3(CastFloat(values["x"]),CastFloat(values["y"]),CastFloat(values["z"]));
		} else if (type == typeof(Vector4)) {
			return new Vector4(CastFloat(values["x"]),CastFloat(values["y"]),CastFloat(values["z"]),CastFloat(values["w"]));
		} else {
			throw new System.NotImplementedException ("Can only read Vector2,3,4. Not objects of type "+type);
		}
	}
	
	public override Dictionary<string,object> WriteJson (Type type, object value) {
		if (type == typeof(Vector2)) {
			Vector2 v = (Vector2)value;
			return new Dictionary<string, object>() {
				{"x",v.x},
				{"y",v.y}
			};
		} else if (type == typeof(Vector3)) {
			Vector3 v = (Vector3)value;
			return new Dictionary<string, object>() {
				{"x",v.x},
				{"y",v.y},
				{"z",v.z}
			};
		} else if (type
		           == typeof(Vector4)) {
			Vector4 v = (Vector4)value;
			return new Dictionary<string, object>() {
				{"x",v.x},
				{"y",v.y},
				{"z",v.z},
				{"w",v.w}
			};
		}
		throw new System.NotImplementedException ("Can only write Vector2,3,4. Not objects of type "+type);
	}
}



public class ColorConverter : JsonConverter
{
	public override bool CanConvert (Type type) {
		return type == typeof(Color);
	}
	
	public override object ReadJson (Type type, Dictionary<string,object> values) {
		return new Color(CastFloat(values["r"])/255.0f,CastFloat(values["g"])/255.0f,CastFloat(values["b"])/255.0f,CastFloat(values["a"])/255.0f);

	}
	
	public override Dictionary<string,object> WriteJson (Type type, object value) {
		if (type
		           == typeof(Color)) {
					Color v = (Color)value;
			return new Dictionary<string, object>() {
				{"r",Mathf.RoundToInt(v.r*0xFF)},
				{"g",Mathf.RoundToInt(v.g*0xFF)},
				{"b",Mathf.RoundToInt(v.b*0xFF)},
				{"a",Mathf.RoundToInt(v.a*0xFF)}
			};
		}
		throw new System.NotImplementedException ("Can only write Color. Not objects of type "+type);
	}



}
