using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Renderer))]
public class DisplayColor : MonoBehaviour {
	
	public GameObject KinectPrefab;
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
	public Transform middlePoint;
	public int pointCloudMultiplier;
	private float PointCloudSlowDistance;
	
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
	void Awake()
	{
		if(GameObject.Find ("KinectPrefab(Clone)") == null)
		{
			GameObject.Instantiate (KinectPrefab);
		}
	}


	void Start () {
		devOrEmu = GameObject.Find ("KinectPrefab(Clone)").GetComponent<DeviceOrEmulator>();
		PointCloudSlowDistance = 6.0f;
		kinect = devOrEmu.getKinect();
	//	tex = new Texture2D(640,480,TextureFormat.ARGB32,false);
		tex = new Texture2D(80,60,TextureFormat.ARGB32,false);
//		tex = new Texture2D(160, 120,TextureFormat.ARGB32, false);
//		tex = new Texture2D(80, 60,TextureFormat.ARGB32, false);
//		renderer.material.mainTexture = tex;
		thePixels = new GameObject[length, width];
		originalPositions = new GameObject[length, width];
		numPixels = length * width;
		//int middlePointx = 0;
		//int middlePointy = 0;
		for(int i = 0; i < length; i++)
		{
			for(int x = 0; x < width; x++)
			{
				thePixels[i,x] = (GameObject)GameObject.Instantiate (pointCloudPixel, new Vector3((float)-i/4, (float)-x/4, 0), Quaternion.identity);
				originalPositions[i,x] = (GameObject)GameObject.Instantiate (positionHolder, new Vector3((float)-i/4, (float)-x/4, 0), Quaternion.identity);
				thePixels[i,x].transform.parent = PointCloudParent.transform;
				originalPositions[i,x].transform.parent = PointCloudParent.transform;
				thePixels[i,x].renderer.material.color = Color.white;

		//		if(x == width/2 && i == length/2)
		//		{
		//			middlePointx = x;
		//			middlePointy = i;
		//		}
				//Add Particle System?
			}
		}
		PointCloudParent.transform.Rotate (new Vector3(0,0,90));
		//middlePoint = thePixels[middlePointx, middlePointy].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (kinect.pollColor())
		{

			//tex.SetPixels32(kinect.getColor());
			//tex.SetPixels32(mipmapImg(kinect.getColor(),640,480));

//			mipmapImg (kinect.getColor(), 640, 480);
			//tex.SetPixels32 (mipmapImg (kinect.getColor(), 320, 240));
//			tex.SetPixels32 (mipmapImg (kinect.getColor (), 320, 240));
//			tex.SetPixels32 (mipmapImg (kinect.getColor (), 80, 60));
		//	tex.Apply(false);
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
				Vector3 pushDirection = (thePixels[i,x].transform.position - middlePoint.position).normalized;
				pushDirection = pushDirection * 3;
				if(((thePixels[i,x].transform.position - originalPositions[i,x].transform.position).magnitude < 50.0f) && thePixels[i,x].rigidbody.velocity.magnitude <= 4.0f){
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
				Vector3 pullDirection = (originalPositions[i,x].transform.position - thePixels[i,x].transform.position).normalized;
			//	pullDirection = pullDirection * 2.5f;
	//			pullDirection = pullDirection 
				if((thePixels[i,x].transform.position - originalPositions[i,x].transform.position).magnitude < 0.1f)
				{
					thePixels[i,x].rigidbody.velocity = Vector3.zero;
				}
				else //if(thePixels[i,x].rigidbody.velocity.magnitude <= 4.0f)
				{
					thePixels[i,x].rigidbody.AddForce (pullDirection * 2.5f);
					thePixels[i,x].rigidbody.velocity = Vector3.ClampMagnitude(thePixels[i,x].rigidbody.velocity, (thePixels[i,x].transform.position - originalPositions[i,x].transform.position).magnitude);
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
//		for(int yy = 0; yy < newHeight; yy++)
//		{
//			for(int xx = 0; xx < newWidth; xx++)
//			{
//				int TLidx = (xx * 8) + yy * 8 * width;
//				int TRidx = (xx * 8 + 1) + yy * width * 2;
//				int BLidx = (xx * 8) + (yy * 8 + 1) * width;
//				int BRidx = (xx * 8 + 1) + (yy * 8 + 1) * width;
//
//
//				int TLidx = (xx * 2) + yy * 2 * width;
//				int TRidx = (xx * 2 + 1) + yy * width * 2;
//				int BLidx = (xx * 2) + (yy * 2 + 1) * width;
//				int BRidx = (xx * 2 + 1) + (yy * 2 + 1) * width;
				//dst[xx + yy * newWidth] = Color32.Lerp(Color32.Lerp(src[BLidx],src[BRidx],.5f),
				//                                       Color32.Lerp(src[TLidx],src[TRidx],.5F),.5F);
			//	thePixels[xx, yy].renderer.material.color = dst[xx + yy * newWidth];

		//		thePixels[xx,yy].renderer.material.color = Color32.Lerp (Color32.Lerp (src[BLidx],src[BRidx],.5F),
		//		                                                         Color32.Lerp(src[TLidx],src[TRidx],.5F),.5F);
//			}
//		}
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
		Vector3 newPositionPixel = new Vector3(thePixels[x,y].transform.position.x, thePixels[x,y].transform.position.y, -newDepthValue - 2);
		Vector3 newPositionOriginal = new Vector3(originalPositions[x,y].transform.position.x, originalPositions[x,y].transform.position.y, -newDepthValue - 2);
		originalPositions[x,y].transform.position = newPositionOriginal;
		thePixels[x,y].transform.position = newPositionPixel;
	}
	public void SetRenderer(int x, int y)
	{
		if((originalPositions[x, y].transform.position - middlePoint.transform.position).magnitude < 8.0f)
		{
			thePixels[x,y].renderer.enabled = true;
		}
		else
		{
			thePixels[x,y].renderer.enabled = false;
		}
	}
	public void DisableAllRenderers()
	{
		for(int i = 0; i < 80; i++)
		{
			for(int x = 0; x < 60; x++)
			{
				thePixels[i,x].renderer.enabled = false;
			}
		}
	}
	public void EnableAllRenderers()
	{
		for(int i = 0; i < 80; i ++)
		{
			for(int x = 0; x < 60; x++)
			{
				thePixels[i,x].renderer.enabled = true;
			}
		}
	}

}
