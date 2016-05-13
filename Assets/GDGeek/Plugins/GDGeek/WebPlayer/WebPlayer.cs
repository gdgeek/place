using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace GDGeek{
	public class WebPlayer : MonoBehaviour {
		public Text _log;
		public static Dictionary<string, string> _parames = new Dictionary<string, string>();
		public void parame(string text){
			string[] s = text.Split(new char[] { '=' });
			if (s.Length == 2) {
					_parames [s[0]] = s[1];
			}
		}

		private static void Register(){
			
			Application.ExternalEval (
				"function GDGeekSendMessage(g, f, p){" +
					"var es = document.getElementsByTagName('embed');" +
				"for(var i = 0; i<es.length; ++i){" +
				"if(es[i].src.indexOf('unity') && es[i].SendMessage){es[i].SendMessage(g, f, p);}" +
					"}" +
				"}" 
				);
			Application.ExternalEval (
				"function parse_url(){" +
				"var url=window.location.href;" +
				"var pattern = /(\\w+)=(\\w+)/ig;" +
				"var parames = {};" +
				"url.replace(pattern, function(a, b, c)" +
				"{parames[b] = c;}" + 
				");" + 
				"return parames;" + 
				"}"
				);
			Application.ExternalEval ("var u1 = parse_url();for(var key in u1){GDGeekSendMessage('GDGeek', 'parame', key+'='+u1[key]);}");
			Application.ExternalEval ("if(GDGeekCallBack){GDGeekCallBack('start')}");


		}
		public static void Close(){
			Application.ExternalEval (
				"var oBody = document.getElementsByTagName('BODY').item(0);" +
				"var div = document.getElementById('gdgeek_window');"+
				"if(oBody && div){oBody.removeChild(div);};"
				);
		}
		public static void Share(string text, string url, string cbOk = "", string cbCancel = ""){
			
			
			Application.ExternalEval (
				"var oBody = document.getElementsByTagName('BODY').item(0);" +
				"var div= document.createElement('div');" +
				"div.height = '280';" +
				"div.width = '440';" +
				"div.hspace = '0';" +
				"div.vspace = '0';" +
				"div.style.position ='absolute';" +
				"div.style.top ='50%';" +
				"div.style.left ='50%';" +
				"div.style.margin ='-100px 0 0 -100px';" +
				"div.style.width ='200px';" +
				"div.style.height ='90px';" +
				"div.style.display ='';" +
				"div.style.border ='1px solid red';" +
				"div.id = 'gdgeek_window';  " +
				"div.style.background= 'yellow';" +
				"div.marginwidth = '0';" +
				"div.marginheight = '0';" +
				"var share = document.createElement('A');" +
				"share.setAttribute('href','"+url+"');" +
				"share.setAttribute('target','_blank');" +
				"share.setAttribute('onclick','"+cbOk+"oBody.removeChild(div);');   " +
				"share.appendChild(document.createTextNode('"+text+"'));" +
				"div.appendChild(document.createElement('BR'));" +
				"div.appendChild(share);" +
				"div.appendChild(document.createElement('BR'));" +
				"div.appendChild(document.createElement('BR'));" +
				"var close = document.createElement('A');" +
				"close.setAttribute('href','#'); " +
				"close.setAttribute('onclick','"+cbCancel+"oBody.removeChild(div);');   " +
				"close.appendChild(document.createTextNode('关闭'));" +
				"div.appendChild(close);" +
				"oBody.appendChild(div);"
				);


		}
		//public void log(string msg){
		//	Debug.Log (msg);		
		//}
		void Start () {
			WebPlayer.Register ();
		}
		

	}
}