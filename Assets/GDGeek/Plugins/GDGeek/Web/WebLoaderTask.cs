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
using Pathfinding.Serialization.JsonFx;
namespace GDGeek{
	public abstract class DataInfo{
		
		[JsonMember]
		public bool succeed = false;
		
		[JsonMember]
		public string message = "";
		
		[JsonMember]
		public double epoch = 0;
		
	}
	/*
	public interface DataInfoLoader<T>{
		T load (string json);

	}*/

	public class WebLoaderTask<T> : Task where T:DataInfo {
		public delegate void Succeed (T info);
		public delegate void Error (string msg);
		public event Succeed onSucceed;
		public event Error onError;
		private WebPack pack_ = null;
		public WebPack pack{
			get{
				return pack_;
			}
		}

		public WebLoaderTask(string url){
			pack_ = new WebPack (url);
			pack_.addField("a", "b");
			bool isOver = false;
			this.init = delegate {
				isOver = false;
				Task web = WebTaskFactory.GetInstance().create(pack, delegate(string json) {
					if(onSucceed != null){
						T info = JsonDataHandler.reader<T>(json);
						if(WebTimestamp.GetInstance() != null){
							WebTimestamp.GetInstance().synchro(info.epoch);
						}
						onSucceed(info);
					}
				},delegate(string msg) {
					if(onError != null){
						onError(msg);
					}
				});
				TaskManager.PushBack (web, delegate{
					isOver = true;
				});
				this.isOver = delegate {
					return isOver;
				};
				TaskManager.Run (web);
			};
		}
		

	}
}
