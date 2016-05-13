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


namespace GDGeek{
	public class WebPack
	{	
		private WWWForm form_ = null; 
		private string url_ = "";
		public WebPack(string url){
			url_ = url;
		}
		public void addBinaryData(string key, byte[] val){
			if(val != null){
				Debug.Log ("???" + key);
				form.AddBinaryData(key, val);
			}else{
				Debug.LogWarning("no value!");
			}
		}

		public void addField(string key, IList list){
			for(int i =0; i<list.Count; ++i){
				form.AddField(key+"[]", list[i].ToString());
			}
		}
		public void addField(string key, object[] list){
			foreach(object val in list){
				if(val != null && !string.IsNullOrEmpty(val.ToString())){
					form.AddField(key+"[]", val.ToString());
				}
			}
		}
		
		private WWWForm form{
			get{
				if(form_ == null){
					form_ = new WWWForm();
				}
				return form_;
			}
		}
		public void addField(string key, object val){
			
			
			if(val != null &&!string.IsNullOrEmpty(val.ToString())){
				form.AddField(key, val.ToString());
			}else{
				Debug.LogWarning("no value!");
			}
		}	

		public WWW www(){

			var headers = this.form.headers;
			headers["Accept-Encoding"] = "gzip";
			string debug = "";
			if (Debug.isDebugBuild) {
				debug = "&debug=1";
				this.addField ("debug", true);
			} else {
				
				this.addField ("debug", false);
			}



			return new WWW (url_, this.form);
			
		}
		
	};
}


