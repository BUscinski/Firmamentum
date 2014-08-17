using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Renderer))]
public class DisplayColor : MonoBehaviour {
	
	public DeviceOrEmulator devOrEmu;
	public GameObject PointCloudParent;
	private Kinect.KinectInterface kinect;
	private Color pixelColor;
//	public GameObject fuck;
	public int length;
	public int width;
	private int numPixels;
	private Texture2D tex;
	public GameObject pointCloudPixel;
	public GameObject positionHolder;
	private GameObject[,] thePixels;
	private GameObject[,] originalPositions;
	private Vector3 middlePoint;
	public int pointCloudMultiplier;
	
	// Use this for initialization

//	void Awake()
//	{
//		kinect = devOrEmu.getKinect();
//		//		tex = new Texture2D(640,480,TextureFormat.ARGB32,false);
//		tex = new Texture2D(80,60,TextureFormat.ARGB32,false);
//		//		tex = new Texture2D(160, 120,TextureFormat.ARGB32, false);
//		//		tex = new Texture2D(80, 60,TextureFormat.ARGB32, false);
//		renderer.material.mainTexture = tex;
//		thePixels = new GameObject[length, width];
//		originalPositions = new Transform[length, width];
//		numPixels = length * width;
//		Debug.Log ("NumPixels: " + numPixels);
//		
//		for(int i = 0; i < length; i++)
//		{
//			for(int x = 0; x < width; x++)
//			{
//				thePixels[i,x] = (GameObject)GameObject.Instantiate (pointCloudPixel, new Vector3((float)-i/4, (float)-x/4, 0), Quaternion.identity);
//				originalPositions[i,x] = thePixels[i,x].transform;
//				//Add Particle System?
//			}
//		}
//
//	}

	void Start () {
		kinect = devOrEmu.getKinect();
	//	tex = new Texture2D(640,480,TextureFormat.ARGB32,false);
		tex = new Texture2D(80,60,TextureFormat.ARGB32,false);
//		tex = new Texture2D(160, 120,TextureFormat.ARGB32, false);
//		tex = new Texture2D(80, 60,TextureFormat.ARGB32, false);
//		renderer.material.mainTexture = tex;
		thePixels = new GameObject[length, width];
		originalPositions = new GameObject[length, width];
		numPixels = length * width;
		Debug.Log ("NumPixels: " + numPixels);

		for(int i = 0; i < length; i++)
		{
			for(int x = 0; x < width; x++)
			{
				thePixels[i,x] = (GameObject)GameObject.Instantiate (pointCloudPixel, new Vector3((float)-i/4, (float)-x/4, 0), Quaternion.identity);
				originalPositions[i,x] = (GameObject)GameObject.Instantiate (positionHolder, new Vector3((float)-i/4, (float)-x/4, 0), Quaternion.identity);
				thePixels[i,x].transform.parent = PointCloudParent.transform;
				originalPositions[i,x].transform.parent = PointCloudParent.transform;

				if(x == width/2 && i == length/2)
				{
					middlePoint = thePixels[i,x].transform.position;
//					middlePoint.transform.position.x += 0.25f;
				}
				//Add Particle System?
			}
		}
		PointCloudParent.transform.Rotate (new Vector3(0,0,270));
	}
	
	// Update is called once per frame
	void Update () {
		if (kinect.pollColor())
		{

			//tex.SetPixels32(kinect.getColor());
			tex.SetPixels32(mipmapImg(kinect.getColor(),640,480));

//			mipmapImg (kinect.getColor(), 640, 480);
			//tex.SetPixels32 (mipmapImg (kinect.getColor(), 320, 240));
//			tex.SetPixels32 (mipmapImg (kinect.getColor (), 320, 240));
//			tex.SetPixels32 (mipmapImg (kinect.getColor (), 80, 60));
			tex.Apply(false);
			if(Input.GetKey (KeyCode.I))
			{
				PushBody();
			}
			if(Input.GetKey (KeyCode.K))
			{
				PullBody();
			}
//			UpdatePixels();
		//	pixelColor = tex.GetPixel (160, 120);
		//	pixelColor.a = 255;
		}
	}
	public void PushBody()
	{
		for(int i = 0; i < length; i ++)
		{
			for(int x = 0; x < width; x++)
			{
				Vector3 pushDirection = (thePixels[i,x].transform.position - middlePoint).normalized;
				pushDirection = pushDirection * 3;
				if((thePixels[i,x].transform.position - originalPositions[i,x].transform.position).magnitude < 20.0f){
					thePixels[i, x].rigidbody.AddForce (pushDirection);
				}
				else{
					thePixels[i,x].rigidbody.velocity = Vector3.zero;
				}
			}
		}
	}

