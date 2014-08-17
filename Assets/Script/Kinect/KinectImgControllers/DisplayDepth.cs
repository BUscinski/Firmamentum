using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Renderer))]
public class DisplayDepth : MonoBehaviour {
	public DisplayColor theColor;
	public DepthWrapper dw;
	private int max;
	private int min;
	
	private Texture2D tex;
	// Use this for initialization
	void Start () {
//		tex = new Texture2D(160,120,TextureFormat.ARGB32,false);
//		tex = new Texture2D(320,240,TextureFormat.ARGB32, false);
		tex = new Texture2D(80,60,TextureFormat.ARGB32,false);
//		renderer.material.mainTexture = tex;
		min = 1000000;
		max = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (dw.pollDepth())
		{
			tex.SetPixels32(convertDepthToColor(dw.depthImg));
			//	tex.SetPixels32(convertPlayersToCutout(dw.segmentations));
			tex.Apply(false);
		}
	}
	private Color32[] convertDepthToColor(short[] depthBuf)
	{
		//320x260
//		Debug.Log (depthBuf.Length);
		int width = 320;
		int height = 240;
		int newWidth = 80;
		int newHeight = 60;
		short[] newDepthBuf = new short[newWidth * newHeight];
		//		int newWidth = 80;
		//		int newHeight = 60;
		Color32[] img = new Color32[newWidth * newHeight];
		for (int yy = 0; yy < newHeight; yy++){
			for(int xx = 0; xx < newWidth; xx++)
			{
				int TLidx = (xx * 4) + yy * 4 * width;
				int TRidx = (xx * 4 + 1) + yy * width * 2;
				int BLidx = (xx * 4) + (yy * 4 + 1) * width;
				int BRidx = (xx * 4 + 1) + (yy * 4 + 1) * width;
				int temp = xx + (yy * newWidth);
				newDepthBuf[temp] = (short) depthBuf[TLidx]; //((depthBuf[TLidx] + depthBuf[TRidx] + depthBuf[BLidx] + depthBuf[BRidx])/4);

				img[temp].r = (byte)(newDepthBuf[temp] / 32);
				img[temp].g = (byte)(newDepthBuf[temp] / 32);
				img[temp].b = (byte)(newDepthBuf[temp] / 32);
				if(newDepthBuf[temp]>max){
					max = newDepthBuf[temp];
				}
				if(newDepthBuf[temp]<min){
					if(newDepthBuf[temp] == 0){
						min = 1;
					}
					else{
						min = newDepthBuf[temp];
					}
				}

				if(newDepthBuf[temp] == 0){
				//	theColor.SetRendererOff (xx, yy);
				}
				else
				{
					theColor.SetRendererOn (xx, yy);
				}
				float DepthValue = (float)newDepthBuf[temp]/(max - min);
				theColor.SetDepth (xx, yy, DepthValue);

			}
		}



		return img;
	}
//	private Color32[] convertDepthToColor(short[] depthBuf)
//	{
//		int numPixelsOn = 76800;
//		Color32[] img = new Color32[depthBuf.Length];
//		for (int pix = 0; pix < depthBuf.Length; pix++)
//		{
//			img[pix].r = (byte)(depthBuf[pix] / 32);
//			img[pix].g = (byte)(depthBuf[pix] / 32);
//			img[pix].b = (byte)(depthBuf[pix] / 32);
//			if(depthBuf[pix] < 25)
//			{
//				theColor.SetRendererOff (pix/320, pix%240);
//				numPixelsOn--;
//			}
//			Debug.Log ("DIS HOW MANY PIXELZ" + numPixelsOn);
//		}
//
//		return img;
//	}


	private Color32[] convertPlayersToCutout(bool[,] players)
	{
//		Color32[] img = new Color32[320*240];
//		Color32[] img = new Color32[160*120];
		Color32[] img = new Color32[80*60];
		for (int pix = 0; pix < 80*60; pix++)
		{
			if(players[0,pix]|players[1,pix]|players[2,pix]|players[3,pix]|players[4,pix]|players[5,pix])
			{
				img[pix].a = (byte)255;
			} else {
				img[pix].a = (byte)0;
			}
		}
		return img;
	}
}
