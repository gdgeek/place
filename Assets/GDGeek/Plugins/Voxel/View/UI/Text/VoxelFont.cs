using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GDGeek{
	public class VoxelFont: MonoBehaviour {
		public Font _font;
		public Renderer _renderer = null;
		public char _text;
		public Color _fontColor = Color.black;
		public Color _bgColor = Color.white;
		private float time_ = 0.0f;
		public float _flicker = -1f;
		private bool show_ = true;
		public void Update(){
			if (_flicker > 0.0f) {
				time_+= Time.deltaTime;
				if(time_ > _flicker){
					time_ -= _flicker;
					if(show_){
						show_ = false;
						_renderer.material.SetColor ("_FontColor", _bgColor);
					}else{
						show_ = true;
						_renderer.material.SetColor ("_FontColor", _fontColor);
					}
				} 


			
			}
		}
		private void rebuilt(){
			CharacterInfo ci;
			_font.GetCharacterInfo (_text, out ci);
			_renderer.sharedMaterial.SetInt ("_Width", ci.maxX - 	ci.minX);
			_renderer.sharedMaterial.SetInt ("_Height", ci.maxY - ci.minY);
			_renderer.sharedMaterial.SetVector ("_Size", new Vector4 (ci.maxX, ci.minX, ci.maxY, ci.minY) );
			_renderer.sharedMaterial.SetVector ("_FontUV", new Vector4 (ci.uvTopLeft.x, ci.uvTopLeft.y, ci.uvTopRight.x, ci.uvTopRight.y));
			_renderer.sharedMaterial.SetVector ("_FontUV2", new Vector4 (ci.uvBottomLeft.x, ci.uvBottomLeft.y,  ci.uvBottomRight.x, ci.uvBottomRight.y));
			_renderer.sharedMaterial.SetColor ("_FontColor", _fontColor);
			_renderer.sharedMaterial.SetColor ("_Color", _bgColor);
		}
		public void setup(char text, Color font, Color bg){
			_text = text;
			_fontColor = font;
			_bgColor = bg;
			rebuilt ();
		}
		public void close(){
			this.transform.localEulerAngles = new Vector3 (0, 180, 0);		
		}
		public void open(){
			
			this.transform.localEulerAngles = new Vector3 (0, 0, 0);	
		}
		public Task closeTask(){
			if (_text == ' '||_fontColor == _bgColor) {
				Task task = new Task();
				TaskManager.PushBack(task, delegate{close();});
				return task;
			}
			TweenTask tt = new TweenTask (delegate {
				
				this.transform.localEulerAngles = new Vector3 (0, -1, 0);	
				return TweenRotation.Begin( this.gameObject, 0.3f, Quaternion.Euler(new Vector3(0,180,0)));		
			});
			return tt;
			
		}

		public Task openTask(){
			if (_text == ' '||_fontColor == _bgColor) {
				Task task = new Task();
				TaskManager.PushBack(task, delegate{open();});
				return task;
			}
			TweenTask tt = new TweenTask (delegate {
				
				this.transform.localEulerAngles = new Vector3 (0, 179, 0);	
				return TweenRotation.Begin( this.gameObject, 0.3f, Quaternion.Euler(new Vector3(0,0,0)));		
			});
			return tt;

		}
		void Awake () {
			
			_renderer.material.SetTexture("_MainTex",_font.material.mainTexture);
			_font.RequestCharactersInTexture(_text+"");
			Font.textureRebuilt += delegate(Font obj) {
				if(obj == _font){
					rebuilt();
				}
			};
			rebuilt ();

		}

	}
}