	public void PullBody()
	{
		for(int i = 0; i < length; i++)
		{
			for(int x = 0; x < width; x++)
			{
				Vector3 pullDirection = (middlePoint - thePixels[i,x].transform.position).normalized;
				pullDirection = pullDirection * 2.5f;
				if((thePixels[i,x].transform.position - originalPositions[i,x].transform.position).magnitude > 0.1f)
				{
					thePixels[i,x].rigidbody.AddForce (pullDirection);
				}
				else
				{
					thePixels[i,x].rigidbody.velocity = Vector3.zero;
				}
			}
		}
	}
	public void StopBody()
	{
		for(int i = 0; i < length; i++)
		{
			for(int x = 0; x < width; x++)
			{
				thePixels[i,x].rigidbody.velocity = Vector3.zero;
			}
		}
	}
	private Color32[] mipmapImg(Color32[] src, int width, int height)
	{
		int newWidth = width / 8;
		int newHeight = height / 8;
//		int newWidth = 80;
//		int newHeight = 60;
		Color32[] dst = new Color32[newWidth * newHeight];
		for(int yy = 0; yy < newHeight; yy++)
		{
			for(int xx = 0; xx < newWidth; xx++)
			{
				int TLidx = (xx * 8) + yy * 8 * width;
				int TRidx = (xx * 8 + 1) + yy * width * 2;
				int BLidx = (xx * 8) + (yy * 8 + 1) * width;
				int BRidx = (xx * 8 + 1) + (yy * 8 + 1) * width;



//				int TLidx = (xx * 2) + yy * 2 * width;
//				int TRidx = (xx * 2 + 1) + yy * width * 2;
//				int BLidx = (xx * 2) + (yy * 2 + 1) * width;
//				int BRidx = (xx * 2 + 1) + (yy * 2 + 1) * width;
				dst[xx + yy * newWidth] = Color32.Lerp(Color32.Lerp(src[BLidx],src[BRidx],.5f),
				                                       Color32.Lerp(src[TLidx],src[TRidx],.5F),.5F);
				thePixels[xx, yy].renderer.material.color = dst[xx + yy * newWidth];


		//		thePixels[xx,yy].renderer.material.color = Color32.Lerp (Color32.Lerp (src[BLidx],src[BRidx],.5F),
		//		                                                         Color32.Lerp(src[TLidx],src[TRidx],.5F),.5F);
			}
		}
		return dst;
	}
//	private Color32[] mipmapImg(Color32[] src, int width, int height)
//			{
//				int newWidth = width / 2;
//				int newHeight = height / 2;
//		//		int newWidth = 80;
//		//		int newHeight = 60;
//				Color32[] dst = new Color32[newWidth * newHeight];
//				for(int yy = 0; yy < newHeight; yy++)
//				{
//					for(int xx = 0; xx < newWidth; xx++)
//					{
//						int TLidx = (xx * 2) + yy * 2 * width;
//						int TRidx = (xx * 2 + 1) + yy * width * 2;
//						int BLidx = (xx * 2) + (yy * 2 + 1) * width;
//						int BRidx = (xx * 2 + 1) + (yy * 2 + 1) * width;
//		
//		
//		
//		//				int TLidx = (xx * 2) + yy * 2 * width;
//		//				int TRidx = (xx * 2 + 1) + yy * width * 2;
//		//				int BLidx = (xx * 2) + (yy * 2 + 1) * width;
//		//				int BRidx = (xx * 2 + 1) + (yy * 2 + 1) * width;
//						dst[xx + yy * newWidth] = Color32.Lerp(Color32.Lerp(src[BLidx],src[BRidx],.5f),
//						                                       Color32.Lerp(src[TLidx],src[TRidx],.5F),.5F);
//						thePixels[xx, yy].renderer.material.color = dst[xx + yy * newWidth];
//		
//		
//				//		thePixels[xx,yy].renderer.material.color = Color32.Lerp (Color32.Lerp (src[BLidx],src[BRidx],.5F),
//				//		                                                         Color32.Lerp(src[TLidx],src[TRidx],.5F),.5F);
//					}
//				}
//				return dst;
//			}



	public Texture2D GetCurrentTexture()
	{
		Texture2D t = new Texture2D(320,240,TextureFormat.ARGB32,false);
		t.SetPixels32(mipmapImg(kinect.getColor(),640,480));
		t.Apply(false);
		return t;
	}
	public void SetRendererOn(int x, int y)
	{
	//	Debug.Log ("Set Renderer On " + x + " " + y);
		thePixels[x,y].renderer.enabled = true;
	}
	public void SetRendererOff(int x, int y)
	{
	//	Debug.Log ("Set Renderer Off " + x + " " + y);
		thePixels[x,y].renderer.enabled = false;
	}
	public void SetDepth(int x, int y, float depthValue)
	{
		float newDepthValue = (depthValue * pointCloudMultiplier);
	//	Debug.Log (newDepthValue);
		thePixels[x,y].transform.position = new Vector3(thePixels[x,y].transform.position.x, thePixels[x,y].transform.position.y, -newDepthValue);
	}
}
