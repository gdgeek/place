using UnityEngine;
using System.Collections;
namespace GDGeek{
	//[ExecuteInEditMode]
	public class VoxelFontManager : MonoBehaviour {


		public bool _doIt = true; 
		public string _text = "";
		public Font _font = null;
		public VoxelFont[] _fonts;
		public Color _bgColor;
		public Color _fontColor;
		public Task _test(){
			TaskList tl = new TaskList ();
			TaskWait tw = new TaskWait ();
			tw.setAllTime (2.0f);
			tl.push (tw);
			
			
			Task open = this.openTask ("B");
			TaskManager.PushFront (open, delegate {
				write ("从这两组图中选出你认为最");
			});

			tl.push (open);
			tl.push (new TaskWait(3.0f));
			
			tl.push (next ("八哥为什么关注了我司的!>"));
			
			tl.push (new TaskWait(3.0f));
			
			tl.push (next ("我司的八哥为什么关注了!>"));
			
			
			tl.push (new TaskWait(3.0f));
			
			tl.push (closeTask ());
			return tl;
		}
		void Start(){
			//TaskManager.Run (test());
		}
		public Task next(string text){
			TaskSet ts = new TaskSet ();
			TaskList tl = new TaskList ();

			tl.push (closeCharTask ());
			tl.push (openCharTask (text));
			ts.push (tl);
			ts.push (shake());
			return ts;
		}
		void Update(){
			if (_doIt) {
				_doIt = false;
				this._fonts = this.gameObject.GetComponentsInChildren<VoxelFont>();
			}
		}
		private void closeChar(){
			for (int i =0; i<_fonts.Length; ++i) {
				_fonts[i].close();			
			}
		}

	
		private void openChar(){
			for (int i =0; i<_fonts.Length; ++i) {
				_fonts[i].open();			
			}
		}
		private void write(string text){

			_font.RequestCharactersInTexture (text);

			int i = 0;
			for (; i<text.Length; ++i) {
				_fonts[i].setup(text[i], _fontColor, _bgColor);
			}
			for(; i<_fonts.Length; ++i){
				_fonts[i].setup(' ', _bgColor, _bgColor);
			}


		}
		private Task closeCharTask(){
			TaskSet ts = new TaskSet ();
			
			for (int i =0; i<_fonts.Length; ++i) {

				ts.push (new TaskPack(_fonts[i].closeTask));

			}
			return ts;
		}
		public Task openCharTask (string text)
		{
			TaskSet ts = new TaskSet ();

			for (int i =0; i<_fonts.Length; ++i) {
			
				TaskList tl = new TaskList();
				if(i < text.Length){
					tl.push (new TaskWait(0.1f * i));
				}
				tl.push (new TaskPack(_fonts[i].openTask));
				ts.push(tl);
			}
			TaskManager.PushFront (ts, delegate{
				this.write(text);
			});
			return ts;
		}
		public Task shake(){
			TweenTask tt = new TweenTask (delegate {
				Tween tween = TweenShake.Begin(this.gameObject, 0.3f, Quaternion.Euler(new Vector3(10, 0, 0)));
				return tween;
			});
			return tt;
		}
		public Task openTask(string text){
			//TaskSet ts = new TaskSet ();
			TweenTask task = new TweenTask (delegate {
				this.gameObject.transform.localScale = Vector3.zero;
				Tween tween = TweenScale.Begin(this.gameObject, 0.3f, Vector3.one);
				tween.method = Tween.Method.BounceIn;
				return tween;
			});
			TaskManager.PushFront (task, delegate {
				this.write(text);
				this.open();
			});

			return task;
		}
		public Task closeTask(){
			Task task = new Task ();
			TaskManager.PushBack (task, delegate {
				this.transform.localScale = Vector3.zero;
						});
			return task;
		}
		
		public void close ()
		{
			this.gameObject.SetActive (false);
		}

		
		public void open ()
		{
			this.gameObject.SetActive (true);
		}
	}
}