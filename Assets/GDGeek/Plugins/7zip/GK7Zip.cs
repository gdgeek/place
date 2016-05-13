using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class GK7Zip {
	
	public static string Compression(string text){
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();	
		byte[] array = Encoding.UTF8.GetBytes(text); 
		MemoryStream input = new MemoryStream (array);
		MemoryStream output = new MemoryStream ();

		coder.WriteCoderProperties(output);
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);
		coder.Code(input, output, input.Length, -1, null);
		output.Flush();
		string ret =  System.Convert.ToBase64String (output.GetBuffer ());
		output.Close();
		input.Close();
		return ret;
	}

	public static string GetPath(){
		return  Application.streamingAssetsPath + "/";
		string path = "";
		#if UNITY_EDITOR
		path = Application.dataPath +"/StreamingAssets"+"/";

		#elif UNITY_IPHONE
		path = Application.dataPath +"/Raw"+"/";

		#elif UNITY_ANDROID
		path = "jar:file://" + Application.dataPath + "!/assets/"+"/;

		#endif
		return path;
	}
	public  static string GetFileName (string key){
		return  GetPath () + key + ".zip";
	}
	public static void SetToFile(string key, string value){
		CompressToFile (value, GetFileName(key));
	}
	public static string GetFromFile(string key){
		return DecompressFromFile (GetFileName(key));
	}
	public static bool FileHas(string key){
		FileInfo file = new FileInfo( GetFileName(key));
		return file.Exists;
	}
	private static string DecompressFromFile(string filename)
	{
		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
	

		FileStream input = new FileStream(filename, FileMode.Open, FileAccess.Read);
		MemoryStream output = new MemoryStream ();
		// Read the decoder properties
		byte[] properties = new byte[5];
		input.Read(properties, 0, 5);

		// Read in the decompress file size.
		byte [] fileLengthBytes = new byte[8];
		input.Read(fileLengthBytes, 0, 8);
		long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

		// Decompress the file.
		coder.SetDecoderProperties(properties);
		coder.Code(input, output, input.Length, fileLength, null);

		output.Flush();

		string ret = System.Text.Encoding.UTF8.GetString(output.ToArray());
		output.Close();
		input.Close();
		return ret;

	}

	private static void CompressToFile(string value, string outFile)
	{
		SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();

		byte[] array = Encoding.UTF8.GetBytes(value); 
		MemoryStream input = new MemoryStream (array);

		FileStream output = new FileStream(outFile, FileMode.Create);

		// Write the encoder properties
		coder.WriteCoderProperties(output);

		// Write the decompressed file size.
		output.Write(BitConverter.GetBytes(input.Length), 0, 8);

		// Encode the file.
		coder.Code(input, output, input.Length, -1, null);
		output.Flush();
		output.Close();
		input.Close();
	}

	public static string Decompressed(string text){

		SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();


		byte[] array = System.Convert.FromBase64String(text); 
		MemoryStream input = new MemoryStream (array);
		MemoryStream output = new MemoryStream ();


		byte[] properties = new byte[5];
		input.Read(properties, 0, 5);

		// Read in the decompress file size.
		byte [] fileLengthBytes = new byte[8];
		input.Read(fileLengthBytes, 0, 8);
		long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

		coder.SetDecoderProperties(properties);
		coder.Code(input, output, input.Length, fileLength, null);

		output.Flush();

		string ret = System.Text.Encoding.UTF8.GetString(output.ToArray());
		output.Close();
		input.Close();
		return ret;
	}
	
}
