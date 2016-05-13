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

namespace GDGeek{
	/*
	public class WebTask : GDGeek.Task{
		

		protected bool over_ = false;

		public bool over{
			get{
				return over_;
			}
			set{
				over_ = value;
			}
		}


		public void doOver(){
			this.over_ = true;
		}
		protected string url_ = "http://ezdoing.com";

		public void setup(string url){
			url_ = url;
			
		}
		public WebTask(){
			this.init = delegate {
				this.over = false;  
				WebTaskFactory.GetInstance().link(this);
			};
			this.isOver = delegate{
				return over_;
			};

		}
		
		private WebPack pack_  = new WebPack("asdf");
		public WebPack pack{
			get{
				return pack_;
			}

		}


		public delegate void Reset();
		public Reset reset = delegate(){};
		

		public WWW www(){
			WWW www = this.pack.www(this.url_) as WWW;
			return www;
		}
		public void handle(WWW www){
			
			if(www.error != null) {
				handleError(www.error);
				Debug.Log(url_ +":"+www.error);
				return;
			}
			var text = "";
			if(www.responseHeaders.ContainsKey("CONTENT-ENCODING") && www.responseHeaders["CONTENT-ENCODING"] == "gzip")
			{
				Debug.Log("a zip");
				Debug.Log(www.text);
	#if UNITY_STANDALONE_WIN
				Debug.Log("not iphone");
				text = JsonData.GZip.DeCompress(www.bytes);  
	#else
				text = www.text;
	#endif
			}else{
				
				Debug.Log("no zip");
				text = www.text;
			} 
			
			
			Debug.Log(url_); 
			handleResult(text); 
			
		}
		
		
		protected void handleError(string text){
			
			Debug.LogError(text);
		}
		protected void handleResult(string text){
			Debug.Log(text);
		}
		
	}*/
}

