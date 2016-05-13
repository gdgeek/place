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
	public class WebTaskFactory : MonoBehaviour{
		public delegate void Callback ();
		public delegate void Handler (string s);
		public delegate void BytesHandler (byte[] b);
		private static WebTaskFactory instance_ = null;

		public void Awake(){
			WebTaskFactory.instance_ = this;
		}


		public static WebTaskFactory GetInstance(){
			return WebTaskFactory.instance_;
		}
	
		private IEnumerator linkIt(WWW www, Handler succeed, Handler error, Callback doOver){
			
			yield return www;
			doHandle(www, succeed, error);
			doOver();
			
		}

		private IEnumerator linkIt(WWW www, BytesHandler succeed, Handler error, Callback doOver){
			
			yield return www;
			doHandle(www, succeed, error);
			doOver();
			
		}


		public Task create(WebPack pack, BytesHandler succeed, Handler error){
			WWW www = pack.www() as WWW;
			Task task = new Task ();
			bool over = false;
			task.init = delegate {
				over = false;  
				StartCoroutine(WebTaskFactory.GetInstance().linkIt(www, succeed, error, delegate{
					over = true;
				}));
			};
			
			task.isOver = delegate{
				return over;
			};
			return task;
			
		}


		public Task create(WebPack pack, Handler succeed, Handler error){
			WWW www = pack.www() as WWW;
			Task task = new Task ();
			bool over = false;
			task.init = delegate {
				over = false;  
				StartCoroutine(WebTaskFactory.GetInstance().linkIt(www, succeed, error, delegate{
					over = true;
				}));
			};

			task.isOver = delegate{
				return over;
			};
			return task;

		}
		public void doHandle(WWW www, BytesHandler succeed, Handler error){
			
			if(www.error != null) {
				error(www.error);
				Debug.Log(":"+www.error);
				return;
			}
			byte[] bytes = null;
			if(www.responseHeaders.ContainsKey("CONTENT-ENCODING") && www.responseHeaders["CONTENT-ENCODING"] == "gzip")
			{
				Debug.Log("a zip");
				Debug.Log(www.bytes.Length);
				//www.bytes
				#if UNITY_STANDALONE_WIN
				Debug.Log("not iphone");
				//text = JsonData.GZip.DeCompress(www.bytes);  
				#else
				bytes = www.bytes;
				#endif
			}else{
				
				Debug.Log("no zip" + www.bytes.Length);
				bytes = www.bytes;
			} 
			
			
			succeed(bytes); 
			
		}


		public void doHandle(WWW www, Handler succeed, Handler error){
			
			if(www.error != null) {
				error(www.error);
				Debug.Log(":"+www.error);
				return;
			}
			var text = "";
			if(www.responseHeaders.ContainsKey("CONTENT-ENCODING") && www.responseHeaders["CONTENT-ENCODING"] == "gzip")
			{
				Debug.Log("a zip");
				Debug.Log(www.text);
				//www.bytes
				#if UNITY_STANDALONE_WIN
				Debug.Log("not iphone");
				//text = JsonData.GZip.DeCompress(www.bytes);  
				#else
				text = www.text;
				#endif
			}else{
				Debug.Log("no zip" + www.text);
				text = www.text;
			} 
			succeed(text); 
			
		}
	
	}
}